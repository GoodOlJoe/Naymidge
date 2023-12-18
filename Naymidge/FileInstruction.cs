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
        public string FQN { get; set; } = fqn;
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
        public string NewFileName { get; set; } = string.Empty;
        public FileInstructionVerb Verb { get; set; } = FileInstructionVerb.Undetermined;
    }
}
