﻿namespace Naymidge
{
    partial class GetParameters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetParameters));
            InputGroup = new GroupBox();
            CmdPickContentDirectory = new Button();
            txtContentDirectoryStatus = new TextBox();
            CheckboxIncludeSubdirectories = new CheckBox();
            label1 = new Label();
            txtContentDirectory = new TextBox();
            RefineGroup = new GroupBox();
            IncludedFilesLabel = new Label();
            NotIncludedFilesLabel = new Label();
            PatternsLabel = new Label();
            tvIncluded = new TreeView();
            tvNotIncluded = new TreeView();
            txtPatterns = new TextBox();
            PicSelectionStatus = new PictureBox();
            ProcessGroup = new GroupBox();
            CheckboxSuggestDatestamp = new CheckBox();
            label2 = new Label();
            CmdRefile = new Button();
            CmdRename = new Button();
            cmdClose = new Button();
            TimerUIRefresh = new System.Windows.Forms.Timer(components);
            TimerSelectionRefresh = new System.Windows.Forms.Timer(components);
            ContentDirectoryDialog = new SaveFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SelectionStatusImageList = new ImageList(components);
            ContextMenuForTreeView = new ContextMenuStrip(components);
            CopyTextMenuItem = new ToolStripMenuItem();
            InputGroup.SuspendLayout();
            RefineGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PicSelectionStatus).BeginInit();
            ProcessGroup.SuspendLayout();
            ContextMenuForTreeView.SuspendLayout();
            SuspendLayout();
            // 
            // InputGroup
            // 
            InputGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            InputGroup.Controls.Add(CmdPickContentDirectory);
            InputGroup.Controls.Add(txtContentDirectoryStatus);
            InputGroup.Controls.Add(CheckboxIncludeSubdirectories);
            InputGroup.Controls.Add(label1);
            InputGroup.Controls.Add(txtContentDirectory);
            InputGroup.Location = new Point(22, 21);
            InputGroup.Name = "InputGroup";
            InputGroup.Size = new Size(1091, 137);
            InputGroup.TabIndex = 0;
            InputGroup.TabStop = false;
            InputGroup.Text = "Content Directory";
            // 
            // CmdPickContentDirectory
            // 
            CmdPickContentDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CmdPickContentDirectory.Font = new Font("Wingdings", 12F, FontStyle.Regular, GraphicsUnit.Point, 2);
            CmdPickContentDirectory.Location = new Point(1026, 26);
            CmdPickContentDirectory.Margin = new Padding(4, 5, 4, 5);
            CmdPickContentDirectory.Name = "CmdPickContentDirectory";
            CmdPickContentDirectory.Size = new Size(35, 26);
            CmdPickContentDirectory.TabIndex = 4;
            CmdPickContentDirectory.Text = "1";
            CmdPickContentDirectory.UseVisualStyleBackColor = true;
            CmdPickContentDirectory.Click += CmdPickContentDirectory_Click;
            // 
            // txtContentDirectoryStatus
            // 
            txtContentDirectoryStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtContentDirectoryStatus.BackColor = SystemColors.Control;
            txtContentDirectoryStatus.BorderStyle = BorderStyle.None;
            txtContentDirectoryStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContentDirectoryStatus.Location = new Point(231, 63);
            txtContentDirectoryStatus.Multiline = true;
            txtContentDirectoryStatus.Name = "txtContentDirectoryStatus";
            txtContentDirectoryStatus.ReadOnly = true;
            txtContentDirectoryStatus.Size = new Size(793, 56);
            txtContentDirectoryStatus.TabIndex = 3;
            txtContentDirectoryStatus.Tag = "Patterns not applied yet";
            // 
            // CheckboxIncludeSubdirectories
            // 
            CheckboxIncludeSubdirectories.AutoSize = true;
            CheckboxIncludeSubdirectories.Location = new Point(159, 60);
            CheckboxIncludeSubdirectories.Name = "CheckboxIncludeSubdirectories";
            CheckboxIncludeSubdirectories.Size = new Size(15, 14);
            CheckboxIncludeSubdirectories.TabIndex = 2;
            CheckboxIncludeSubdirectories.UseVisualStyleBackColor = true;
            CheckboxIncludeSubdirectories.CheckedChanged += Selection_Changed;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(27, 60);
            label1.Name = "label1";
            label1.Size = new Size(126, 13);
            label1.TabIndex = 1;
            label1.Text = "Include subdirectories?";
            // 
            // txtContentDirectory
            // 
            txtContentDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtContentDirectory.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContentDirectory.Location = new Point(30, 29);
            txtContentDirectory.Name = "txtContentDirectory";
            txtContentDirectory.Size = new Size(988, 25);
            txtContentDirectory.TabIndex = 0;
            txtContentDirectory.Text = "{last used output file}";
            txtContentDirectory.TextChanged += Selection_Changed;
            // 
            // RefineGroup
            // 
            RefineGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RefineGroup.Controls.Add(IncludedFilesLabel);
            RefineGroup.Controls.Add(NotIncludedFilesLabel);
            RefineGroup.Controls.Add(PatternsLabel);
            RefineGroup.Controls.Add(tvIncluded);
            RefineGroup.Controls.Add(tvNotIncluded);
            RefineGroup.Controls.Add(txtPatterns);
            RefineGroup.Controls.Add(PicSelectionStatus);
            RefineGroup.Location = new Point(22, 158);
            RefineGroup.Name = "RefineGroup";
            RefineGroup.Size = new Size(1091, 267);
            RefineGroup.TabIndex = 1;
            RefineGroup.TabStop = false;
            RefineGroup.Text = "Refine Content";
            // 
            // IncludedFilesLabel
            // 
            IncludedFilesLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            IncludedFilesLabel.AutoSize = true;
            IncludedFilesLabel.Location = new Point(668, 19);
            IncludedFilesLabel.Margin = new Padding(4, 0, 4, 0);
            IncludedFilesLabel.Name = "IncludedFilesLabel";
            IncludedFilesLabel.Size = new Size(82, 21);
            IncludedFilesLabel.TabIndex = 6;
            IncludedFilesLabel.Text = "0 included";
            // 
            // NotIncludedFilesLabel
            // 
            NotIncludedFilesLabel.AutoSize = true;
            NotIncludedFilesLabel.Location = new Point(295, 19);
            NotIncludedFilesLabel.Margin = new Padding(4, 0, 4, 0);
            NotIncludedFilesLabel.Name = "NotIncludedFilesLabel";
            NotIncludedFilesLabel.Size = new Size(109, 21);
            NotIncludedFilesLabel.TabIndex = 5;
            NotIncludedFilesLabel.Text = "0 not included";
            // 
            // PatternsLabel
            // 
            PatternsLabel.AutoSize = true;
            PatternsLabel.Location = new Point(101, 19);
            PatternsLabel.Margin = new Padding(4, 0, 4, 0);
            PatternsLabel.Name = "PatternsLabel";
            PatternsLabel.Size = new Size(133, 21);
            PatternsLabel.TabIndex = 4;
            PatternsLabel.Text = "Filename Patterns";
            // 
            // tvIncluded
            // 
            tvIncluded.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            tvIncluded.BackColor = Color.FromArgb(221, 255, 224);
            tvIncluded.Font = new Font("Consolas", 12F);
            tvIncluded.ForeColor = Color.Green;
            tvIncluded.Location = new Point(665, 42);
            tvIncluded.Name = "tvIncluded";
            tvIncluded.ShowLines = false;
            tvIncluded.ShowNodeToolTips = true;
            tvIncluded.ShowRootLines = false;
            tvIncluded.Size = new Size(399, 207);
            tvIncluded.TabIndex = 3;
            tvIncluded.MouseClick += TvIncluded_MouseClick;
            // 
            // tvNotIncluded
            // 
            tvNotIncluded.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            tvNotIncluded.BackColor = Color.FromArgb(255, 240, 240);
            tvNotIncluded.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tvNotIncluded.ForeColor = Color.Maroon;
            tvNotIncluded.ItemHeight = 24;
            tvNotIncluded.Location = new Point(297, 42);
            tvNotIncluded.Name = "tvNotIncluded";
            tvNotIncluded.ShowLines = false;
            tvNotIncluded.ShowNodeToolTips = true;
            tvNotIncluded.ShowRootLines = false;
            tvNotIncluded.Size = new Size(360, 207);
            tvNotIncluded.TabIndex = 2;
            tvNotIncluded.MouseClick += TvNotIncluded_MouseClick;
            // 
            // txtPatterns
            // 
            txtPatterns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtPatterns.Location = new Point(105, 42);
            txtPatterns.Margin = new Padding(4, 5, 4, 5);
            txtPatterns.Multiline = true;
            txtPatterns.Name = "txtPatterns";
            txtPatterns.ScrollBars = ScrollBars.Vertical;
            txtPatterns.Size = new Size(182, 207);
            txtPatterns.TabIndex = 1;
            txtPatterns.TextChanged += Selection_Changed;
            // 
            // PicSelectionStatus
            // 
            PicSelectionStatus.InitialImage = null;
            PicSelectionStatus.Location = new Point(7, 25);
            PicSelectionStatus.Name = "PicSelectionStatus";
            PicSelectionStatus.Size = new Size(72, 72);
            PicSelectionStatus.TabIndex = 0;
            PicSelectionStatus.TabStop = false;
            // 
            // ProcessGroup
            // 
            ProcessGroup.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProcessGroup.Controls.Add(CheckboxSuggestDatestamp);
            ProcessGroup.Controls.Add(label2);
            ProcessGroup.Controls.Add(CmdRefile);
            ProcessGroup.Controls.Add(CmdRename);
            ProcessGroup.Location = new Point(22, 431);
            ProcessGroup.Name = "ProcessGroup";
            ProcessGroup.Size = new Size(1091, 74);
            ProcessGroup.TabIndex = 2;
            ProcessGroup.TabStop = false;
            ProcessGroup.Text = "Processing";
            // 
            // CheckboxSuggestDatestamp
            // 
            CheckboxSuggestDatestamp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CheckboxSuggestDatestamp.AutoSize = true;
            CheckboxSuggestDatestamp.Location = new Point(500, 25);
            CheckboxSuggestDatestamp.Name = "CheckboxSuggestDatestamp";
            CheckboxSuggestDatestamp.Size = new Size(15, 14);
            CheckboxSuggestDatestamp.TabIndex = 4;
            CheckboxSuggestDatestamp.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(290, 25);
            label2.Name = "label2";
            label2.Size = new Size(207, 13);
            label2.TabIndex = 3;
            label2.Text = "Suggest a date stamp when renaming?";
            // 
            // CmdRefile
            // 
            CmdRefile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CmdRefile.Location = new Point(811, 22);
            CmdRefile.Name = "CmdRefile";
            CmdRefile.Size = new Size(256, 41);
            CmdRefile.TabIndex = 1;
            CmdRefile.Text = "2. Smart Re&file Included Content";
            CmdRefile.UseVisualStyleBackColor = true;
            CmdRefile.Click += CmdRefile_Click;
            // 
            // CmdRename
            // 
            CmdRename.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CmdRename.Location = new Point(521, 22);
            CmdRename.Name = "CmdRename";
            CmdRename.Size = new Size(284, 41);
            CmdRename.TabIndex = 0;
            CmdRename.Text = "1. View and &Rename Included Content";
            CmdRename.UseVisualStyleBackColor = true;
            CmdRename.Click += CmdRename_Click;
            // 
            // cmdClose
            // 
            cmdClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdClose.Location = new Point(930, 524);
            cmdClose.Name = "cmdClose";
            cmdClose.Size = new Size(184, 38);
            cmdClose.TabIndex = 3;
            cmdClose.Text = "&Close";
            cmdClose.UseVisualStyleBackColor = true;
            cmdClose.Click += CmdClose_Click;
            // 
            // TimerUIRefresh
            // 
            TimerUIRefresh.Tick += TimerUIRefresh_Tick;
            // 
            // TimerSelectionRefresh
            // 
            TimerSelectionRefresh.Interval = 3000;
            TimerSelectionRefresh.Tick += TimerSelectionRefresh_Tick;
            // 
            // SelectionStatusImageList
            // 
            SelectionStatusImageList.ColorDepth = ColorDepth.Depth32Bit;
            SelectionStatusImageList.ImageStream = (ImageListStreamer)resources.GetObject("SelectionStatusImageList.ImageStream");
            SelectionStatusImageList.TransparentColor = Color.Transparent;
            SelectionStatusImageList.Images.SetKeyName(0, "LED_black_red_green_medium.h.72.png");
            // 
            // ContextMenuForTreeView
            // 
            ContextMenuForTreeView.Items.AddRange(new ToolStripItem[] { CopyTextMenuItem });
            ContextMenuForTreeView.Name = "ContextMenuForTreeView";
            ContextMenuForTreeView.Size = new Size(243, 26);
            ContextMenuForTreeView.ItemClicked += ContextMenuForTreeView_ItemClicked;
            // 
            // CopyTextMenuItem
            // 
            CopyTextMenuItem.Name = "CopyTextMenuItem";
            CopyTextMenuItem.Size = new Size(242, 22);
            CopyTextMenuItem.Text = "Copy All Full Paths to Clipboard";
            // 
            // GetParameters
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1137, 581);
            Controls.Add(cmdClose);
            Controls.Add(ProcessGroup);
            Controls.Add(RefineGroup);
            Controls.Add(InputGroup);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "GetParameters";
            Text = "Naymidge";
            FormClosing += GetParameters_FormClosing;
            Load += GetParameters_Load;
            Resize += GetParameters_Resize;
            InputGroup.ResumeLayout(false);
            InputGroup.PerformLayout();
            RefineGroup.ResumeLayout(false);
            RefineGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PicSelectionStatus).EndInit();
            ProcessGroup.ResumeLayout(false);
            ProcessGroup.PerformLayout();
            ContextMenuForTreeView.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox InputGroup;
        private GroupBox RefineGroup;
        private GroupBox ProcessGroup;
        private Button cmdClose;
        private Button CmdRename;
        private PictureBox PicSelectionStatus;
        private TextBox txtPatterns;
        private TreeView tvNotIncluded;
        private TreeView tvIncluded;
        private Label IncludedFilesLabel;
        private Label NotIncludedFilesLabel;
        private Label PatternsLabel;
        private TextBox txtContentDirectory;
        private CheckBox CheckboxIncludeSubdirectories;
        private Label label1;
        private TextBox txtContentDirectoryStatus;
        private Button CmdPickContentDirectory;
        private System.Windows.Forms.Timer TimerUIRefresh;
        private System.Windows.Forms.Timer TimerSelectionRefresh;
        private SaveFileDialog ContentDirectoryDialog;
        private FolderBrowserDialog folderBrowserDialog1;
        private ImageList SelectionStatusImageList;
        private Button CmdRefile;
        private ContextMenuStrip ContextMenuForTreeView;
        private ToolStripMenuItem CopyTextMenuItem;
        private CheckBox CheckboxSuggestDatestamp;
        private Label label2;
    }
}