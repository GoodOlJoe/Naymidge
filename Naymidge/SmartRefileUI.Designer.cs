namespace Naymidge
{
    partial class SmartRefileUI
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
            components = new System.ComponentModel.Container();
            FileByDateCheckBox = new CheckBox();
            SourceCountLabel = new Label();
            label2 = new Label();
            TargetExampleLabel = new Label();
            cmdProceed = new Button();
            cmdCancel = new Button();
            TargetTextbox = new TextBox();
            CmdPickContentDirectory = new Button();
            TimerUIRefresh = new System.Windows.Forms.Timer(components);
            UseDateTakenCheckBox = new CheckBox();
            TargetDirectoryDialog = new SaveFileDialog();
            SuspendLayout();
            // 
            // FileByDateCheckBox
            // 
            FileByDateCheckBox.AutoSize = true;
            FileByDateCheckBox.Location = new Point(79, 102);
            FileByDateCheckBox.Margin = new Padding(4);
            FileByDateCheckBox.Name = "FileByDateCheckBox";
            FileByDateCheckBox.Size = new Size(418, 25);
            FileByDateCheckBox.TabIndex = 2;
            FileByDateCheckBox.Text = "File By Date (move to date/year directories under target)";
            FileByDateCheckBox.UseVisualStyleBackColor = true;
            // 
            // SourceCountLabel
            // 
            SourceCountLabel.AutoSize = true;
            SourceCountLabel.Location = new Point(17, 30);
            SourceCountLabel.Margin = new Padding(4, 0, 4, 0);
            SourceCountLabel.Name = "SourceCountLabel";
            SourceCountLabel.Size = new Size(134, 21);
            SourceCountLabel.TabIndex = 1;
            SourceCountLabel.Text = "{source file count}";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 69);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(55, 21);
            label2.TabIndex = 2;
            label2.Text = "Target:";
            // 
            // TargetExampleLabel
            // 
            TargetExampleLabel.AutoSize = true;
            TargetExampleLabel.Location = new Point(21, 175);
            TargetExampleLabel.Margin = new Padding(4, 0, 4, 0);
            TargetExampleLabel.Name = "TargetExampleLabel";
            TargetExampleLabel.Size = new Size(370, 21);
            TargetExampleLabel.TabIndex = 4;
            TargetExampleLabel.Text = "{file by date example, dependent on checkbox value}";
            // 
            // cmdProceed
            // 
            cmdProceed.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdProceed.Location = new Point(439, 245);
            cmdProceed.Margin = new Padding(4);
            cmdProceed.Name = "cmdProceed";
            cmdProceed.Size = new Size(96, 32);
            cmdProceed.TabIndex = 4;
            cmdProceed.Text = "&Proceed";
            cmdProceed.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdCancel.Location = new Point(543, 245);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(96, 32);
            cmdCancel.TabIndex = 5;
            cmdCancel.Text = "&Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            // 
            // TargetTextbox
            // 
            TargetTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TargetTextbox.Location = new Point(79, 66);
            TargetTextbox.Name = "TargetTextbox";
            TargetTextbox.Size = new Size(502, 29);
            TargetTextbox.TabIndex = 0;
            // 
            // CmdPickContentDirectory
            // 
            CmdPickContentDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CmdPickContentDirectory.Font = new Font("Wingdings", 12F, FontStyle.Regular, GraphicsUnit.Point, 2);
            CmdPickContentDirectory.Location = new Point(590, 68);
            CmdPickContentDirectory.Margin = new Padding(4, 5, 4, 5);
            CmdPickContentDirectory.Name = "CmdPickContentDirectory";
            CmdPickContentDirectory.Size = new Size(35, 26);
            CmdPickContentDirectory.TabIndex = 1;
            CmdPickContentDirectory.Text = "1";
            CmdPickContentDirectory.UseVisualStyleBackColor = true;
            CmdPickContentDirectory.Click += CmdPickContentDirectory_Click;
            // 
            // UseDateTakenCheckBox
            // 
            UseDateTakenCheckBox.AutoSize = true;
            UseDateTakenCheckBox.Location = new Point(99, 135);
            UseDateTakenCheckBox.Margin = new Padding(4);
            UseDateTakenCheckBox.Name = "UseDateTakenCheckBox";
            UseDateTakenCheckBox.Size = new Size(292, 25);
            UseDateTakenCheckBox.TabIndex = 3;
            UseDateTakenCheckBox.Text = "Use 'Date Taken' if no date in filename";
            UseDateTakenCheckBox.UseVisualStyleBackColor = true;
            // 
            // SmartRefileUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(652, 290);
            Controls.Add(UseDateTakenCheckBox);
            Controls.Add(CmdPickContentDirectory);
            Controls.Add(TargetTextbox);
            Controls.Add(cmdProceed);
            Controls.Add(cmdCancel);
            Controls.Add(TargetExampleLabel);
            Controls.Add(label2);
            Controls.Add(SourceCountLabel);
            Controls.Add(FileByDateCheckBox);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "SmartRefileUI";
            Text = "Smart Re-filing";
            Load += SmartRefileUI_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox FileByDateCheckBox;
        private Label SourceCountLabel;
        private Label label2;
        private Label TargetExampleLabel;
        private Button cmdProceed;
        private Button cmdCancel;
        private TextBox TargetTextbox;
        private Button CmdPickContentDirectory;
        private System.Windows.Forms.Timer TimerUIRefresh;
        private CheckBox UseDateTakenCheckBox;
        private SaveFileDialog TargetDirectoryDialog;
    }
}