using FlyleafLib;
using FlyleafLib.Controls.WinForms;
using FlyleafLib.MediaPlayer;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Naymidge
{
    public partial class RenameUI : Form
    {
        public Player PlayerMain { get; set; }
        public Config Config { get; set; }
        //private readonly ProcessingScope _Scope;
        private readonly List<FileInstruction> _Instructions;
        private int CurrentItem = 0;
        public RenameUI(ProcessingScope scope)
        {
            // _Scope.Contents has the original files to be acted upon, as selected by the caller
            // _Instructions contains the decisions made by the user of this form: to delete or rename, etc.
            _Instructions = new List<FileInstruction>(scope.Contents.Count);
            foreach(string fqn in scope.Contents)
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
        private void RenameUI_KeyUp(object sender, KeyEventArgs e) { e.SuppressKeyPress = FormKeyUpHandled(sender, e); }
        private void RenameUI_Load(object sender, EventArgs e) { DoLayout(); }
        private void RenameUI_Resize(object sender, EventArgs e) { DoLayout(); }
        private void TvAllBacks_AfterSelect(object sender, TreeViewEventArgs e) { DoBackImageSelectionChanged(e); }
        private void TxtNameInput_KeyUp(object sender, KeyEventArgs e) { e.SuppressKeyPress = InputKeyUpHandled(sender, e); }

        private void DoCloseButtonClicked() { Close(); }
        private void DoLayout()
        {
            OuterContainer.Height = ClientRectangle.Height - UpperPanel.Height - LowerPanel.Height;
            OuterContainer.Left = 0;
            OuterContainer.Top = UpperPanel.Height;
            OuterContainer.Width = ClientRectangle.Width;

            SetBackImagePosition();
            SetBackDetailsLabelPosition();
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
                }
            }

            return false;
        }
        private bool InputKeyUpHandled(object sender, KeyEventArgs e)
        {
            if (!(e.Control && e.Shift && e.Alt))
            {
                switch (e.KeyCode)
                {
                    // ctrl e - focus on name input field
                    case Keys.Enter:
                        CurrentItem++;
                        txtNameInput.Clear();
                        UpdateDisplays();
                        return true;
                }
            }
            return false;
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
        private void SetBackDetailsLabelPosition() { DockUpperRight(BackDetailsLabel, PicboxBack); }
        private void PicboxBack_SizeChanged(object sender, EventArgs e) { DoLayout(); }

        private void RenameUI_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = FormKeyDownHandled(e);
        }
        Label? KeyboardShortcutsHelp = null;
        private void KeyboardShortcuts(bool visible)
        {
            Control p = tvRecent; // UpperPanel; // parent
            if (visible)
            {
                if (null == KeyboardShortcutsHelp)
                {
                    KeyboardShortcutsHelp = new()
                    {
                        Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Monospace), Font.Size),
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
    }
}