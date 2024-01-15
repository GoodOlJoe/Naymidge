using FlyleafLib;
using FlyleafLib.MediaPlayer;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Naymidge
{
    public partial class RenameUI : Form
    {
        public Player PlayerMain { get; set; }
        public Config Config { get; set; }
        private readonly List<FileInstruction> _Instructions;
        private int CurrentItem = 0;
        private const string DeleteNotice = "(marked for deletion)";
        private const string ReuseLastNameShortcut = "ÿ";
        private int DecCurrent => CurrentItem == 0 ? _Instructions.Count - 1 : --CurrentItem;
        private int IncCurrent => CurrentItem >= _Instructions.Count - 1 ? 0 : ++CurrentItem;
        private const int _MaxFQNLength = 258; // windows limit
        private int _MaxFileNameLength = _MaxFQNLength; // adjusted for each file based on path

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

            txtNameInput.KeyPress += NameInput_KeyPress;
            cmdProceed.Click += CmdProceed_Click;

            flyleafHostMain.Player = PlayerMain;
            KeyboardShortcutsLabel.Text = @"Alt-B     Select a Back image to view           F10      Mark/Unmark for delete
Alt-E     Edit the name                         F11      Previous item
<number>  Reuse numbered name from the list     F12      Next item
<space>   Reuse most recent name";
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
            lblPositionDisplay.Text = $"{CurrentItem + 1}/{_Instructions.Count}";
            LayoutPositionDisplay();
            OpenMedia(PlayerMain, CurrentItem);
            PopulateBackImage(_Instructions[CurrentItem].FQN);
            UpdateFilenameCharCounter();
        }
        private void CalculateMaxFileNameLength()
        {
            string? path = Path.GetDirectoryName(_Instructions[CurrentItem].FQN);
            int pathLength = string.IsNullOrEmpty(path) ? 0 : path.Length;
            string? ext = Path.GetExtension(_Instructions[CurrentItem].FQN);
            int extLength = string.IsNullOrEmpty(ext) ? 0 : ext.Length;
            _MaxFileNameLength = _MaxFQNLength - pathLength - extLength;

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

            // now load the most likely back'sLineKey image
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
        private void CmdCancel_Click(object sender, EventArgs e) { DoCancelButtonClicked(); }
        private void CmdProceed_Click(object? sender, EventArgs e) { DoProceedButtonClicked(); }
        private void InnerContainer_SplitterMoved(object sender, SplitterEventArgs e) { DoLayout(); }
        private void OuterContainer_SplitterMoved(object sender, SplitterEventArgs e) { DoLayout(); }
        private void RenameUI_Load(object sender, EventArgs e)
        {
            DoLayout();
            WindowState = FormWindowState.Maximized;
        }
        private void RenameUI_Resize(object sender, EventArgs e) { DoLayout(); }
        private void TvAllBacks_AfterSelect(object sender, TreeViewEventArgs e) { DoBackImageSelectionChanged(e); }
        private void RenameUI_KeyUp(object sender, KeyEventArgs e) { e.Handled = FormKeyUpHandled(e); }
        private void NameInput_KeyPress(object? sender, KeyPressEventArgs e) { e.Handled = InputKeyPressHandled(e); }
        private void txtNameInput_TextChanged(object sender, EventArgs e) { UpdateFilenameCharCounter(); }
        private void AddRecentEntryToUi(string entry) { AddRecentEntryToHistory(entry); }
        private void SetBackDetailsLabelPosition() { DockUpperRight(BackDetailsLabel, PicboxBack); }
        private void DoCancelButtonClicked()
        {
            int delete = _Instructions.Where(inst => inst.Verb == FileInstructionVerb.Delete && !inst.Completed).Count();
            int rename = _Instructions.Where(inst => inst.Verb == FileInstructionVerb.Rename && !inst.Completed).Count();
            string msg = "";
            string pronoun = rename + delete > 1 ? "those" : "that";
            string changeNoun = rename + delete > 1 ? "changes" : "change";
            string renameNoun = rename > 1 ? "renames" : "rename";
            string deleteNoun = delete > 1 ? "deletes" : "delete";
            if (rename > 0 && 0 == delete)
                msg = $"You have {rename:N0} {renameNoun} pending, do you want to cancel and lose {pronoun}?";
            else if (delete > 0 && 0 == rename)
                msg = $"You have {delete:N0} {deleteNoun} pending, do you want to cancel and lose {pronoun}?";
            else if (rename > 0 && delete > 0)
                msg = $"You have {rename:N0} {renameNoun} and {delete:N0} {deleteNoun} pending, do you want to cancel and lose {pronoun}?";

            if (string.IsNullOrEmpty(msg) || MessageBox.Show(msg, $"Cancel pending {changeNoun}?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
        }
        private void DoProceedButtonClicked()
        {
            ActionUI ui = new();
            PlayerMain.Stop(); // release lock on the currently displayed media file
            ui.ProcessFileInstructions(_Instructions);
        }
        private void DoLayout()
        {
            MiddlePanel.Height = ClientRectangle.Height - UpperPanel.Height - LowerPanel.Height;
            MiddlePanel.Left = 0;
            MiddlePanel.Top = UpperPanel.Height;
            MiddlePanel.Width = ClientRectangle.Width;

            SetBackImagePosition();
            SetBackDetailsLabelPosition();
            UpdateProgressLabel();
            LayoutPositionDisplay();
        }
        private void LayoutPositionDisplay()
        {
            lblPositionDisplay.Top = 0;
            lblPositionDisplay.Left = UpperPanel.Width - lblPositionDisplay.Width;
        }
        private bool FormKeyUpHandled(KeyEventArgs e)
        {
            bool handled = false;
            if (!(e.Control || e.Shift || e.Alt))
            {
                switch (e.KeyCode)
                {
                    case Keys.F10: // toggle mark for delete
                        _Instructions[CurrentItem].ToggleDelete();
                        CurrentItem = IncCurrent;
                        SetUiToItem(CurrentItem);
                        handled = true;
                        break;
                    case Keys.F11:
                        CurrentItem = DecCurrent;
                        SetUiToItem(CurrentItem);
                        handled = true;
                        break;
                    case Keys.F12:
                        CurrentItem = IncCurrent;
                        SetUiToItem(CurrentItem);
                        handled = true;
                        break;
                }
            }
            return handled;
        }
        private bool InputKeyPressHandled(KeyPressEventArgs e)
        {
            Debug.WriteLine(e.KeyChar.ToString());
            if (e.KeyChar == '\r')
            {
                // special shortcut for reusing the most recent name is a space, saves them typing "1"
                string newName = txtNameInput.Text.Equals(" ") ? ReuseLastNameShortcut : txtNameInput.Text.Trim();

                if (!string.IsNullOrEmpty(newName))
                {
                    newName = newName.Equals(ReuseLastNameShortcut) ? "1" : newName;

                    // first see if they are resuing a numbered entry from the list
                    if (int.TryParse(newName, out int reuseIndex))
                    {
                        string reusedName = GetNumberedEntryFromHistory(reuseIndex);
                        newName = string.IsNullOrEmpty(reusedName) ? newName : reusedName;
                    }

                    if (!newName.Equals(DeleteNotice))
                    {
                        _Instructions[CurrentItem].Rename(newName);
                        AddRecentEntryToUi(newName);
                    }
                }
                CurrentItem = IncCurrent;
                SetUiToItem(CurrentItem);
                return true;
            }
            return false;
        }
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
        private void UpdateProgressLabel()
        {
            ProgressLabel.Text = "";
            if (_Instructions == null || _Instructions.Count == 0) return;
            int undetermined = _Instructions.Where(inst => inst.Verb == FileInstructionVerb.Undetermined).Count();
            int delete = _Instructions.Where(inst => inst.Verb == FileInstructionVerb.Delete && !inst.Completed).Count();
            int rename = _Instructions.Where(inst => inst.Verb == FileInstructionVerb.Rename && !inst.Completed).Count();


            ProgressLabel.Text =
@$"delete    {delete,6:N0}
rename    {rename,6:N0}
no action {undetermined,6:N0}
----------------
          {_Instructions.Count,6:N0}";

            ProgressLabel.BackColor = 0 == undetermined ?
                Color.PaleGreen :
                Color.Transparent;
        }
        private void UpdateFilenameCharCounter()
        {
            CalculateMaxFileNameLength();
            int remaining = _MaxFileNameLength - txtNameInput.Text.Trim().Length;
            FilenameCharCountLabel.Text = remaining.ToString();
            txtNameInput.BackColor = remaining < 0 ? Color.MediumVioletRed : Color.White;
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
        private string GetNumberedEntryFromHistory(int index)
        {
            char[] delims = ['\r', '\n'];
            string[] lines = TxtRecent.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            List<string> unnumberedEntries = new(lines.Length + 1);
            foreach (string l in lines)
            {
                string line = l.Trim();
                string sLineKey = line;
                sLineKey = sLineKey[..(sLineKey.IndexOf(' ') + 1)].Trim();
                if (sLineKey.Equals(index.ToString()))
                {
                    return line[(line.IndexOf(' ') + 1)..];
                }
            }
            return "";
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
            TxtRecent.SelectionStart = TxtRecent.Text.Length;
            TxtRecent.ScrollToCaret();
        }
    }
}
