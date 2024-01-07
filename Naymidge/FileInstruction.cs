namespace Naymidge
{
    internal enum FileInstructionVerb
    {
        Undetermined = 0,
        Rename,
        Delete
    }
    internal class FileInstruction(string fqn)
    {
        public string FQN { get; } = fqn;
        public bool Completed = false;
        public string DirName
        {
            get
            {
                string ret = "";
                string? dname = Path.GetDirectoryName(FQN);
                if (dname != null) ret = dname;
                return ret;
            }
        }
        public string FileName { get => Path.GetFileName(FQN); }
        public string NewFileName { get; private set; } = string.Empty;
        public FileInstructionVerb Verb { get; private set; } = FileInstructionVerb.Undetermined;

        public void Rename(string newName) { NewFileName = newName; Verb = FileInstructionVerb.Rename; }
        public void Reset() { NewFileName = ""; Verb = FileInstructionVerb.Undetermined; }
        public void ToggleDelete()
        {
            if (FileInstructionVerb.Delete == Verb)
            {
                Reset();
            }
            else
            {
                NewFileName = ""; Verb = FileInstructionVerb.Delete;
            }
        }
    }
}
