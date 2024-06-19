using System.Text.RegularExpressions;

namespace Naymidge
{
    internal static partial class FileActions
    {
        internal static string DoInstruction(FileInstruction instruction)
        {
            string retMsg = "";
            switch (instruction.Verb)
            {
                case FileInstructionVerb.Delete:
                    DoDelete(instruction);
                    break;
                case FileInstructionVerb.Rename:
                    retMsg = DoRename(instruction);
                    break;
            }
            return retMsg;
        }
        private static void DoDelete(FileInstruction instruction)
        {
            File.Delete(instruction.FQN);
        }
        private static string DoRename(FileInstruction instruction)
        {
            string newFQN = SerialFQN(TargetFQN(instruction));
            File.Move(instruction.FQN, newFQN);
            return newFQN;
        }
        internal static string DoSmartRefile(FileInstruction instruction, string target, bool fileByDate, bool useDateTakenIfFilenameUndated)
        {
            string finalTarget = FinalRefileTarget(instruction, target, fileByDate, useDateTakenIfFilenameUndated);
            string newFQN = SerialFQN(TargetFQNForRefile(instruction, finalTarget));
            instruction.Rename(newFQN);
            EnsureDirectoryExists(newFQN);
            File.Move(instruction.FQN, newFQN);
            return newFQN;
        }
        private static void EnsureDirectoryExists(string FQN)
        {
            if (string.IsNullOrEmpty(FQN)) return;

            string? target = Path.GetDirectoryName(FQN);
            if (!string.IsNullOrEmpty(target) && !Path.Exists(target))
            {
                try
                {
                    Directory.CreateDirectory(target);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"The system was unable to create {target}\r\n\r\n{ex.Message}");
                }
            }
        }
        private static string FinalRefileTarget(FileInstruction instruction, string target, bool fileByDate, bool useDateTakenIfFilenameUndated)
        {
            if (!fileByDate) return target;

            string retval = target;
            string patt = @"^((?<year>\d\d\d\d) ((?<month>\d\d) )?)?(?<everything_else>.+)$";

            Match match = Regex.Match(instruction.FileName, patt);
            if (match.Success && 0 < match.Groups.Count)
            {
                // the file name starts with 'yyyy' or 'yyyy mm'
                if (match.Groups.ContainsKey("year") && !string.IsNullOrEmpty(match.Groups["year"].Value))
                {
                    retval = Path.Combine(retval, match.Groups["year"].Value);

                    if (match.Groups.ContainsKey("month") && !string.IsNullOrEmpty(match.Groups["month"].Value))
                        retval = Path.Combine(retval, match.Groups["month"].Value);
                }
                else if (useDateTakenIfFilenameUndated && !string.IsNullOrEmpty(instruction.DateTaken) && instruction.DateTaken.Length > 7)
                {
                    // the file name does not start with a date, use the 'date taken' property,
                    // which may be from image meta data or may be the file creation date if no meta data
                    retval = Path.Combine(retval, instruction.DateTaken[0..4]); // year
                    retval = Path.Combine(retval, instruction.DateTaken[5..7]); // month
                }
                else
                {
                    // the file name does not start with a date, use 'undated'
                    retval = Path.Combine(target, "undated");
                }
            }
            return retval;
        }
        private static string TargetFQN(FileInstruction instruction)
        {
            string? dir = Path.GetDirectoryName(instruction.FQN);
            dir = string.IsNullOrEmpty(dir) ? "" : dir;

            string? ext = Path.GetExtension(instruction.FQN);
            ext = string.IsNullOrEmpty(ext) ? "" : ext;

            return Path.Combine(dir, $"{instruction.NewFileName}{ext}");
        }
        private static string TargetFQNForRefile(FileInstruction instruction, string newTargetDir)
        {
            string dir = newTargetDir;
            string basename = Path.GetFileNameWithoutExtension(instruction.FQN);
            string? ext = Path.GetExtension(instruction.FQN);
            ext = string.IsNullOrEmpty(ext) ? "" : ext;

            return Path.Combine(dir, $"{basename}{ext}");
        }
        private static string SerialFQN(string FQN)
        {
            // return an FQN that will be unique in the given dir, adding serial numbers if necessary

            if (!File.Exists(FQN)) return FQN; // no need to add serial

            string? dir = Path.GetDirectoryName(FQN);
            dir = string.IsNullOrEmpty(dir) ? "" : dir;

            string? filename = Path.GetFileNameWithoutExtension(FQN);
            filename = string.IsNullOrEmpty(filename) ? "" : filename;

            string? ext = Path.GetExtension(FQN);
            ext = string.IsNullOrEmpty(ext) ? "" : ext;

            Regex rgx = FileNameSerialNumberRegex();
            MatchCollection matches = rgx.Matches(filename);
            string basename = matches.Count > 0 ? matches[0].Groups["basename"].Value : filename;

            int serial = 1;
            string trialName = Path.Combine(dir, $"{basename} {serial}{ext}");
            while (File.Exists(trialName))
            {
                serial++;
                trialName = Path.Combine(dir, $"{basename} {serial}{ext}");
            }

            return trialName;
        }

        [GeneratedRegex(@"(?<basename>.+?)(?<serial>[\d\W]+)?$")]
        private static partial Regex FileNameSerialNumberRegex();
    }
}
