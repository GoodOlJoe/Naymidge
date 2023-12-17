namespace Naymidge
{
    public class ProcessingScope
    {
        public string ContentRoot { get; set; }
        public bool IncludeSubdirs { get; set; } = false;
        public List<string> Contents { get; set; } = new List<string>(2000);
        public readonly List<string> Patterns = new(30);
        public void Reset() { Patterns.Clear(); Contents.Clear(); }
        public ProcessingScope CloneEmpty() // return a scope with same settings but an empty contents list
        {
            return new ProcessingScope()
            {
                ContentRoot = ContentRoot,
                IncludeSubdirs = IncludeSubdirs,
            };
        }
    }
}
