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
            TextStatus = new TextBox();
            statusStrip1.SuspendLayout();
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
            // TextStatus
            // 
            TextStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextStatus.BackColor = Color.FromArgb(255, 245, 227);
            TextStatus.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextStatus.Location = new Point(8, 9);
            TextStatus.Multiline = true;
            TextStatus.Name = "TextStatus";
            TextStatus.ReadOnly = true;
            TextStatus.Size = new Size(913, 482);
            TextStatus.TabIndex = 3;
            // 
            // ActionUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 566);
            Controls.Add(TextStatus);
            Controls.Add(cmdCancel);
            Controls.Add(cmdClose);
            Controls.Add(statusStrip1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "ActionUI";
            Text = "Process File Changes";
            Load += ActionUI_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private Button cmdClose;
        private Button cmdCancel;
        private ToolStripProgressBar ProgressBar;
        private TextBox TextStatus;
    }
}