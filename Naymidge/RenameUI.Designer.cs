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
            OuterContainer = new SplitContainer();
            InnerContainer = new SplitContainer();
            flyleafHostMain = new FlyleafLib.Controls.WinForms.FlyleafHost();
            flyleafHostNext = new FlyleafLib.Controls.WinForms.FlyleafHost();
            flyleafHostPrev = new FlyleafLib.Controls.WinForms.FlyleafHost();
            LowerPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)OuterContainer).BeginInit();
            OuterContainer.Panel1.SuspendLayout();
            OuterContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InnerContainer).BeginInit();
            InnerContainer.Panel1.SuspendLayout();
            InnerContainer.Panel2.SuspendLayout();
            InnerContainer.SuspendLayout();
            SuspendLayout();
            // 
            // UpperPanel
            // 
            UpperPanel.BackColor = Color.RosyBrown;
            UpperPanel.Dock = DockStyle.Top;
            UpperPanel.Location = new Point(0, 0);
            UpperPanel.Name = "UpperPanel";
            UpperPanel.Size = new Size(1173, 82);
            UpperPanel.TabIndex = 4;
            // 
            // OuterContainer
            // 
            OuterContainer.Anchor = AnchorStyles.None;
            OuterContainer.BorderStyle = BorderStyle.FixedSingle;
            OuterContainer.Location = new Point(286, 104);
            OuterContainer.Name = "OuterContainer";
            // 
            // OuterContainer.Panel1
            // 
            OuterContainer.Panel1.Controls.Add(InnerContainer);
            OuterContainer.Size = new Size(601, 397);
            OuterContainer.SplitterDistance = 368;
            OuterContainer.TabIndex = 5;
            // 
            // InnerContainer
            // 
            InnerContainer.BorderStyle = BorderStyle.FixedSingle;
            InnerContainer.Dock = DockStyle.Fill;
            InnerContainer.Location = new Point(0, 0);
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
            InnerContainer.Size = new Size(368, 397);
            InnerContainer.SplitterDistance = 145;
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
            flyleafHostMain.Name = "flyleafHostMain";
            flyleafHostMain.OpenOnDrop = false;
            flyleafHostMain.PanMoveOnCtrl = true;
            flyleafHostMain.PanRotateOnShiftWheel = true;
            flyleafHostMain.PanZoomOnCtrlWheel = true;
            flyleafHostMain.Player = null;
            flyleafHostMain.Size = new Size(366, 143);
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
            flyleafHostNext.Location = new Point(260, 0);
            flyleafHostNext.Name = "flyleafHostNext";
            flyleafHostNext.OpenOnDrop = false;
            flyleafHostNext.PanMoveOnCtrl = true;
            flyleafHostNext.PanRotateOnShiftWheel = true;
            flyleafHostNext.PanZoomOnCtrlWheel = true;
            flyleafHostNext.Player = null;
            flyleafHostNext.Size = new Size(106, 246);
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
            flyleafHostPrev.Name = "flyleafHostPrev";
            flyleafHostPrev.OpenOnDrop = false;
            flyleafHostPrev.PanMoveOnCtrl = true;
            flyleafHostPrev.PanRotateOnShiftWheel = true;
            flyleafHostPrev.PanZoomOnCtrlWheel = true;
            flyleafHostPrev.Player = null;
            flyleafHostPrev.Size = new Size(102, 246);
            flyleafHostPrev.SwapDragEnterOnShift = true;
            flyleafHostPrev.SwapOnDrop = true;
            flyleafHostPrev.TabIndex = 0;
            flyleafHostPrev.ToggleFullScreenOnDoubleClick = true;
            // 
            // LowerPanel
            // 
            LowerPanel.BackColor = Color.Khaki;
            LowerPanel.Dock = DockStyle.Bottom;
            LowerPanel.Location = new Point(0, 522);
            LowerPanel.Name = "LowerPanel";
            LowerPanel.Size = new Size(1173, 122);
            LowerPanel.TabIndex = 6;
            // 
            // RenameUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1173, 644);
            Controls.Add(UpperPanel);
            Controls.Add(OuterContainer);
            Controls.Add(LowerPanel);
            Name = "RenameUI";
            Text = "Rename";
            Load += RenameUI_Load;
            Resize += RenameUI_Resize;
            OuterContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)OuterContainer).EndInit();
            OuterContainer.ResumeLayout(false);
            InnerContainer.Panel1.ResumeLayout(false);
            InnerContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)InnerContainer).EndInit();
            InnerContainer.ResumeLayout(false);
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
    }
}
