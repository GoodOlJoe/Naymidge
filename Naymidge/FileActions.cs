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
        private static string TargetFQN(FileInstruction instruction)
        {
            string? dir = Path.GetDirectoryName(instruction.FQN);
            dir = string.IsNullOrEmpty(dir) ? "" : dir;

            string? ext = Path.GetExtension(instruction.FQN);
            ext = string.IsNullOrEmpty(ext) ? "" : ext;

            return Path.Combine(dir, $"{instruction.NewFileName}{ext}");
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
