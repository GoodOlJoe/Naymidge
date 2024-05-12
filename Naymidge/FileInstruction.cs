using MetadataExtractor;
using System.Linq;

namespace Naymidge
{
    internal enum FileInstructionVerb
    {
        Undetermined = 0,
        Rename,
        Delete
    }
    internal class FileInstruction
    {
        private string _FQN;
        public FileInstruction(string fqn)
        {
            _FQN = fqn;
            MetadataDirectories = ImageMetadataReader.ReadMetadata(fqn).ToList();
            DateTaken = InterestingImageFactCatalog.GetValueFor("Date Taken", this);
            GPSTimeZoneTaken = InterestingImageFactCatalog.GetValueFor("Time Zone Taken", this);
            GPSLat = InterestingImageFactCatalog.GetValueFor("Latitude", this);
            GPSLong = InterestingImageFactCatalog.GetValueFor("Longitude", this);
            CameraDescription = InterestingImageFactCatalog.GetValueFor("Camera Description", this);
            GPSImageDirection = InterestingImageFactCatalog.GetValueFor("Image Direction", this);
        }
        public List<MetadataExtractor.Directory>? MetadataDirectories = null;

        // image file facts harvested mostly from meta data tags
        public string DateTaken = "";
        public string GPSTimeZoneTaken = "";
        public string GPSLat = "";
        public string GPSLong = "";
        public string CameraDescription = "";
        public string GPSImageDirection = "";

        public string FQN => _FQN;
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
