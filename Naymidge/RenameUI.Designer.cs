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
            flyleafHost = new FlyleafLib.Controls.WinForms.FlyleafHost();
            SuspendLayout();
            // 
            // flyleafHost
            // 
            flyleafHost.AllowDrop = true;
            flyleafHost.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flyleafHost.BackColor = Color.Black;
            flyleafHost.DragMove = true;
            flyleafHost.IsFullScreen = false;
            flyleafHost.KeyBindings = true;
            flyleafHost.Location = new Point(6, 6);
            flyleafHost.Name = "flyleafHost";
            flyleafHost.OpenOnDrop = false;
            flyleafHost.PanMoveOnCtrl = true;
            flyleafHost.PanRotateOnShiftWheel = true;
            flyleafHost.PanZoomOnCtrlWheel = true;
            flyleafHost.Player = null;
            flyleafHost.Size = new Size(790, 440);
            flyleafHost.SwapDragEnterOnShift = true;
            flyleafHost.SwapOnDrop = true;
            flyleafHost.TabIndex = 0;
            flyleafHost.ToggleFullScreenOnDoubleClick = true;
            // 
            // RenameUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(flyleafHost);
            Name = "RenameUI";
            Text = "Rename";
            ResumeLayout(false);
        }

        #endregion

        private FlyleafLib.Controls.WinForms.FlyleafHost flyleafHost;
    }
}
