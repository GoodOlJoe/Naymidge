using FlyleafLib;
using FlyleafLib.MediaPlayer;

namespace Naymidge
{
    public partial class RenameUI : Form
    {
        public Player Player { get; set; }
        public Config Config { get; set; }

        public RenameUI()
        {
            Engine.Start(new EngineConfig()
            {
                //FFmpegPath = @"C:\Flyleaf\FFmpeg",
                FFmpegPath = ":FFmpeg",
                FFmpegDevices = false,    // Prevents loading avdevice/avfilter dll files. Enable it only if you plan to use dshow/gdigrab etc.
                //PluginsPath = ":Plugins",

#if RELEASE
                FFmpegLogLevel      = FFmpegLogLevel.Quiet,
                LogLevel            = LogLevel.Quiet,

#else
                FFmpegLogLevel = FFmpegLogLevel.Warning,
                LogLevel = LogLevel.Debug,
                LogOutput = ":debug",
                //LogOutput         = ":console",
                //LogOutput         = @"C:\Flyleaf\Logs\flyleaf.log",                
#endif

                //PluginsPath       = @"C:\Flyleaf\Plugins",

                UIRefresh = false,    // Required for Activity, BufferedDuration, Stats in combination with Config.Player.Stats = true
                UIRefreshInterval = 250,      // How often (in ms) to notify the UI
                UICurTimePerSecond = true,     // Whether to notify UI for CurTime only when it's second changed or by UIRefreshInterval
            });

            Config = new Config();
            //Config.Video.BackgroundColor = System.Drawing.Color.DarkGray;

            Player = new Player(Config);
            InitializeComponent();
            flyleafHost.Player = Player;

        }
    }
}
