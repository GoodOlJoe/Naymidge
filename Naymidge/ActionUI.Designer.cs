namespace Naymidge
{
    partial class ActionUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            ProgressBar = new ToolStripProgressBar();
            cmdClose = new Button();
            cmdCancel = new Button();
            TextBoxPanel = new Panel();
            TextErrors = new TextBox();
            TextStatus = new TextBox();
            statusStrip1.SuspendLayout();
            TextBoxPanel.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { ProgressBar });
            statusStrip1.Location = new Point(0, 544);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 18, 0);
            statusStrip1.Size = new Size(933, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // ProgressBar
            // 
            ProgressBar.Name = "ProgressBar";
            ProgressBar.Size = new Size(100, 16);
            // 
            // cmdClose
            // 
            cmdClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdClose.Location = new Point(824, 498);
            cmdClose.Margin = new Padding(4);
            cmdClose.Name = "cmdClose";
            cmdClose.Size = new Size(96, 32);
            cmdClose.TabIndex = 1;
            cmdClose.Text = "&Close";
            cmdClose.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdCancel.Location = new Point(720, 498);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(96, 32);
            cmdCancel.TabIndex = 2;
            cmdCancel.Text = "Ca&ncel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += CmdCancel_Click;
            // 
            // TextBoxPanel
            // 
            TextBoxPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextBoxPanel.Controls.Add(TextErrors);
            TextBoxPanel.Controls.Add(TextStatus);
            TextBoxPanel.Location = new Point(8, 9);
            TextBoxPanel.Name = "TextBoxPanel";
            TextBoxPanel.Size = new Size(913, 482);
            TextBoxPanel.TabIndex = 4;
            // 
            // TextErrors
            // 
            TextErrors.Anchor = AnchorStyles.None;
            TextErrors.BackColor = Color.FromArgb(255, 192, 192);
            TextErrors.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextErrors.ForeColor = Color.FromArgb(192, 0, 0);
            TextErrors.Location = new Point(0, 319);
            TextErrors.Multiline = true;
            TextErrors.Name = "TextErrors";
            TextErrors.ReadOnly = true;
            TextErrors.ScrollBars = ScrollBars.Vertical;
            TextErrors.Size = new Size(913, 160);
            TextErrors.TabIndex = 5;
            TextErrors.Text = "";
            // 
            // TextStatus
            // 
            TextStatus.Anchor = AnchorStyles.None;
            TextStatus.BackColor = Color.FromArgb(255, 245, 227);
            TextStatus.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextStatus.Location = new Point(0, 0);
            TextStatus.Multiline = true;
            TextStatus.Name = "TextStatus";
            TextStatus.ReadOnly = true;
            TextStatus.ScrollBars = ScrollBars.Vertical;
            TextStatus.Size = new Size(913, 313);
            TextStatus.TabIndex = 4;
            // 
            // ActionUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 566);
            Controls.Add(TextBoxPanel);
            Controls.Add(cmdCancel);
            Controls.Add(cmdClose);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "ActionUI";
            Text = "Process File Changes";
            Load += ActionUI_Load;
            Resize += ActionUI_Resize;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            TextBoxPanel.ResumeLayout(false);
            TextBoxPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private Button cmdClose;
        private Button cmdCancel;
        private ToolStripProgressBar ProgressBar;
        private Panel TextBoxPanel;
        private TextBox TextErrors;
        private TextBox TextStatus;
    }
}