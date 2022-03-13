using System.IO;

namespace StiMole
{
    internal class ReadDirectory
    {
        public static string[] FoldersInDirectory(string workingDirectory)
        {
            try
            {
                return Directory.GetDirectories(workingDirectory);
            }
            catch
            {
                return null;
            }
        }

        public static string[] FilesInDirectory(string workingDirectory)
        {
            try
            {
                return Directory.GetFiles(workingDirectory);
            }
            catch
            {
                return null;
            }
        }
    }
}
