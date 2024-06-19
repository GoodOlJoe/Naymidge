using System.Data;

namespace Naymidge
{
    public partial class ActionUI : Form
    {
        private bool _UserCancel;
        public ActionUI()
        {
            InitializeComponent();
            cmdClose.Click += CmdClose_Click;
            cmdCancel.Click += CmdCancel_Click;
        }
        private void CmdClose_Click(object? sender, EventArgs e) { DoCloseButtonClicked(); }
        private void CmdCancel_Click(object? sender, EventArgs e) { DoCancelButtonClicked(); }
        private void ActionUI_Load(object sender, EventArgs e)
        {
            TextErrors.Visible = false;
            WindowState = FormWindowState.Maximized;
            DoLayout();
        }
        private void DoCloseButtonClicked() { Close(); }
        private void DoCancelButtonClicked() { _UserCancel = true; }
        /// <summary>
        /// Move each file in the given file instructions to the target, renaming the file with a sequence number
        /// first if needed to avoid filename collisions if necessary.
        /// </summary>
        /// <param name="instructions">List of FileInstruction objects to act on</param>
        /// <param name="target">The fully qualified target directory to move each file to (if fileByDate is false) or the root directory under which a date/month filing structure will be used to move the files to (if fileByDate is true)</param>
        /// <param name="fileByDate">False to move the files directory to the target directory. True to add a date/month filing structure under the target directory</param>
        internal void DoSmartFiling(List<FileInstruction> instructions, string target, bool fileByDate)
        {
            target = @"E:\All\Photos\MomDad"; // todo: use the path passed in
            fileByDate = true; // todo: use the switch passed in

            string spacing = TextStatus.Text.Length > 0 ? "\r\n\r\n" : "";
            TextStatus.Text += $"{spacing}DELETING\r\n";
            foreach (FileInstruction fi in instructions)
            {
                ProgressBar.Value++;
                try
                {
                    FileActions.DoSmartFile(fi);
                    TextStatus.Text += $"  {fi.FileName}\r\n";
                    delete.Completed = true;
                }
                catch (Exception ex)
                {
                    LogError($"  *** ERROR {delete.FileName}\r\n{ex.Message}");
                }
                TextStatus.SelectionStart = TextStatus.Text.Length;
                TextStatus.ScrollToCaret();
                ProgressBar.Value = 0;
            }

        }
        internal void ProcessFileInstructions(List<FileInstruction> instructions)
        {
            int delete = instructions.Where(inst => inst.Verb == FileInstructionVerb.Delete && !inst.Completed).Count();
            int rename = instructions.Where(inst => inst.Verb == FileInstructionVerb.Rename && !inst.Completed).Count();
            if (0 == delete + rename)
            {
                MessageBox.Show("There are no renames or renames pending.", "Nothing to do", MessageBoxButtons.OK);
                return;
            }

            string msg = "";
            _UserCancel = false;
            if (delete > 0 && !_UserCancel)
            {
                ProgressBar.Value = 0;
                ProgressBar.Maximum = delete;
                string deleteNoun = delete > 1 ? "deletes" : "delete";
                string pronoun = delete > 1 ? "those deletes" : "that delete";
                msg = $"You have {delete:N0} {deleteNoun} pending, do you want to do {pronoun}?";
                if (MessageBox.Show(msg, $"Proceed with {deleteNoun}?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!Visible) Show();
                    DoDeletes(instructions.Where(inst => inst.Verb == FileInstructionVerb.Delete && !inst.Completed));
                }
            }

            if (rename > 0 && !_UserCancel)
            {
                ProgressBar.Value = 0;
                ProgressBar.Maximum = rename;
                string renameNoun = rename > 1 ? "renames" : "rename";
                string pronoun = rename > 1 ? "those renames" : "that rename";
                msg = $"You have {rename:N0} {renameNoun} pending, do you want to do {pronoun}?";
                if (MessageBox.Show(msg, $"Proceed with {renameNoun}?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (!Visible) Show();
                    DoRenames(instructions.Where(inst => inst.Verb == FileInstructionVerb.Rename && !inst.Completed));
                }
            }

            if (_UserCancel)
            {
                TextStatus.Text += "*** CANCELED BY USER, NOT ALL OPERATIONS WERE COMPLETED ***\r\n";
            }
        }
        private void DoDeletes(IEnumerable<FileInstruction> deletes)
        {
            string spacing = TextStatus.Text.Length > 0 ? "\r\n\r\n" : "";
            TextStatus.Text += $"{spacing}DELETING\r\n";
            foreach (FileInstruction delete in deletes)
            {
                ProgressBar.Value++;
                try
                {
                    FileActions.DoInstruction(delete);
                    TextStatus.Text += $"  {delete.FileName}\r\n";
                    delete.Completed = true;
                }
                catch (Exception ex)
                {
                    LogError($"  *** ERROR {delete.FileName}\r\n{ex.Message}");
                }
                TextStatus.SelectionStart = TextStatus.Text.Length;
                TextStatus.ScrollToCaret();
                ProgressBar.Value = 0;
            }
        }
        private void LogError(string msg)
        {
            if (!TextErrors.Visible)
            {
                TextErrors.Visible = true;
                DoLayout();
            }
            TextErrors.Text += $"{msg}\r\n";
        }
        private void DoRenames(IEnumerable<FileInstruction> renames)
        {
            string spacing = TextStatus.Text.Length > 0 ? "\r\n\r\n" : "";
            TextStatus.Text += $"{spacing}RENAMING\r\n";
            foreach (FileInstruction rename in renames)
            {
                ProgressBar.Value++;
                try
                {
                    string newName = FileActions.DoInstruction(rename);
                    TextStatus.Text += $"  {rename.FileName} =>\r\n        {newName}\r\n";
                    rename.Completed = true;
                }
                catch (Exception ex)
                {
                    LogError($"  *** ERROR {rename.FileName}\r\nrenaming to {rename.NewFileName} (possibly with added serial number)\r\n{ex.Message}");
                }
                TextStatus.SelectionStart = TextStatus.Text.Length;
                TextStatus.ScrollToCaret();
                ProgressBar.Value = 0;
            }
        }

        private void ActionUI_Resize(object sender, EventArgs e) { DoLayout(); }
        private void DoLayout()
        {
            TextStatus.Location = new Point(0, 0);
            TextStatus.Width = TextStatus.Parent != null ? TextStatus.Parent.ClientRectangle.Width : ClientRectangle.Width;
            if (TextErrors.Visible)
            {
                TextStatus.Height = TextStatus.Parent != null ? TextStatus.Parent.ClientRectangle.Height / 20 * 15 : ClientRectangle.Height / 20 * 15;
                TextErrors.Location = new Point(0, TextStatus.Bottom);
                TextErrors.Width = TextStatus.Width;
                TextErrors.Height = TextStatus.Height / 3;

            }
            else
                TextStatus.Height = TextStatus.Parent != null ? TextStatus.Parent.ClientRectangle.Height : ClientRectangle.Height;
        }
    }
}
