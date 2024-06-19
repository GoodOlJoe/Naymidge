using MetadataExtractor;
using System.Text.RegularExpressions;

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
        private readonly string _FQN;
        public FileInstruction(string fqn)
        {
            _FQN = fqn;
            try
            {
                MetadataDirectories = [.. ImageMetadataReader.ReadMetadata(fqn)];
                DateTimeTaken = FormattedDateTimeTaken(InterestingImageFactCatalog.GetValueFor("Date Taken", this));
                DateTaken = FormattedDateTaken(DateTimeTaken);
                GPSTimeZoneTaken = InterestingImageFactCatalog.GetValueFor("Time Zone Taken", this);
                GPSLat = InterestingImageFactCatalog.GetValueFor("Latitude", this);
                GPSLong = InterestingImageFactCatalog.GetValueFor("Longitude", this);
                MapURL = FormattedMapURL(GPSLat, GPSLong);
                CameraDescription = InterestingImageFactCatalog.GetValueFor("Camera Description", this);
                GPSImageDirection = InterestingImageFactCatalog.GetValueFor("Image Direction", this);
                RequiredRotationToNormal = FormattedRotationRequirement(
                    InterestingImageFactCatalog.GetValueFor("Image Orientation", this),
                    InterestingImageFactCatalog.GetValueFor("Video Orientation", this));
            }
            catch { }
        }
        public List<MetadataExtractor.Directory>? MetadataDirectories = null;

        // image file facts harvested mostly from meta data tags
        public string DateTimeTaken = "";
        public string DateTaken = "";
        public string GPSTimeZoneTaken = "";
        public string GPSLat = "";
        public string GPSLong = "";
        public string MapURL = "";
        public string CameraDescription = "";
        public string GPSImageDirection = "";
        public string RequiredRotationToNormal = "0";

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
        private static string FormattedMapURL(string gpsLat, string gpsLong)
        {
            string retval = "";
            if (string.IsNullOrEmpty(gpsLat) || string.IsNullOrEmpty(gpsLong))
                return retval;

            string _lat = gpsLat.Replace(" ", "");
            _lat = _lat[0].Equals('-') ? $"{_lat[1..^0]}S" : $"{_lat}N";

            string _long = gpsLong.Replace(" ", "");
            _long = _long[0].Equals('-') ? $"{_long[1..^0]}W" : $"{_long}E";

            return $"https://www.google.com/maps/place/{_lat}+{_long}";
        }
        private static string FormattedRotationRequirement(string imageOrientation, string videoOrientation)
        {
            string retval = "0";
            string patt = @"\(Rotate (?<rot>90 CW|90 CCW|180)\)$";

            Match match = Regex.Match(imageOrientation, patt);
            if (match.Success &&
                0 < match.Groups.Count &&
                match.Groups.ContainsKey("rot"))
            {
                string rot = match.Groups["rot"].Value;

                if (rot.Equals("180")) retval = "180";
                if (rot.Equals("90 CW")) retval = "90";
                if (rot.Equals("90 CCW")) retval = "270";
                return retval;
            }

            // dont' really understand this yet. MOV video files have a property
            // that seems to have the value -90 but the video needs 180° rotation.
            // this will probably need refinement as I learn more.
            if (!string.IsNullOrEmpty(videoOrientation))
            {
                if (videoOrientation.Equals("-90")) retval = "180";
            }
            return retval;
        }
        private static string FormattedDateTimeTaken(string dateTaken)
        {
            string patternForMetaData = @"^(?<year>\d\d\d\d):(?<month>\d\d):(?<day>\d\d) (?<time>\d\d:\d\d:\d\d)$";

            Match match = Regex.Match(dateTaken, patternForMetaData);
            if (!match.Success) return dateTaken;

            GroupCollection gc = match.Groups;

            return $"{gc["year"].Value} {gc["month"].Value} {gc["day"].Value} {gc["time"].Value}";
        }
        private static string FormattedDateTaken(string dateTimeTaken)
        {
            if (string.IsNullOrEmpty(dateTimeTaken)) return "";

            string patt = @"^(?<year>\d\d\d\d) (?<month>\d\d) ((?<day>\d\d) )?";

            Match match = Regex.Match(dateTimeTaken, patt);
            if (!match.Success) return "";

            GroupCollection gc = match.Groups;
            string y = gc.ContainsKey("year") ? gc["year"].Value : "";
            string m = gc.ContainsKey("month") ? gc["month"].Value : "";
            string d = gc.ContainsKey("day") ? gc["day"].Value : "";
            return $"{y} {m} {d}".Trim();
        }
    }
}
