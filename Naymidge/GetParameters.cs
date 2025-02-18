using System.Diagnostics;
using System.Reflection;
using System.Text;
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
        private readonly List<string> _Contents = new(1000);
        private readonly Stack<Cursor> CursorStack = new(); // for tracking busy cursor
        private readonly ProcessingScope _Scope = new();
        private readonly RenameParameters _RenameParameters = new();

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
            WindowState = FormWindowState.Maximized;
        }
        private void SetControlMruBindings()
        {
            txtContentDirectory.DataBindings.Add(new Binding("Text", Properties.Settings.Default, "MruContentDirectory", true, DataSourceUpdateMode.OnPropertyChanged));
            txtContentDirectory.Text = Properties.Settings.Default.MruContentDirectory;
            txtPatterns.DataBindings.Add(new Binding("Text", Properties.Settings.Default, "MruFilenamePatterns", true, DataSourceUpdateMode.OnPropertyChanged));
            txtPatterns.Text = Properties.Settings.Default.MruFilenamePatterns;
            CheckboxIncludeSubdirectories.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "MruIncludeSubdirectories", true, DataSourceUpdateMode.OnPropertyChanged));
            CheckboxIncludeSubdirectories.Checked = Properties.Settings.Default.MruIncludeSubdirectories;
            CheckboxSuggestDatestamp.DataBindings.Add(new Binding("Checked", Properties.Settings.Default, "MruSuggestDatestamp", true, DataSourceUpdateMode.OnPropertyChanged));
            CheckboxSuggestDatestamp.Checked = Properties.Settings.Default.MruSuggestDatestamp;
            _RenameParameters.SuggestDateStamp = CheckboxSuggestDatestamp.Checked;
        }
        private bool UpdateScope()
        {
            string contentDirName = txtContentDirectory.Text.Trim();
            if (string.IsNullOrEmpty(contentDirName)) { return false; }
            //_Scope.Contents.Clear();
            //foreach (TreeNode node in tvIncluded.Nodes)
            //    if (node.ToolTipText is string fqn)
            //        _Scope.Contents.Add(fqn);
            return true;
        }
        private void CmdClose_Click(object sender, EventArgs e) { Application.Exit(); }
        private void CmdRename_Click(object sender, EventArgs e) { DoRenameForm(); }
        private void CmdRefile_Click(object sender, EventArgs e) { DoSmartRefiling(); }
        private void TimerSelectionRefresh_Tick(object sender, EventArgs e) { DoRefreshSelection(); }
        private void Selection_Changed(object sender, EventArgs e) { ResetSelectionRefreshTimer(); }
        private void TimerUIRefresh_Tick(object sender, EventArgs e) { UpdateUIEnablement(); }
        private void CmdPickContentDirectory_Click(object sender, EventArgs e) { DoPickContentDirectory(); }
        private void GetParameters_FormClosing(object sender, FormClosingEventArgs e) { Naymidge.Properties.Settings.Default.Save(); }
        private void GetParameters_Resize(object sender, EventArgs e) { PositionComponents(); }

        private void DoRenameForm()
        {
            if (!TrySelection()) return;
            _RenameParameters.SuggestDateStamp = CheckboxSuggestDatestamp.Checked;
            RenameUI frm = new(_Scope, _RenameParameters);
            frm.ShowDialog();
            DoRefreshSelection();
        }
        private void DoSmartRefiling()
        {
            if (!TrySelection()) return;
            SmartRefileUI frm = new(_Scope);
            frm.ShowDialog();
            DoRefreshSelection();
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
                if (Directory.Exists(txtContentDirectory.Text))
                {
                    SearchOption opt = CheckboxIncludeSubdirectories.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                    _Contents.AddRange(Directory.EnumerateFiles(txtContentDirectory.Text, "*", opt));
                    int dirCount = Directory.EnumerateDirectories(txtContentDirectory.Text, "*", opt).Count();
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
            CmdRename.Enabled = tvIncluded.Nodes.Count > 0;
            CmdRefile.Enabled = tvIncluded.Nodes.Count > 0;
        }
        private bool TrySelection()
        {
            if (0 == _Contents.Count || !UpdateScope()) return false;
            _Scope.Reset();

            char[] delims = ['\r', '\n'];
            _Scope.Patterns.AddRange(txtPatterns.Text.Split(delims, StringSplitOptions.RemoveEmptyEntries));

            tvIncluded.Nodes.Clear();
            tvNotIncluded.Nodes.Clear();
            foreach (string s in _Contents)
            {
                string fn = Path.GetFileName(s);
                bool matched = false;
                foreach (string p in _Scope.Patterns.Where(pat => !pat.StartsWith("//"))) // // is comment
                {
                    try
                    {
                        matched = Regex.IsMatch(fn, p, RegexOptions.IgnoreCase);
                    }
                    catch
                    {
                        // ignore invalid regex pattern but continue processing other patterns
                    }
                    if (matched) break;
                }
                if (matched)
                {
                    AddToTreeView(tvIncluded, s);
                    _Scope.Contents.Add(s);
                }
                else
                {
                    AddToTreeView(tvNotIncluded, s);
                }

            }
            tvIncluded.Enabled = true;
            tvNotIncluded.Enabled = true;
            return true;
        }
        private static void AddToTreeView(TreeView tv, string FQN)
        {
            TreeNodeCollection nodes = tv.Nodes;
            string fn = Path.GetFileName(FQN);
            TreeNode node = new(fn) { ToolTipText = FQN };
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

        private void ContextMenuForTreeView_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (null != sender && sender is ContextMenuStrip menu)
                if (null != menu.Tag && CopyTextMenuItem == e.ClickedItem)
                    Clipboard.SetText(GetTVItemsAsText((TreeView)menu.Tag));
        }
        private static string GetTVItemsAsText(TreeView tv)
        {
            StringBuilder sb = new();
            foreach (TreeNode n in tv.Nodes)
                sb.AppendLine(n.ToolTipText);
            return sb.ToString();
        }
        private void DisplayTVContextMenu(TreeView tv)
        {
            ContextMenuForTreeView.Tag = tv;
            ContextMenuForTreeView.Show(MousePosition);
        }
        private void TvIncluded_MouseClick(object sender, MouseEventArgs e) { if (MouseButtons.Right == e.Button) DisplayTVContextMenu((TreeView)sender); }
        private void TvNotIncluded_MouseClick(object sender, MouseEventArgs e) { if (MouseButtons.Right == e.Button) DisplayTVContextMenu((TreeView)sender); }
    }
}

