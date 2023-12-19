using FlyleafLib;
using FlyleafLib.Controls.WinForms;
using FlyleafLib.MediaPlayer;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Naymidge
{
    public partial class RenameUI : Form
    {
        public Player PlayerMain { get; set; }
        public Config Config { get; set; }
        private readonly List<FileInstruction> _Instructions;
        private int CurrentItem = 0;
        private Label? KeyboardShortcutsHelp = null;
        private AutoCompleteStringCollection AllAutoCompleteTerms = new AutoCompleteStringCollection();
        private List<string> AllAcTermsList = new(1000);
        private Regex AutoCompleteParsePattern = new Regex(@"[A-Za-z]{3,}", RegexOptions.Compiled | RegexOptions.NonBacktracking);

        public RenameUI(ProcessingScope scope)
        {
            // _Scope.Contents has the original files to be acted upon, as selected by the caller
            // _Instructions contains the decisions made by the user of this form: to delete or rename, etc.
            _Instructions = new List<FileInstruction>(scope.Contents.Count);
            foreach (string fqn in scope.Contents)
                _Instructions.Add(new FileInstruction(fqn));

            CurrentItem = 0;
            Engine.Start(new EngineConfig()
            {
                FFmpegPath = ":FFmpeg",
                FFmpegDevices = false,    // Prevents loading avdevice/avfilter dll files. Enable it only if you plan to use dshow/gdigrab etc.
                PluginsPath = ":Plugins",
#if RELEASE
                FFmpegLogLevel      = FFmpegLogLevel.Quiet,
                LogLevel            = LogLevel.Quiet,
#else
                FFmpegLogLevel = FFmpegLogLevel.Warning,
                LogLevel = LogLevel.Debug,
                LogOutput = ":debug", /* ":console" *//* @"C:\Flyleaf\Logs\flyleaf.log" */
#endif
                UIRefresh = false,    // Required for Activity, BufferedDuration, Stats in combination with Config.PlayerMain.Stats = true
                UIRefreshInterval = 250,      // How often (in ms) to notify the UI
            });
            Config = new Config();
            PlayerMain = new Player(Config);
            InitializeComponent();
            flyleafHostMain.Player = PlayerMain;

            txtNameInput.AutoCompleteCustomSource = AllAutoCompleteTerms;
            txtNameInput.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtNameInput.AutoCompleteSource = AutoCompleteSource.CustomSource;

            UpdateDisplays();
            DoLayout();
        }
        private void UpdateDisplays()
        {
            if (CurrentItem < 0 || CurrentItem > _Instructions.Count)
            {
                MediaDetailsLabel.Text = "";
                return;
            }
            MediaDetailsLabel.Text = _Instructions[CurrentItem].FQN;
            OpenMedia(PlayerMain, CurrentItem);
            PopulateBackImage(_Instructions[CurrentItem].FQN);
        }
        private void PopulateBackImage(string frontImageFQN)
        {
            string ext = Path.GetExtension(frontImageFQN);
            string backPatternForThisFront = $"^(?<prefix>[A-Za-z]+)1(?<serial>.+)(?<ext>{ext})$";
            string fname = Path.GetFileName(frontImageFQN);
            string? dn = Path.GetDirectoryName(frontImageFQN);
            string dname = string.IsNullOrEmpty(dn) ? "" : dn;

            Match match = Regex.Match(fname, backPatternForThisFront);
            if (!match.Success) return;

            // populate list with 'possible' backs for the given front image
            string backWildcard = $"{match.Groups["prefix"].Value}2*{ext}";
            Regex rgxMatchingBacks = new($"^([A-Za-z]+)1(.+)({ext})$");
            List<string> matchingBacks =
                [.. Directory.EnumerateFiles(dname, backWildcard, SearchOption.TopDirectoryOnly)];

            // list all matching backs
            TvAllBacks.Nodes.Clear();
            TreeNode root = new("Back Images");
            TvAllBacks.Nodes.Add(root);
            foreach (string possibleBack in matchingBacks)
            {
                string backFn = Path.GetFileName(possibleBack);
                TreeNode backnode = new(backFn) { Tag = possibleBack };
                root.Nodes.Add(backnode);
            }
            TvAllBacks.ExpandAll();

            // now load the most likely back's image
            string exactBack = $"{match.Groups["prefix"].Value}2{match.Groups["serial"].Value}{ext}";
            DisplayBackImage(Path.Combine(dname, exactBack));
        }
        private void OpenMedia(Player player, int index)
        {
            if (player == null) return;
            if (index < 0 || index > _Instructions.Count) return;
            player.OpenAsync(_Instructions[index].FQN);
        }

        private void BackDetailsLabel_TextChanged(object sender, EventArgs e) { SetBackDetailsLabelPosition(); }
        private void CmdClose_Click(object sender, EventArgs e) { DoCloseButtonClicked(); }
        private void InnerContainer_SplitterMoved(object sender, SplitterEventArgs e) { DoLayout(); }
        private void OuterContainer_SplitterMoved(object sender, SplitterEventArgs e) { DoLayout(); }
        //private void PicboxBack_SizeChanged(object sender, EventArgs e) { DoLayout(); }
        private void RenameUI_KeyDown(object sender, KeyEventArgs e) { e.SuppressKeyPress = FormKeyDownHandled(e); }
        private void RenameUI_KeyUp(object sender, KeyEventArgs e) { e.SuppressKeyPress = FormKeyUpHandled(sender, e); }
        private void RenameUI_Load(object sender, EventArgs e) { DoLayout(); }
        private void RenameUI_Resize(object sender, EventArgs e) { DoLayout(); }
        private void TvAllBacks_AfterSelect(object sender, TreeViewEventArgs e) { DoBackImageSelectionChanged(e); }
        private void NameInput_KeyUp(object sender, KeyEventArgs e) { e.SuppressKeyPress = InputKeyUpHandled(sender, e); }

        private void DoCloseButtonClicked() { Close(); }
        private void DoLayout()
        {
            MiddlePanel.Height = ClientRectangle.Height - UpperPanel.Height - LowerPanel.Height;
            MiddlePanel.Left = 0;
            MiddlePanel.Top = UpperPanel.Height;
            MiddlePanel.Width = ClientRectangle.Width;

            SetBackImagePosition();
            SetBackDetailsLabelPosition();
            UpdateProgressLabel();
        }
        private bool FormKeyUpHandled(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                KeyboardShortcuts(visible: false);
                switch (e.KeyCode)
                {
                    case Keys.E: txtNameInput.Focus(); return true; // ctrl e - focus on name input field
                    case Keys.B: TvAllBacks.Focus(); return true;   // ctrl b - focus on back image names
                    case Keys.Left: CurrentItem = DecCurrent; SetUiToItem(CurrentItem); return true;
                    case Keys.Right: CurrentItem = IncCurrent; SetUiToItem(CurrentItem); return true;
                }
            }

            return false;
        }
        private bool InputKeyUpHandled(object sender, KeyEventArgs e)
        {
            bool changingItems = false;
            if (!(e.Control || e.Shift || e.Alt))
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        string newName = txtNameInput.Text.Trim();
                        if (!string.IsNullOrEmpty(newName) && !newName.Equals(DeleteNotice))
                        {
                            _Instructions[CurrentItem].Rename(newName);
                            AddRecentEntryToUi(newName);
                        }
                        CurrentItem = IncCurrent;
                        changingItems = true;
                        break;
                }
            }
            else if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D:
                        _Instructions[CurrentItem].Delete();
                        CurrentItem = IncCurrent;
                        changingItems = true;
                        break;
                }
            }
            if (changingItems)
            {
                SetUiToItem(CurrentItem);
                return true;
            }
            return false;
        }
        private const string DeleteNotice = "(marked for deletion)";
        private int DecCurrent => CurrentItem == 0 ? _Instructions.Count - 1 : --CurrentItem;
        private int IncCurrent => CurrentItem >= _Instructions.Count - 1 ? 0 : ++CurrentItem;
        private void SetUiToItem(int itemNdx)
        {
            switch (_Instructions[itemNdx].Verb)
            {
                case FileInstructionVerb.Rename:
                    string? newName = _Instructions[itemNdx].NewFileName;
                    newName = string.IsNullOrEmpty(newName) ? "" : newName;
                    txtNameInput.Text = newName;
                    break;
                case FileInstructionVerb.Delete:
                    txtNameInput.Text = DeleteNotice;
                    break;
                default:
                    txtNameInput.Clear();
                    break;
            }
            UpdateProgressLabel();
            UpdateDisplays();
        }
        //private void CreateRenameInstruction(int item, string newName) { _Instructions[item].Rename(newName); }
        //private void CreateDeleteInstruction(int item) { _Instructions[item].Delete(); }
        private void DoBackImageSelectionChanged(TreeViewEventArgs e) { if (e.Node?.Tag is string FQN) DisplayBackImage(FQN); }
        private void DisplayBackImage(string FQN)
        {
            if (File.Exists(FQN))
            {
                PicboxBack.ImageLocation = FQN;
                BackDetailsLabel.Text = Path.GetFileName(FQN);
            }
            else
            {
                BackDetailsLabel.Text = "";
                PicboxBack.Image = null;
            }
        }
        private void SetBackImagePosition()
        {
            if (PicboxBack.Parent != null)
                DockUpperRight(PicboxBack, PicboxBack.Parent);
        }
        private void DockUpperRight(Control c, Control relativeTo)
        {
            // dock c relative to the given control, whether or not 
            // the "relative to" control is the parent of c
            Point rLocation = relativeTo.PointToScreen(Point.Empty);
            Point cLocation = new(rLocation.X + relativeTo.Width - c.Width, rLocation.Y);
            if (null == c.Parent)
                c.Location = PointToClient(cLocation); // place on Form
            else
                c.Location = c.Parent.PointToClient(cLocation); // place on Parent
        }
        private void SetBackDetailsLabelPosition()
        {
            DockUpperRight(BackDetailsLabel, PicboxBack);
        }
        private void KeyboardShortcuts(bool visible)
        {
            Control p = TxtRecent; // UpperPanel; // parent
            if (visible)
            {
                if (null == KeyboardShortcutsHelp)
                {
                    KeyboardShortcutsHelp = new()
                    {
                        Font = new System.Drawing.Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Monospace), Font.Size),
                        BackColor = Color.AntiqueWhite,
                        Text = @"
  Keyboard Shortcuts
  ------------------
  Ctrl-D     Delete the item (mark for delete)
  Ctrl-B     Select a Back image to view
  Ctrl-E     Edit the name
  Ctrl-Left  Previous item
  Ctrl-Right Next item
  <number>   Reuse numbered name from the list
"
                    };
                    p.Controls.Add(KeyboardShortcutsHelp);
                }
                KeyboardShortcutsHelp.Top = 10;
                KeyboardShortcutsHelp.Left = 10;
                KeyboardShortcutsHelp.Width = p.ClientRectangle.Width - 20;
                KeyboardShortcutsHelp.Height = p.ClientRectangle.Height - 20;
                KeyboardShortcutsHelp.Visible = true;
            }
            else
            {
                if (null != KeyboardShortcutsHelp)
                {
                    KeyboardShortcutsHelp.Visible = false;
                }
            }
        }
        private bool FormKeyDownHandled(KeyEventArgs e)
        {
            if (e.Control) KeyboardShortcuts(visible: true);
            return false;
        }
        private void UpdateProgressLabel()
        {
            ProgressLabel.Text = "";
            if (_Instructions == null || _Instructions.Count == 0) return;
            int undetermined = _Instructions.Where(inst => inst.Verb == FileInstructionVerb.Undetermined).Count();
            ProgressLabel.Text = $"{_Instructions.Count - undetermined:N0}/{_Instructions.Count:N0}";
        }
        private string? GetNumberedRecentEntry(string entryNumber)
        {
            char[] delims = ['\r', '\n'];
            string[] lines = TxtRecent.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            string? match = lines.Where(s => s.Trim().StartsWith($"{entryNumber} ")).FirstOrDefault();
            if (match == null)
                return null;
            else
                return match.Trim()[(match.IndexOf(' ') + 1)..];
        }
        private void AddRecentEntryToUi(string entry)
        {
            AddRecentEntryToAutoComplete(entry);
            AddRecentEntryToHistory(entry);
        }
        private readonly List<string> AutoCompleteNoiseWords = ["and", "of", "the"];
        private void AddRecentEntryToAutoComplete(string entry)
        {
            foreach (Match m in AutoCompleteParsePattern.Matches(entry).Where(m => !AutoCompleteNoiseWords.Contains(m.Value)))
            {
                AllAutoCompleteTerms.Add(m.Value);
                if (!AllAcTermsList.Contains(m.Value))
                {
                    AllAcTermsList.Add(m.Value);
                }
            }
        }
        private void AddRecentEntryToHistory(string entry)
        {
            char[] delims = ['\r', '\n'];
            string[] lines = TxtRecent.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            List<string> unnumberedEntries = new(lines.Length + 1);
            foreach (string l in lines)
            {
                string s = l.Trim();
                s = s[(s.IndexOf(' ') + 1)..];
                if (!s.ToLower().Equals(entry.ToLower()))
                    unnumberedEntries.Add(s);
            }

            StringBuilder numberedEntries = new(unnumberedEntries.Count + 1);
            int i = unnumberedEntries.Count + 1;
            foreach (string s in unnumberedEntries)
                numberedEntries.AppendLine($"{i--,4:D} {s}");
            numberedEntries.AppendLine($"{1,4:D} {entry}");

            TxtRecent.Text = numberedEntries.ToString();
        }

        private void CboNameInput_KeyUp(object sender, KeyEventArgs e) { DoNameInputAutoComplete(e); }
        private void DoNameInputAutoComplete(KeyEventArgs e)
        {
            //use keyUp event, as text changed traps too many other evengts.

            string sBoxText = CboNameInput.Text;
            AutoCompleteStringCollection filteredTerms = [.. AllAcTermsList.Where(s => s.Contains(sBoxText))];
            //AutoCompleteStringCollection filteredTerms = new();
            //foreach (string matchingTerm in AllAcTermsList.Where(s => s.Contains(sBoxText)))
            //{
            //    filteredTerms.Add(matchingTerm);
            //}


            ////NOW THAT WE HAVE OUR FILTERED LIST, WE NEED TO RE-BIND IT WIHOUT CHANGING THE TEXT IN THE cbox
            ////1).UNREGISTER THE SELECTED EVENT BEFORE RE-BINDING, b/c IT TRIGGERS ON BIND.
            //CboNameInput.SelectedIndexChanged -= CboNameInput_SelectedIndexChanged; //don't select on typing.
            //CboNameInput.DataSource = filteredTerms; //2).rebind to filtered list.
            //CboNameInput.SelectedIndexChanged += CboNameInput_SelectedIndexChanged;

            ////3).show the user the new filtered list.
            //CboNameInput.DroppedDown = true; //this will overwrite the text in the ComboBox, so 4&5 put it back.

            ////4).binding data source erases text, so now we need to put the user's text back,
            //CboNameInput.Text = sBoxText;
            //CboNameInput.SelectionStart = sBoxText.Length; //5). need to put the user's cursor back where it was.
        }

        private void CboNameInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboNameInput.SelectedValue != null)
            {
                Debug.WriteLine(string.Format(@"Item #{0} was selected.", CboNameInput.SelectedValue));
            }

        }
    }
}
