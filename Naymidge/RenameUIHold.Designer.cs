namespace Naymidge
{
    partial class RenameUIHold
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
            OuterContainer = new SplitContainer();
            InnerContainer = new SplitContainer();
            flyleafHostMain = new FlyleafLib.Controls.WinForms.FlyleafHost();
            flyleafHostNext = new FlyleafLib.Controls.WinForms.FlyleafHost();
            flyleafHostPrev = new FlyleafLib.Controls.WinForms.FlyleafHost();
            tvRecent = new TreeView();
            LowerPanel = new Panel();
            cmdClose = new Button();
            txtNameInput = new TextBox();
            UpperPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OuterContainer).BeginInit();
            OuterContainer.Panel1.SuspendLayout();
            OuterContainer.Panel2.SuspendLayout();
            OuterContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InnerContainer).BeginInit();
            InnerContainer.Panel1.SuspendLayout();
            InnerContainer.Panel2.SuspendLayout();
            InnerContainer.SuspendLayout();
            LowerPanel.SuspendLayout();
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
            UpperPanel.Size = new Size(1047, 115);
            UpperPanel.TabIndex = 4;
            // 
            // MediaDetailsLabel
            // 
            MediaDetailsLabel.AutoSize = true;
            MediaDetailsLabel.BackColor = Color.MistyRose;
            MediaDetailsLabel.Dock = DockStyle.Fill;
            MediaDetailsLabel.Location = new Point(0, 0);
            MediaDetailsLabel.Margin = new Padding(0);
            MediaDetailsLabel.Name = "MediaDetailsLabel";
            MediaDetailsLabel.Size = new Size(112, 21);
            MediaDetailsLabel.TabIndex = 0;
            MediaDetailsLabel.Text = "{media details}";
            // 
            // OuterContainer
            // 
            OuterContainer.Anchor = AnchorStyles.None;
            OuterContainer.BorderStyle = BorderStyle.FixedSingle;
            OuterContainer.Location = new Point(0, 144);
            OuterContainer.Margin = new Padding(4);
            OuterContainer.Name = "OuterContainer";
            // 
            // OuterContainer.Panel1
            // 
            OuterContainer.Panel1.Controls.Add(InnerContainer);
            // 
            // OuterContainer.Panel2
            // 
            OuterContainer.Panel2.Controls.Add(tvRecent);
            OuterContainer.Size = new Size(980, 329);
            OuterContainer.SplitterDistance = 722;
            OuterContainer.SplitterWidth = 5;
            OuterContainer.TabIndex = 5;
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
            InnerContainer.Panel2.Controls.Add(flyleafHostNext);
            InnerContainer.Panel2.Controls.Add(flyleafHostPrev);
            InnerContainer.Size = new Size(722, 329);
            InnerContainer.SplitterDistance = 218;
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
            flyleafHostMain.Size = new Size(720, 216);
            flyleafHostMain.SwapDragEnterOnShift = true;
            flyleafHostMain.SwapOnDrop = true;
            flyleafHostMain.TabIndex = 1;
            flyleafHostMain.ToggleFullScreenOnDoubleClick = true;
            // 
            // flyleafHostNext
            // 
            flyleafHostNext.AllowDrop = true;
            flyleafHostNext.BackColor = Color.Black;
            flyleafHostNext.Dock = DockStyle.Right;
            flyleafHostNext.DragMove = true;
            flyleafHostNext.IsFullScreen = false;
            flyleafHostNext.KeyBindings = true;
            flyleafHostNext.Location = new Point(584, 0);
            flyleafHostNext.Margin = new Padding(4);
            flyleafHostNext.Name = "flyleafHostNext";
            flyleafHostNext.OpenOnDrop = false;
            flyleafHostNext.PanMoveOnCtrl = true;
            flyleafHostNext.PanRotateOnShiftWheel = true;
            flyleafHostNext.PanZoomOnCtrlWheel = true;
            flyleafHostNext.Player = null;
            flyleafHostNext.Size = new Size(136, 103);
            flyleafHostNext.SwapDragEnterOnShift = true;
            flyleafHostNext.SwapOnDrop = true;
            flyleafHostNext.TabIndex = 1;
            flyleafHostNext.ToggleFullScreenOnDoubleClick = true;
            // 
            // flyleafHostPrev
            // 
            flyleafHostPrev.AllowDrop = true;
            flyleafHostPrev.BackColor = Color.Black;
            flyleafHostPrev.Dock = DockStyle.Left;
            flyleafHostPrev.DragMove = true;
            flyleafHostPrev.IsFullScreen = false;
            flyleafHostPrev.KeyBindings = true;
            flyleafHostPrev.Location = new Point(0, 0);
            flyleafHostPrev.Margin = new Padding(4);
            flyleafHostPrev.Name = "flyleafHostPrev";
            flyleafHostPrev.OpenOnDrop = false;
            flyleafHostPrev.PanMoveOnCtrl = true;
            flyleafHostPrev.PanRotateOnShiftWheel = true;
            flyleafHostPrev.PanZoomOnCtrlWheel = true;
            flyleafHostPrev.Player = null;
            flyleafHostPrev.Size = new Size(131, 103);
            flyleafHostPrev.SwapDragEnterOnShift = true;
            flyleafHostPrev.SwapOnDrop = true;
            flyleafHostPrev.TabIndex = 0;
            flyleafHostPrev.ToggleFullScreenOnDoubleClick = true;
            // 
            // tvRecent
            // 
            tvRecent.Dock = DockStyle.Fill;
            tvRecent.Location = new Point(0, 0);
            tvRecent.Margin = new Padding(4);
            tvRecent.Name = "tvRecent";
            tvRecent.Size = new Size(251, 327);
            tvRecent.TabIndex = 0;
            // 
            // LowerPanel
            // 
            LowerPanel.BackColor = Color.Khaki;
            LowerPanel.Controls.Add(cmdClose);
            LowerPanel.Controls.Add(txtNameInput);
            LowerPanel.Dock = DockStyle.Bottom;
            LowerPanel.Location = new Point(0, 554);
            LowerPanel.Margin = new Padding(4);
            LowerPanel.Name = "LowerPanel";
            LowerPanel.Size = new Size(1047, 67);
            LowerPanel.TabIndex = 6;
            // 
            // cmdClose
            // 
            cmdClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdClose.Location = new Point(921, 16);
            cmdClose.Name = "cmdClose";
            cmdClose.Size = new Size(117, 35);
            cmdClose.TabIndex = 1;
            cmdClose.Text = "&Close";
            cmdClose.UseVisualStyleBackColor = true;
            cmdClose.Click += CmdClose_Click;
            // 
            // txtNameInput
            // 
            txtNameInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNameInput.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNameInput.Location = new Point(7, 9);
            txtNameInput.Name = "txtNameInput";
            txtNameInput.Size = new Size(709, 46);
            txtNameInput.TabIndex = 0;
            txtNameInput.KeyUp += TxtNameInput_KeyUp;
            // 
            // RenameUIHold
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1047, 621);
            Controls.Add(UpperPanel);
            Controls.Add(OuterContainer);
            Controls.Add(LowerPanel);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KeyPreview = true;
            Margin = new Padding(4);
            Name = "RenameUIHold";
            Text = "Rename";
            Load += RenameUI_Load;
            KeyUp += RenameUI_KeyUp;
            Resize += RenameUI_Resize;
            UpperPanel.ResumeLayout(false);
            UpperPanel.PerformLayout();
            OuterContainer.Panel1.ResumeLayout(false);
            OuterContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)OuterContainer).EndInit();
            OuterContainer.ResumeLayout(false);
            InnerContainer.Panel1.ResumeLayout(false);
            InnerContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)InnerContainer).EndInit();
            InnerContainer.ResumeLayout(false);
            LowerPanel.ResumeLayout(false);
            LowerPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel UpperPanel;
        private SplitContainer OuterContainer;
        private SplitContainer InnerContainer;
        private FlyleafLib.Controls.WinForms.FlyleafHost flyleafHostMain;
        private Panel LowerPanel;
        private FlyleafLib.Controls.WinForms.FlyleafHost flyleafHostNext;
        private FlyleafLib.Controls.WinForms.FlyleafHost flyleafHostPrev;
        private TreeView tvRecent;
        private Label MediaDetailsLabel;
        private Button cmdClose;
        private TextBox txtNameInput;
    }
}
