using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Naymidge
{
    public partial class GetParameters : Form
    {
        private enum SelectionStatus
        {
            NoContentDirectory = 1,
            NoFilters = 2,
            FiltersEstablished = 3,
        }
        private readonly List<string> _Contents = new List<string>(1000);
        private readonly Stack<Cursor> CursorStack = new Stack<Cursor>(); // for tracking busy cursor
        private readonly ProcessingScope _Scope = new ProcessingScope();
        //private readonly Settings _UserSettings = new Settings();

        public GetParameters()
        {
            InitializeComponent();
        }
        private void GetParameters_Load(object sender, EventArgs e)
        {
            txtContentDirectoryStatus.Text = txtContentDirectoryStatus.Tag?.ToString();
            TimerUIRefresh.Enabled = true;
            SelectionStatusImageList.Images.AddStrip(Properties.Resources.BlackRedGreen72x3);
            CurrentSelectionStatus = SelectionStatus.NoContentDirectory;
            SetControlMruBindings();
            DoRefreshSelection();
        }
        private void SetControlMruBindings()
        {
            txtContentDirectory.DataBindings.Add(new Binding("Text", Properties.Settings.Default, "MruContentDirectory", true, DataSourceUpdateMode.OnPropertyChanged));
            txtContentDirectory.Text = Properties.Settings.Default.MruContentDirectory;
            txtPatterns.DataBindings.Add(new Binding("Text", Properties.Settings.Default, "MruFilenamePatterns", true, DataSourceUpdateMode.OnPropertyChanged));
            txtPatterns.Text = Properties.Settings.Default.MruFilenamePatterns;
            CheckboxIncludeSubdirectories.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "MruIncludeSubdirectories", true, DataSourceUpdateMode.OnPropertyChanged));
            CheckboxIncludeSubdirectories.Checked = Properties.Settings.Default.MruIncludeSubdirectories;
        }
        private bool UpdateScope()
        {
            string contentDirName = txtContentDirectory.Text.Trim();
            if (string.IsNullOrEmpty(contentDirName)) { return false; }

            //GcIndividual rootInd = gedcom_.Individuals.Values.Where(ind => ind.Name.FullName.Equals(contentDirName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            //if (null == rootInd) { return false; }

            //_Scope.StartRef = rootInd.Key;
            //_Scope.RootIndName = rootInd.FullName;
            //_Scope.RootSpouses = CheckboxIncludeSubdirectories.Checked;

            //string genStr = txtGensAnc.Text.Trim();
            //if (!int.TryParse(genStr, out int maxAncGens)) { return false; }
            //_Scope.AncestorGenerations = maxAncGens;

            //genStr = txtGensDesc.Text.Trim();
            //if (!int.TryParse(genStr, out int maxDescGens)) { return false; }
            //_Scope.DescendantGenerations = maxDescGens;
            //_Scope.DescSpouses = CheckboxDescendantSpouse.Checked;

            return true;
        }
        private void CmdClose_Click(object sender, EventArgs e) { Application.Exit(); }
        //private void CmdIngest_Click(object sender, EventArgs e) { DoIngest(); }
        private void CmdRename_Click(object sender, EventArgs e) { DoRenameForm(); }
        private void TimerSelectionRefresh_Tick(object sender, EventArgs e) { DoRefreshSelection(); }
        private void Selection_Changed(object sender, EventArgs e) { ResetSelectionRefreshTimer(); }
        private void TimerUIRefresh_Tick(object sender, EventArgs e) { UpdateUIEnablement(); }
        private void CmdPickContentDirectory_Click(object sender, EventArgs e) { DoPickContentDirectory(); }
        private void GetParameters_FormClosing(object sender, FormClosingEventArgs e) { Naymidge.Properties.Settings.Default.Save(); }
        private void GetParameters_Resize(object sender, EventArgs e) { PositionComponents(); }

        //private void DoIngest()
        //{
        //    txtLoadedFileStatus.Text = txtLoadedFileStatus.Tag.ToString();
        //    string inputFileName = txtContentDirectory.Text.Trim();
        //    if (inputFileName.Length > 0 && (gedcom_ == null || !gedcom_.FileName.Equals(inputFileName)))
        //    {
        //        ShowBusy(busy: true);
        //        gedcom_ = null;
        //        gedcom_ = new GedcomFile551(inputFileName);
        //        ShowBusy(busy: false);
        //    }
        //    if (null != gedcom_)
        //    {
        //        txtLoadedFileStatus.Text =
        //            $"{gedcom_.Moniker} loaded\r\n" +
        //            $"{gedcom_.Contents.Count} Contents\r\n" +
        //            $"{gedcom_.Families.Count} Families\r\n";

        //        txtRootIndividual.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //        txtRootIndividual.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        txtRootIndividual.AutoCompleteCustomSource.Clear();
        //        string[] allNames = new string[gedcom_.Contents.Count];
        //        int i = 0;
        //        foreach (GcIndividual ind in gedcom_.Contents.Values)
        //        {
        //            allNames[i++] = ind.FullName;
        //        }
        //        txtRootIndividual.AutoCompleteCustomSource.AddRange(allNames);

        //        DoRefreshSelection();
        //    }
        //}
        private void DoRenameForm()
        {
            //if (!TrySelection()) return;
            //index_ = new PlaceIndex();
            //foreach (GcIndividual ind in _Scope.Contents)
            //{
            //    foreach (GcDatePlaceEvent ev in ind.Events.
            //        Where(ev => null != ev.When && !string.IsNullOrEmpty(ev.Place)).
            //        OrderBy(ev => ev.When.TimelineDate))
            //    {
            //        index_.Add(ev);
            //    }
            //}
            //RenameUI frm = new RenameUI(index_, gedcom_);
            RenameUI frm = new RenameUI();
            frm.ShowDialog();
        }
        private void DoPickContentDirectory()
        {
            ContentDirectoryDialog.InitialDirectory = txtContentDirectory.Text.Trim();
            ContentDirectoryDialog.CheckPathExists = true;
            ContentDirectoryDialog.CheckFileExists = false;
            ContentDirectoryDialog.OverwritePrompt = false;
            ContentDirectoryDialog.FileName = "Process This Directory";
            ContentDirectoryDialog.Title = "Choose directory to process";
            ContentDirectoryDialog.Filter = "Directory|directory|All files|*.*";

            if (DialogResult.OK == ContentDirectoryDialog.ShowDialog(this))
            {
                txtContentDirectory.Text = Path.GetDirectoryName(ContentDirectoryDialog.FileName);
                DoCheckContentDirectory();
            }
        }
        private void DoCheckContentDirectory()
        {
            _Contents.Clear();
            txtContentDirectory.Text = txtContentDirectory.Text.Trim();
            if (!string.IsNullOrEmpty(txtContentDirectory.Text))
            {
                ShowBusy(true);
                int dirCount = 0;
                if (Directory.Exists(txtContentDirectory.Text))
                {
                    SearchOption opt = CheckboxIncludeSubdirectories.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                    _Contents.AddRange(Directory.EnumerateFiles(txtContentDirectory.Text, "*", opt));
                    dirCount = Directory.EnumerateDirectories(txtContentDirectory.Text, "*", opt).Count();
                    UpdateContentDirStatus(txtContentDirectory.Text, dirCount + 1, _Contents.Count, CheckboxIncludeSubdirectories.Checked);
                }
                else
                {
                    UpdateContentDirStatus(txtContentDirectory.Text, 0, _Contents.Count, CheckboxIncludeSubdirectories.Checked);
                }
                ShowBusy(false);
            }
        }
        private void UpdateContentDirStatus(string dirName, int dirCount, int fileCount, bool subDirectoriesIncluded)
        {
            if (0 == dirCount)
            {

                txtContentDirectoryStatus.Text = $"{dirName}\r\nis not a directory";
                txtContentDirectoryStatus.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                int subDirCount = subDirectoriesIncluded ? dirCount - 1 : 0;
                string subPhrase = $"including {subDirCount:N0} {(1 == subDirCount ? "subdirectory" : "subdirectories")}";
                subPhrase = subDirectoriesIncluded && subDirCount > 0 ? subPhrase : "";

                string filePhrase = 1 == fileCount ? "1 file" : $"{fileCount:N0} files";
                string s =
                    filePhrase +
                    $"\r\nin {dirName}" +
                    (string.IsNullOrEmpty(subPhrase) ? string.Empty : $"\r\n{subPhrase}");

                txtContentDirectoryStatus.Text = s;
                txtContentDirectoryStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }
        private void UpdateUIEnablement()
        {
            if (0 == _Contents.Count) CurrentSelectionStatus = SelectionStatus.NoContentDirectory;
            CmdRename.Enabled = _Contents.Count > 0;
            RefineGroup.Enabled = _Contents.Count > 0;
        }
        private bool TrySelection()
        {
            if (0 == _Contents.Count || !UpdateScope()) return false;
            _Scope.Reset();

            char[] delims = new char[] { '\r', '\n' };
            _Scope.Patterns.AddRange(txtPatterns.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries));

            tvIncluded.Nodes.Clear();
            tvNotIncluded.Nodes.Clear();
            foreach (string s in _Contents)
            {
                string fn = Path.GetFileName(s);
                bool matched = false;
                foreach (string p in _Scope.Patterns.Where(pat => !pat.StartsWith("//"))) // // is comment
                {
                    matched = Regex.IsMatch(fn, p, RegexOptions.IgnoreCase);
                    if (matched) break;
                }
                if (matched)
                {
                    AddToTreeView(tvIncluded, fn);
                    _Scope.Contents.Add(fn);
                }
                else
                {
                    AddToTreeView(tvNotIncluded, fn);
                }

            }
            tvIncluded.Enabled = true;
            tvNotIncluded.Enabled = true;
            return true;
        }
        private void AddToTreeView(TreeView tv, string FQN)
        {
            TreeNodeCollection nodes = tv.Nodes;
            string fn = Path.GetFileName(FQN);

            //foreach (string s in FQN.Split(Path.DirectorySeparatorChar))
            //{
            //    TreeNode node;
            //    if (nodes.ContainsKey(s))
            //        node = nodes[s];
            //    else
            //        node = nodes[nodes.Add(new TreeNode(s))];
            //    nodes = node.Nodes;
            //}

            TreeNode node = new TreeNode(fn);
            node.ToolTipText = FQN;
            nodes.Add(node);
        }
        private void ShowBusy(bool busy)
        {
            if (busy)
            {
                // show busy
                CursorStack.Push(Cursor);
                Cursor = Cursors.WaitCursor;
            }
            else
            {
                // show not busy
                Cursor = CursorStack.Count > 0 ? CursorStack.Pop() : Cursors.Default;
            }
        }
        private void ResetSelectionRefreshTimer()
        {
            ClearSelection();
            CurrentSelectionStatus = SelectionStatus.NoFilters;
            TimerSelectionRefresh.Enabled = false;
            TimerSelectionRefresh.Enabled = true;
        }
        private void ClearSelection()
        {
            tvIncluded.Enabled = false;
            tvNotIncluded.Enabled = false;
        }
        private void DoRefreshSelection()
        {
            TimerSelectionRefresh.Enabled = false;
            DoCheckContentDirectory();
            if (TrySelection())
            {
                CurrentSelectionStatus = SelectionStatus.FiltersEstablished;
            }
            else
            {
                if (0 == _Contents.Count) CurrentSelectionStatus = SelectionStatus.NoContentDirectory;
                else CurrentSelectionStatus = SelectionStatus.NoFilters;
            }
            NotIncludedFilesLabel.Text = $"{tvNotIncluded.Nodes.Count:N0} not included";
            IncludedFilesLabel.Text = $"{tvIncluded.Nodes.Count:N0} included";
        }
        private SelectionStatus selectionStatus_;
        private SelectionStatus CurrentSelectionStatus
        {
            get => selectionStatus_;
            set
            {
                if (selectionStatus_ != value)
                {
                    selectionStatus_ = value;
                    PicSelectionStatus.Image = SelectionStatusImageList.Images[(int)selectionStatus_];
                }
            }
        }
        private void PositionLabels()
        {
            IncludedFilesLabel.Left = tvIncluded.Left;
            NotIncludedFilesLabel.Left = tvNotIncluded.Left;
        }
        private void PositionComponents()
        {
            int FillSpace = RefineGroup.Width - txtPatterns.Right;

            int padBetween = 15;
            tvNotIncluded.Left = txtPatterns.Right + padBetween;
            tvNotIncluded.Width = (FillSpace - (padBetween * 3)) / 2;
            tvIncluded.Left = tvNotIncluded.Right + padBetween;
            tvIncluded.Width = tvNotIncluded.Width;
            PositionLabels();
        }
    }
}
