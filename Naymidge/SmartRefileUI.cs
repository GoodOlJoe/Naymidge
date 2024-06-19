namespace Naymidge
{
    public partial class SmartRefileUI : Form
    {
        private readonly List<FileInstruction> _Instructions;

        public SmartRefileUI(ProcessingScope scope)
        {
            InitializeComponent();

            // _Scope.Contents has the original files to be acted upon, as selected by the caller
            // _Instructions contains the decisions made by the user of this form: to delete or rename, etc.
            _Instructions = new List<FileInstruction>(scope.Contents.Count);
            foreach (string fqn in scope.Contents)
                _Instructions.Add(new FileInstruction(fqn));

            SourceCountLabel.Text = $"Move {_Instructions.Count} files";

            cmdProceed.Click += CmdProceed_Click;
            cmdCancel.Click += CmdCancel_Click;
            TimerUIRefresh.Tick += DoTimerUIRefresh_Tick;
            TimerUIRefresh.Enabled = true;
        }
        private void SetControlMruBindings()
        {
            TargetTextbox.DataBindings.Add(new Binding("Text", Properties.Settings.Default, "MruRefileTarget", true, DataSourceUpdateMode.OnPropertyChanged));
            TargetTextbox.Text = Properties.Settings.Default.MruRefileTarget;
            UseDateTakenCheckBox.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "MruUseDateTaken", true, DataSourceUpdateMode.OnPropertyChanged));
            UseDateTakenCheckBox.Checked = Properties.Settings.Default.MruUseDateTaken;
            FileByDateCheckBox.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "MruFileByDate", true, DataSourceUpdateMode.OnPropertyChanged));
            FileByDateCheckBox.Checked = Properties.Settings.Default.MruFileByDate;
        }
        private void DoPickTargetDirectory()
        {
            TargetDirectoryDialog.InitialDirectory = TargetTextbox.Text.Trim();
            TargetDirectoryDialog.CheckPathExists = true;
            TargetDirectoryDialog.CheckFileExists = false;
            TargetDirectoryDialog.OverwritePrompt = false;
            TargetDirectoryDialog.FileName = "Target This Directory";
            TargetDirectoryDialog.Title = "Choose target directory";
            TargetDirectoryDialog.Filter = "Directory|directory|All files|*.*";

            if (DialogResult.OK == TargetDirectoryDialog.ShowDialog(this))
            {
                TargetTextbox.Text = Path.GetDirectoryName(TargetDirectoryDialog.FileName);
            }
        }
        private void SmartRefileUI_Load(object sender, EventArgs e) { SetControlMruBindings(); }
        private void CmdCancel_Click(object? sender, EventArgs e) { DoCancelButtonClicked(); }
        private void CmdProceed_Click(object? sender, EventArgs e) { DoProceedButtonClicked(); }
        private void DoCancelButtonClicked() { Close(); }
        private void CmdPickContentDirectory_Click(object sender, EventArgs e) { DoPickTargetDirectory(); }
        private void DoProceedButtonClicked()
        {
            if (TargetValid(TargetTextbox.Text.Trim()))
            {
                ActionUI ui = new();
                bool useDateTakenIfFilenameUndated = FileByDateCheckBox.Checked && UseDateTakenCheckBox.Checked;
                ui.DoSmartRefiling(_Instructions, TargetTextbox.Text.Trim(), FileByDateCheckBox.Checked, useDateTakenIfFilenameUndated);
            }
        }
        private void UpdateUIEnablement()
        {
            cmdProceed.Enabled = _Instructions.Count > 0 && TargetTextbox.Text.Trim().Length > 0;
            UseDateTakenCheckBox.Enabled = FileByDateCheckBox.Checked;
            UpdateTargetExample();
        }
        private void UpdateTargetExample()
        {
            string target = TargetTextbox.Text.Trim();
            TargetExampleLabel.Text = "";
            if (!string.IsNullOrEmpty(target))
            {
                if (FileByDateCheckBox.Checked)
                {
                    TargetExampleLabel.Text = Path.Combine(target, "yyyy", "mm");
                    if (!UseDateTakenCheckBox.Checked)
                        TargetExampleLabel.Text += $"\r\n  OR\r\n{Path.Combine(target, "undated")}";
                }
                else
                    TargetExampleLabel.Text = $"{target} (no subdirectories)";
            }
        }
        private void DoTimerUIRefresh_Tick(object? sender, EventArgs e) { UpdateUIEnablement(); }
        private static bool TargetValid(string target)
        {
            if (Path.Exists(target)) return true;
            if (DialogResult.Yes == MessageBox.Show($"{target} does not exist. Create it?", "Create Directory?", buttons: MessageBoxButtons.YesNo))
            {
                try
                {
                    Directory.CreateDirectory(target);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"The system was unable to create {target}\r\n\r\n{ex.Message}");
                }
            }
            return false;
        }
    }
}
