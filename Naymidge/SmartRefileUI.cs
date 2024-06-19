using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naymidge
{
    public partial class SmartRefileUI : Form
    {
        private readonly List<FileInstruction> _Instructions;
        private int CurrentItem = 0;

        public SmartRefileUI(ProcessingScope scope)
        {
            InitializeComponent();

            // _Scope.Contents has the original files to be acted upon, as selected by the caller
            // _Instructions contains the decisions made by the user of this form: to delete or rename, etc.
            _Instructions = new List<FileInstruction>(scope.Contents.Count);
            foreach (string fqn in scope.Contents)
                _Instructions.Add(new FileInstruction(fqn));

            CurrentItem = 0;
            SourceCountLabel.Text = $"Move {_Instructions.Count.ToString()} files";

            cmdProceed.Click += CmdProceed_Click;
            cmdCancel.Click += CmdCancel_Click;
            TimerUIRefresh.Tick += DoTimerUIRefresh_Tick;
            TimerUIRefresh.Enabled = true;
        }
        private void CmdCancel_Click(object? sender, EventArgs e) { DoCancelButtonClicked(); }
        private void CmdProceed_Click(object? sender, EventArgs e) { DoProceedButtonClicked(); }
        private void DoProceedButtonClicked()
        {
            if (TargetValid(TargetTextbox.Text.Trim()))
            {
                ActionUI ui = new();
                bool useDateTakenIfFilenameUndated = FileByDateCheckBox.Checked && UseDateTakenCheckBox.Checked;
                ui.DoSmartRefiling(_Instructions, TargetTextbox.Text.Trim(), FileByDateCheckBox.Checked, useDateTakenIfFilenameUndated);
            }
        }
        private void DoCancelButtonClicked() { Close(); }
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
        private bool TargetValid(string target)
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
