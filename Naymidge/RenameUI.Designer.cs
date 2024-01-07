namespace Naymidge
{
    partial class RenameUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UpperPanel = new Panel();
            MediaDetailsLabel = new Label();
            LowerPanel = new Panel();
            KeyboardShortcutsLabel = new Label();
            cmdProceed = new Button();
            ProgressLabel = new Label();
            cmdCancel = new Button();
            txtNameInput = new TextBox();
            MiddlePanel = new Panel();
            OuterContainer = new SplitContainer();
            InnerContainer = new SplitContainer();
            flyleafHostMain = new FlyleafLib.Controls.WinForms.FlyleafHost();
            BackDetailsLabel = new Label();
            TvAllBacks = new TreeView();
            PicboxBack = new PictureBox();
            TxtRecent = new TextBox();
            UpperPanel.SuspendLayout();
            LowerPanel.SuspendLayout();
            MiddlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OuterContainer).BeginInit();
            OuterContainer.Panel1.SuspendLayout();
            OuterContainer.Panel2.SuspendLayout();
            OuterContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InnerContainer).BeginInit();
            InnerContainer.Panel1.SuspendLayout();
            InnerContainer.Panel2.SuspendLayout();
            InnerContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicboxBack).BeginInit();
            SuspendLayout();
            // 
            // UpperPanel
            // 
            UpperPanel.BackColor = Color.RosyBrown;
            UpperPanel.Controls.Add(MediaDetailsLabel);
            UpperPanel.Dock = DockStyle.Top;
            UpperPanel.Location = new Point(0, 0);
            UpperPanel.Margin = new Padding(4);
            UpperPanel.Name = "UpperPanel";
            UpperPanel.Size = new Size(1047, 65);
            UpperPanel.TabIndex = 4;
            // 
            // MediaDetailsLabel
            // 
            MediaDetailsLabel.AutoSize = true;
            MediaDetailsLabel.BackColor = Color.MistyRose;
            MediaDetailsLabel.Location = new Point(9, 9);
            MediaDetailsLabel.Margin = new Padding(0);
            MediaDetailsLabel.Name = "MediaDetailsLabel";
            MediaDetailsLabel.Size = new Size(112, 21);
            MediaDetailsLabel.TabIndex = 0;
            MediaDetailsLabel.Text = "{media details}";
            // 
            // LowerPanel
            // 
            LowerPanel.BackColor = Color.Khaki;
            LowerPanel.Controls.Add(KeyboardShortcutsLabel);
            LowerPanel.Controls.Add(cmdProceed);
            LowerPanel.Controls.Add(ProgressLabel);
            LowerPanel.Controls.Add(cmdCancel);
            LowerPanel.Controls.Add(txtNameInput);
            LowerPanel.Dock = DockStyle.Bottom;
            LowerPanel.Location = new Point(0, 500);
            LowerPanel.Margin = new Padding(4);
            LowerPanel.Name = "LowerPanel";
            LowerPanel.Size = new Size(1047, 121);
            LowerPanel.TabIndex = 6;
            // 
            // KeyboardShortcutsLabel
            // 
            KeyboardShortcutsLabel.AutoSize = true;
            KeyboardShortcutsLabel.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KeyboardShortcutsLabel.Location = new Point(9, 58);
            KeyboardShortcutsLabel.Name = "KeyboardShortcutsLabel";
            KeyboardShortcutsLabel.Size = new Size(147, 15);
            KeyboardShortcutsLabel.TabIndex = 4;
            KeyboardShortcutsLabel.Text = "{keyboard shortcuts}";
            // 
            // cmdProceed
            // 
            cmdProceed.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdProceed.Location = new Point(918, 29);
            cmdProceed.Name = "cmdProceed";
            cmdProceed.Size = new Size(117, 35);
            cmdProceed.TabIndex = 3;
            cmdProceed.Text = "&Proceed";
            cmdProceed.UseVisualStyleBackColor = true;
            // 
            // ProgressLabel
            // 
            ProgressLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ProgressLabel.AutoSize = true;
            ProgressLabel.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ProgressLabel.Location = new Point(769, 9);
            ProgressLabel.Name = "ProgressLabel";
            ProgressLabel.Size = new Size(70, 15);
            ProgressLabel.TabIndex = 2;
            ProgressLabel.Text = "{nnn/nnn}";
            // 
            // cmdCancel
            // 
            cmdCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdCancel.Location = new Point(917, 74);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(117, 35);
            cmdCancel.TabIndex = 1;
            cmdCancel.Text = "&Cancel";
            cmdCancel.UseVisualStyleBackColor = true;
            cmdCancel.Click += CmdCancel_Click;
            // 
            // txtNameInput
            // 
            txtNameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNameInput.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNameInput.Location = new Point(7, 9);
            txtNameInput.Name = "txtNameInput";
            txtNameInput.Size = new Size(756, 46);
            txtNameInput.TabIndex = 0;
            txtNameInput.KeyUp += NameInput_KeyUp;
            // 
            // MiddlePanel
            // 
            MiddlePanel.BackColor = Color.Aquamarine;
            MiddlePanel.Controls.Add(OuterContainer);
            MiddlePanel.Location = new Point(4, 72);
            MiddlePanel.Name = "MiddlePanel";
            MiddlePanel.Size = new Size(1031, 421);
            MiddlePanel.TabIndex = 7;
            // 
            // OuterContainer
            // 
            OuterContainer.BackColor = Color.FromArgb(240, 152, 32);
            OuterContainer.BorderStyle = BorderStyle.FixedSingle;
            OuterContainer.Dock = DockStyle.Fill;
            OuterContainer.Location = new Point(0, 0);
            OuterContainer.Margin = new Padding(4);
            OuterContainer.Name = "OuterContainer";
            // 
            // OuterContainer.Panel1
            // 
            OuterContainer.Panel1.Controls.Add(InnerContainer);
            // 
            // OuterContainer.Panel2
            // 
            OuterContainer.Panel2.BackColor = Color.Pink;
            OuterContainer.Panel2.Controls.Add(TxtRecent);
            OuterContainer.Size = new Size(1031, 421);
            OuterContainer.SplitterDistance = 759;
            OuterContainer.SplitterWidth = 5;
            OuterContainer.TabIndex = 6;
            // 
            // InnerContainer
            // 
            InnerContainer.BorderStyle = BorderStyle.FixedSingle;
            InnerContainer.Dock = DockStyle.Fill;
            InnerContainer.Location = new Point(0, 0);
            InnerContainer.Margin = new Padding(4);
            InnerContainer.Name = "InnerContainer";
            InnerContainer.Orientation = Orientation.Horizontal;
            // 
            // InnerContainer.Panel1
            // 
            InnerContainer.Panel1.Controls.Add(flyleafHostMain);
            // 
            // InnerContainer.Panel2
            // 
            InnerContainer.Panel2.BackColor = Color.FromArgb(128, 128, 255);
            InnerContainer.Panel2.Controls.Add(BackDetailsLabel);
            InnerContainer.Panel2.Controls.Add(TvAllBacks);
            InnerContainer.Panel2.Controls.Add(PicboxBack);
            InnerContainer.Size = new Size(759, 421);
            InnerContainer.SplitterDistance = 278;
            InnerContainer.SplitterWidth = 6;
            InnerContainer.TabIndex = 0;
            // 
            // flyleafHostMain
            // 
            flyleafHostMain.AllowDrop = true;
            flyleafHostMain.BackColor = Color.Black;
            flyleafHostMain.Dock = DockStyle.Fill;
            flyleafHostMain.DragMove = true;
            flyleafHostMain.IsFullScreen = false;
            flyleafHostMain.KeyBindings = true;
            flyleafHostMain.Location = new Point(0, 0);
            flyleafHostMain.Margin = new Padding(4);
            flyleafHostMain.Name = "flyleafHostMain";
            flyleafHostMain.OpenOnDrop = false;
            flyleafHostMain.PanMoveOnCtrl = true;
            flyleafHostMain.PanRotateOnShiftWheel = true;
            flyleafHostMain.PanZoomOnCtrlWheel = true;
            flyleafHostMain.Player = null;
            flyleafHostMain.Size = new Size(757, 276);
            flyleafHostMain.SwapDragEnterOnShift = true;
            flyleafHostMain.SwapOnDrop = true;
            flyleafHostMain.TabIndex = 1;
            flyleafHostMain.ToggleFullScreenOnDoubleClick = true;
            // 
            // BackDetailsLabel
            // 
            BackDetailsLabel.Anchor = AnchorStyles.None;
            BackDetailsLabel.AutoSize = true;
            BackDetailsLabel.BackColor = Color.Transparent;
            BackDetailsLabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BackDetailsLabel.Location = new Point(603, 47);
            BackDetailsLabel.Margin = new Padding(0);
            BackDetailsLabel.Name = "BackDetailsLabel";
            BackDetailsLabel.Size = new Size(74, 13);
            BackDetailsLabel.TabIndex = 2;
            BackDetailsLabel.Text = "{back details}";
            // 
            // TvAllBacks
            // 
            TvAllBacks.Dock = DockStyle.Left;
            TvAllBacks.Location = new Point(0, 0);
            TvAllBacks.Name = "TvAllBacks";
            TvAllBacks.ShowNodeToolTips = true;
            TvAllBacks.Size = new Size(287, 135);
            TvAllBacks.TabIndex = 1;
            TvAllBacks.AfterSelect += TvAllBacks_AfterSelect;
            // 
            // PicboxBack
            // 
            PicboxBack.Dock = DockStyle.Right;
            PicboxBack.Location = new Point(424, 0);
            PicboxBack.Name = "PicboxBack";
            PicboxBack.Size = new Size(333, 135);
            PicboxBack.SizeMode = PictureBoxSizeMode.Zoom;
            PicboxBack.TabIndex = 0;
            PicboxBack.TabStop = false;
            // 
            // TxtRecent
            // 
            TxtRecent.Dock = DockStyle.Fill;
            TxtRecent.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TxtRecent.Location = new Point(0, 0);
            TxtRecent.Multiline = true;
            TxtRecent.Name = "TxtRecent";
            TxtRecent.Size = new Size(265, 419);
            TxtRecent.TabIndex = 0;
            // 
            // RenameUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Fuchsia;
            ClientSize = new Size(1047, 621);
            Controls.Add(MiddlePanel);
            Controls.Add(UpperPanel);
            Controls.Add(LowerPanel);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KeyPreview = true;
            Margin = new Padding(4);
            Name = "RenameUI";
            Text = "Rename";
            Load += RenameUI_Load;
            //KeyDown += RenameUI_KeyDown;
            KeyUp += RenameUI_KeyUp;
            Resize += RenameUI_Resize;
            UpperPanel.ResumeLayout(false);
            UpperPanel.PerformLayout();
            LowerPanel.ResumeLayout(false);
            LowerPanel.PerformLayout();
            MiddlePanel.ResumeLayout(false);
            OuterContainer.Panel1.ResumeLayout(false);
            OuterContainer.Panel2.ResumeLayout(false);
            OuterContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OuterContainer).EndInit();
            OuterContainer.ResumeLayout(false);
            InnerContainer.Panel1.ResumeLayout(false);
            InnerContainer.Panel2.ResumeLayout(false);
            InnerContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InnerContainer).EndInit();
            InnerContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PicboxBack).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel UpperPanel;
        private Panel LowerPanel;
        private Label MediaDetailsLabel;
        private Button cmdCancel;
        private TextBox txtNameInput;
        private Label ProgressLabel;
        private Panel MiddlePanel;
        private SplitContainer OuterContainer;
        private SplitContainer InnerContainer;
        private FlyleafLib.Controls.WinForms.FlyleafHost flyleafHostMain;
        private Label BackDetailsLabel;
        private TreeView TvAllBacks;
        private PictureBox PicboxBack;
        private TextBox TxtRecent;
        private Button cmdProceed;
        private Label KeyboardShortcutsLabel;
    }
}
