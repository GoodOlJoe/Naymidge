using FlyleafLib;
using FlyleafLib.Controls.WinForms;
using FlyleafLib.MediaPlayer;

namespace Naymidge
{
    public partial class RenameUI : Form
    {
        public Player PlayerMain { get; set; }
        public Player PlayerPrev { get; set; }
        public Player PlayerNext { get; set; }
        public Config Config { get; set; }
        private readonly ProcessingScope _Scope;
        private int CurrentItem = 0;
        public RenameUI(ProcessingScope scope)
        {
            _Scope = scope;
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
                //UICurTimePerSecond = true,     // Whether to notify UI for CurTime only when it's second changed or by UIRefreshInterval
            });

            Config = new Config();
            //Config.Video.BackgroundColor = System.Drawing.Color.DarkGray;

            PlayerMain = new Player(Config);
            PlayerPrev = new Player();
            PlayerPrev.Audio.Volume = 0;
            PlayerNext = new Player();
            PlayerNext.Audio.Volume = 0;
            InitializeComponent();

            // Parse the control to the Player
            flyleafHostMain.Player = PlayerMain;
            //flyleafHostMain.Player.PropertyChanged += Player_PropertyChanged; // On Swap you should unsubscribe player 1 and subscribe to player 2
            flyleafHostPrev.Player = PlayerPrev;
            flyleafHostNext.Player = PlayerNext;
            flyleafHostPrev.Player.Activity.Timeout = 2000;
            flyleafHostNext.Player.Activity.Timeout = 2000;

            UpdateDisplays();
        }
        private void UpdateDisplays()
        {
            if (CurrentItem < 0 || CurrentItem > _Scope.Contents.Count)
            {
                MediaDetailsLabel.Text = "";
                return;
            }
            MediaDetailsLabel.Text = _Scope.Contents[CurrentItem];
            OpenMedia(PlayerMain, CurrentItem);
            OpenMedia(PlayerPrev, CurrentItem-1);
            OpenMedia(PlayerNext, CurrentItem+1);
        }
        private void OpenMedia(Player player, int index)
        {
            if (player == null) return;
            if (index < 0 || index > _Scope.Contents.Count) return;
            player.OpenAsync(_Scope.Contents[index]);
        }
        private void RenameUI_Load(object sender, EventArgs e) { DoLayout(); }
        private void RenameUI_Resize(object sender, EventArgs e) { DoLayout(); }
        private void DoLayout()
        {
            OuterContainer.Height = ClientRectangle.Height - UpperPanel.Height - LowerPanel.Height;
            OuterContainer.Left = 0;
            OuterContainer.Top = UpperPanel.Height;
            OuterContainer.Width = ClientRectangle.Width;

            flyleafHostPrev.Width = (int)(InnerContainer.Panel2.ClientRectangle.Width * .4);
            flyleafHostNext.Width = flyleafHostPrev.Width;
        }
        private void CmdClose_Click(object sender, EventArgs e) { DoCloseForm(); }
        private void DoCloseForm() { Close(); }
        private void RenameUI_KeyUp(object sender, KeyEventArgs e) { e.SuppressKeyPress = FormKeyUpHandled(sender, e); }
        private bool FormKeyUpHandled(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    // ctrl e - focus on name input field
                    case Keys.E: txtNameInput.Focus(); return true;
                }
            }
            return false;
        }

        private void TxtNameInput_KeyUp(object sender, KeyEventArgs e) { e.SuppressKeyPress = InputKeyUpHandled(sender, e); }
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
    }
}
