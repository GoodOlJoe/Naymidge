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
    public partial class SmartFile : Form
    {
        private readonly List<FileInstruction> _Instructions;
        private int CurrentItem = 0;

        public SmartFile(ProcessingScope scope)
        {
            // _Scope.Contents has the original files to be acted upon, as selected by the caller
            // _Instructions contains the decisions made by the user of this form: to delete or rename, etc.
            _Instructions = new List<FileInstruction>(scope.Contents.Count);
            foreach (string fqn in scope.Contents)
                _Instructions.Add(new FileInstruction(fqn));

            CurrentItem = 0;

            InitializeComponent();

            cmdProceed.Click += CmdProceed_Click;
            cmdCancel.Click += CmdCancel_Click;

        }
        private void CmdCancel_Click(object? sender, EventArgs e) { DoCancelButtonClicked(); }
        private void CmdProceed_Click(object? sender, EventArgs e) { DoProceedButtonClicked(); }
        private void DoProceedButtonClicked()
        {
            ActionUI ui = new();
            ui.ProcessFileInstructions(_Instructions);
        }
        private void DoCancelButtonClicked() { Close(); }
        private void UpdateUIEnablement()
        {
            // here we need to update the proceed button: if there is at least one file to move,
            // and if a target has been specified
            cmdProceed.Enabled = _Contents.Count > 0;
        }

    }
}
