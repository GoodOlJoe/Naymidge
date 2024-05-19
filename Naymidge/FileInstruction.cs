using MetadataExtractor;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using Vortice.Win32;

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
            DateTaken = FormattedDateTaken(InterestingImageFactCatalog.GetValueFor("Date Taken", this));
            GPSTimeZoneTaken = InterestingImageFactCatalog.GetValueFor("Time Zone Taken", this);
            GPSLat = InterestingImageFactCatalog.GetValueFor("Latitude", this);
            GPSLong = InterestingImageFactCatalog.GetValueFor("Longitude", this);
            MapURL = FormattedMapURL(GPSLat, GPSLong);
            CameraDescription = InterestingImageFactCatalog.GetValueFor("Camera Description", this);
            GPSImageDirection = InterestingImageFactCatalog.GetValueFor("Image Direction", this);
        }
        public List<MetadataExtractor.Directory>? MetadataDirectories = null;

        // image file facts harvested mostly from meta data tags
        public string DateTaken = "";
        public string GPSTimeZoneTaken = "";
        public string GPSLat = "";
        public string GPSLong = "";
        public string MapURL = "";
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
        private string FormattedMapURL(string gpsLat, string gpsLong)
        {
            string retval = "";
            if (string.IsNullOrEmpty(gpsLat) || string.IsNullOrEmpty(gpsLong))
                return retval;

            string _lat = gpsLat.Replace(" ", "");
            _lat = _lat[0].Equals("-") ? $"{_lat}S" : $"{_lat}N";

            string _long = gpsLong.Replace(" ", "");
            _long = _long[0].Equals("-") ? $"{_long}W" : $"{_long}E";

            return $"https://www.google.com/maps/place/{_lat}+{_long}";
        }
        private string FormattedDateTaken(string dateTaken)
        {
            string patternForMetaData = @"^(?<year>\d\d\d\d):(?<month>\d\d):(?<day>\d\d) (?<time>\d\d:\d\d:\d\d)$";

            Match match = Regex.Match(dateTaken, patternForMetaData);
            if (!match.Success) return dateTaken;

            GroupCollection gc = match.Groups;

            return $"{gc["year"].Value} {gc["month"].Value} {gc["day"].Value} {gc["time"].Value}";
        }
    }
}
