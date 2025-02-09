using Microsoft.VisualBasic;

namespace Naymidge
{
    public class RenameParameters
    {
        public bool SuggestDateStamp { get; set; } = false;
        public void Reset() { SuggestDateStamp = false; }
    }
}
