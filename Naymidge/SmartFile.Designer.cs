namespace Naymidge
{
    partial class SmartFile
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
            label4 = new Label();
            cmdProceed = new Button();
            cmdCancel = new Button();
            textBox1 = new TextBox();
            CmdPickContentDirectory = new Button();
            TimerUIRefresh = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // FileByDateCheckBox
            // 
            FileByDateCheckBox.AutoSize = true;
            FileByDateCheckBox.Location = new Point(79, 102);
            FileByDateCheckBox.Margin = new Padding(4);
            FileByDateCheckBox.Name = "FileByDateCheckBox";
            FileByDateCheckBox.Size = new Size(418, 25);
            FileByDateCheckBox.TabIndex = 0;
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(79, 147);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(370, 21);
            label4.TabIndex = 4;
            label4.Text = "{file by date example, dependent on checkbox value}";
            // 
            // cmdProceed
            // 
            cmdProceed.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdProceed.Location = new Point(439, 203);
            cmdProceed.Margin = new Padding(4);
            cmdProceed.Name = "cmdProceed";
            cmdProceed.Size = new Size(96, 32);
            cmdProceed.TabIndex = 6;
            cmdProceed.Text = "&Proceed";
            cmdProceed.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdCancel.Location = new Point(543, 203);
            cmdCancel.Margin = new Padding(4);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(96, 32);
            cmdCancel.TabIndex = 5;
            cmdCancel.Text = "&Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(79, 66);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(502, 29);
            textBox1.TabIndex = 7;
            // 
            // CmdPickContentDirectory
            // 
            CmdPickContentDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CmdPickContentDirectory.Font = new Font("Wingdings", 12F, FontStyle.Regular, GraphicsUnit.Point, 2);
            CmdPickContentDirectory.Location = new Point(590, 68);
            CmdPickContentDirectory.Margin = new Padding(4, 5, 4, 5);
            CmdPickContentDirectory.Name = "CmdPickContentDirectory";
            CmdPickContentDirectory.Size = new Size(35, 26);
            CmdPickContentDirectory.TabIndex = 8;
            CmdPickContentDirectory.Text = "1";
            CmdPickContentDirectory.UseVisualStyleBackColor = true;
            // 
            // SmartFile
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(652, 248);
            Controls.Add(CmdPickContentDirectory);
            Controls.Add(textBox1);
            Controls.Add(cmdProceed);
            Controls.Add(cmdCancel);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(SourceCountLabel);
            Controls.Add(FileByDateCheckBox);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "SmartFile";
            Text = "Smart Filing";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox FileByDateCheckBox;
        private Label SourceCountLabel;
        private Label label2;
        private Label label4;
        private Button cmdProceed;
        private Button cmdCancel;
        private TextBox textBox1;
        private Button CmdPickContentDirectory;
        private System.Windows.Forms.Timer TimerUIRefresh;
    }
}