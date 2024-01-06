using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naymidge
{
    internal static class FileActions
    {
        internal static string DoInstruction(FileInstruction instruction)
        {
            string retMsg = "";
            switch (instruction.Verb)
            {
                case FileInstructionVerb.Delete:
                    retMsg = DoDelete(instruction);
                    break;
                case FileInstructionVerb.Rename:
                    retMsg = DoRename(instruction);
                    break;
            }
            return retMsg;
        }
        private static string DoDelete(FileInstruction instruction)
        {
            try
            {
                File.Delete(instruction.FQN);
            }
            catch (Exception ex)
            {
                return ($"Cannot delete [{instruction.FQN}]\r\n{ex.Message}");
            }
            return "";
        }
        private static string DoRename(FileInstruction instruction)
        {
            if (!File.Exists(instruction.FQN))
                return ($"Cannot rename file that does not exist [{instruction.FQN}]");

            string newFQN = GetNewFQN(instruction);
            if (string.IsNullOrEmpty(newFQN))
                return ($"Cannot rename file, unknown error [{instruction.FQN}]");

            if (File.Exists(newFQN))
                newFQN = GetNewSerialFQN(newFQN);

            return "";
        }
        private static string GetNewFQN(FileInstruction instruction)
        {
            string? dir = Path.GetDirectoryName(instruction.FQN);
            if (string.IsNullOrEmpty(dir))
                return "";
            return Path.Combine(dir, instruction.NewFileName);
        }
        private static string GetNewSerialFQN(string FQN)
        {
            // create a FQN with a serial number that will make it unique in the given dir
            string? dir = Path.GetDirectoryName(FQN);
            Directory.GetFiles()
            if (string.IsNullOrEmpty(dir))
                return "";
            return Path.Combine(dir, instruction.NewFileName);
        }
    }
}
