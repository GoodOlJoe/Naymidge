using FlyleafLib;
using FlyleafLib.MediaPlayer;
using System.Data;
using System.Diagnostics;
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
        private const string DeleteNotice = "(marked for deletion)";
        private const string ReuseLastNameShortcut = "�";
        private int DecCurrent => CurrentItem == 0 ? _Instructions.Count - 1 : --CurrentItem;
        private int IncCurrent => CurrentItem >= _Instructions.Count - 1 ? 0 : ++CurrentItem;
        private const int _MaxFQNLength = 258; // windows limit
        private int _MaxFileNameLength = _MaxFQNLength; // adjusted for each file based on path
        private bool SuggestDateStamp = false;
        private readonly Color colorNormal = SystemColors.WindowText;
        private readonly Color colorHint = SystemColors.InactiveCaption;

        public RenameUI(ProcessingScope scope, RenameParameters renameParameters)
        {
            SuggestDateStamp = renameParameters.SuggestDateStamp;

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

            MapLinkLabel.LinkClicked += MapLinkLabel_LinkClicked;
            txtNameInput.KeyPress += NameInput_KeyPress;
            cmdProceed.Click += CmdProceed_Click;

            flyleafHostMain.Player = PlayerMain;
            KeyboardShortcutsLabel.Text = @"Alt-B    Select a Back image to view        F10    Mark/ Unmark for delete        <number> Reuse numbered name from the list
Alt-E    Edit the name                      F11    Previous item                  <space>  Reuse most recent name
Alt-D    Enter Date Taken                   F12    Next item";
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
            UpdateMediaDetails();
            lblPositionDisplay.Text = $"{CurrentItem + 1}/{_Instructions.Count}";
            LayoutPositionDisplay();
            OpenMedia(PlayerMain, CurrentItem);

            // the relationship between the front image filename and the
            // corresponding back image file name differs depending on how we
            // scanned. And the patterns are dynamic so we unfortunately
            // separate standalone functions for finding the right back image.
            // Any of these BackImageHandler functions has to test the incoming
            // filename to see if it fits "its" pattern, and if it does, load up
            // the back image and return true. If it doesn't fit "its" pattern,
            // it returns false.
            List<Func<string, bool>> BackImageHandlers = [
                PopulateBackImageFastFotoPattern,
                PopulateBackImageHomeGrownPattern,
                ];

            ClearBackImage();
            for (int i = 0; i < BackImageHandlers.Count; i++)
            {
                if (BackImageHandlers[i](_Instructions[CurrentItem].FQN))
                    break;
            }

            UpdateFilenameCharCounter();
        }
        private void ClearBackImage() { PicboxBack.Image = null; }
        private void UpdateMediaDetails()
        {
            FileInstruction finst = _Instructions[CurrentItem];

            string DateTaken = string.IsNullOrEmpty(finst.DateTimeTaken) ? "" : $"\nTaken: {finst.DateTimeTaken}";
            MediaDetailsLabel.Text = $"{finst.FQN}{DateTaken}";

            MapLinkLabel.Tag = finst.MapURL;
            MapLinkLabel.Visible = !string.IsNullOrEmpty(finst.MapURL);

            //IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(finst.FQN);
            //foreach (var dir in directories)
            //    foreach (var tag in dir.Tags)
            //        Debug.WriteLine($"{dir.Name} - {tag.Name} = {tag.Description}");
        }
        private void CalculateMaxFileNameLength()
        {
            string? path = Path.GetDirectoryName(_Instructions[CurrentItem].FQN);
            int pathLength = string.IsNullOrEmpty(path) ? 0 : path.Length;
            string? ext = Path.GetExtension(_Instructions[CurrentItem].FQN);
            int extLength = string.IsNullOrEmpty(ext) ? 0 : ext.Length;
            _MaxFileNameLength = _MaxFQNLength - pathLength - extLength;

        }

        // WHERE I'M AT AS OF 6 FEB 2025 I guess I'm not really going to be able
        // to use this approach because the regex patterns are dynamic for each
        // image. I may have to have separate functions (modified copies ofr
        // PopulateBackImage that do their own regex for the types of patterns,
        // and just call each one until one returns True or something/ put the
        // most frequent ones first (now the most frequent one will be the
        // FastFoto patterns where *_a.jpg is the front and *_b.jpg is the back

        private class FrontBackImagePattern
        {
            public string BackPatternForThisFront = "";
            public string BackWildcard = "";
            public string ExactBack = "";
        }

        private bool PopulateBackImageFastFotoPattern(string frontImageFQN)
        {
            string ext = Path.GetExtension(frontImageFQN);
            string backPatternForThisFront = @$"^(?<rootname>((?<prefix>.+)_(?<serial>\d\d\d\d)))(_a)?(?<ext>{ext})$";
            string fname = Path.GetFileName(frontImageFQN);
            string? dn = Path.GetDirectoryName(frontImageFQN);
            string dname = string.IsNullOrEmpty(dn) ? "" : dn;

            Match match = Regex.Match(fname, backPatternForThisFront);
            if (!match.Success) return false;

            string backFName = Path.Combine(dname, $"{match.Groups["rootname"]}_b{ext}");
            if (File.Exists(backFName))
            {
                // now load the most likely back'sLineKey image
                DisplayBackImage(backFName);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool PopulateBackImageHomeGrownPattern(string frontImageFQN)
        {
            string ext = Path.GetExtension(frontImageFQN);
            string backPatternForThisFront = $"^(?<prefix>[A-Za-z]+)1(?<serial>.+)(?<ext>{ext})$";
            string fname = Path.GetFileName(frontImageFQN);
            string? dn = Path.GetDirectoryName(frontImageFQN);
            string dname = string.IsNullOrEmpty(dn) ? "" : dn;

            Match match = Regex.Match(fname, backPatternForThisFront);
            if (!match.Success) return false;

            // populate list with 'possible' backs for the given front image
            string backWildcard = $"{match.Groups["prefix"].Value}2*{ext}";
            List<string> matchingBacks =
                [.. System.IO.Directory.EnumerateFiles(dname, backWildcard, SearchOption.TopDirectoryOnly)];

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

            return true;
        }
        private void OpenMedia(Player player, int index)
        {
            if (player == null) return;
            if (index < 0 || index > _Instructions.Count) return;
            player.Rotation = RotationNeeded(_Instructions[index].RequiredRotationToNormal);
            player.OpenAsync(_Instructions[index].FQN);
        }
        private static uint RotationNeeded(string rot)
        {
            return rot switch
            {
                "180" => 180,
                "90" => 90,
                "270" => 270,
                _ => 0,
            };
        }
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
        private void TxtNameInput_TextChanged(object sender, EventArgs e)
        {
            txtNameInput.ForeColor = colorNormal; // once they're editing, switch to normal color (not 'hint' color)
            UpdateFilenameCharCounter();
        }
        private void MapLinkLabel_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (null == MapLinkLabel.Tag) return;
            string? target = MapLinkLabel.Tag.ToString();
            if (string.IsNullOrEmpty(target)) return;
            Process.Start(new ProcessStartInfo(target) { UseShellExecute = true });
        }
        private void AddRecentEntryToUi(string entry) { AddRecentEntryToHistory(entry); }
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
            {
                PlayerMain.Stop(); // release lock on the currently displayed media file
                Close();
            }
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

                // we populate the name field with a helper value of the date taken. But if they hit enter
                // without changing that value, we don't want it to "count" as the new name, so we ignore
                // that special case where there is a DateTaken metadata value and the new name equals that 
                // metadata value exactly
                if (!string.IsNullOrEmpty(_Instructions[CurrentItem].DateTaken) && newName.Trim().Equals(_Instructions[CurrentItem].DateTaken.Trim()))
                    newName = "";

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
                    txtNameInput.ForeColor = colorNormal; // what's showing in the text box is the real rename for this item
                    break;
                case FileInstructionVerb.Delete:
                    txtNameInput.Text = DeleteNotice;
                    txtNameInput.ForeColor = colorNormal; // deletes always show in the normal color
                    break;
                default:
                    if (txtNameInput.Text.Equals(DeleteNotice)) txtNameInput.Clear();
                    // if there's no name, put a helper value of the date taken
                    if (SuggestDateStamp) txtNameInput.Text = $"{_Instructions[itemNdx].DateTaken} ";
                    txtNameInput.SelectionStart = txtNameInput.Text.Length;
                    txtNameInput.SelectionLength = 0;
                    // what's showing in the text box is the name of the previous item. We leave it there
                    // because it's often easier to modify then type a new name each time. But the color
                    // shows there's no name established for the current item yet
                    txtNameInput.ForeColor = colorHint;
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
            }
            else
            {
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
        private string GetNumberedEntryFromHistory(int index)
        {
            char[] delims = ['\r', '\n'];
            string[] lines = TxtRecent.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries);
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
