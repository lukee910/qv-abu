using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QvAbu.CLI.Wrappers
{
    public interface IFile
    {
        Task Copy(string sourceFileName, string destFileName, bool overwrite = true);
        Task Delete(string path);
        Task<bool> Exists(string path);
        string GetFileName(FileInfo info);
        /// <summary>
        /// Get the name of a file that doesn't create a conflict, along the lines of windows' copy function:
        /// Example: "Path\New File.txt" => "Path\New File (1).txt"
        /// </summary>
        /// <param name="fileName">The path of the file</param>
        /// <returns>Null if the given file is not valid or a fileName of a file that doesn't exist</returns>
        FileInfo GetNonConflictFileName(string fileName);
        Task<string> ReadAllText(string fileName);
        Task WriteAllText(string fileName, string text);
    }

    public class FileWrapper : IFile
    {
        #region Members

        #endregion

        #region Ctor

        #endregion

        #region Properties

        #endregion

        #region Methods

        public async Task Copy(string sourceFileName, string destFileName, bool overwrite = true)
        {
            var destParts = destFileName.Split('\\');
            var dir = string.Join("\\", destParts.Take(destParts.Length - 1));
            if (await Task.Run(() => !Directory.Exists(dir)))
            {
                await Task.Run(() => Directory.CreateDirectory(dir));
            }

            await Task.Run(() => File.Copy(sourceFileName, destFileName, overwrite));
        }

        public async Task Delete(string path)
            => await Task.Run(() => File.Delete(path));

        public string GetFileName(FileInfo info) => info.Name.Replace(info.Extension, "");

        public FileInfo GetNonConflictFileName(string fileName)
        {
            FileInfo fileInfo;
            try
            {
                fileInfo = new FileInfo(fileName);
            }
            catch
            {
                return null;
            }

            var i = 1;
            var newFileInfo = new FileInfo(fileInfo.FullName);
            while (newFileInfo.Exists)
            {
                newFileInfo = new FileInfo(fileInfo.FullName.Replace(fileInfo.Extension, $" ({i++}){fileInfo.Extension}"));

                if (i == 100)
                {
                    return null;
                }
            }
            return newFileInfo;
        }

        public async Task<bool> Exists(string path)
            => await Task.Run(() => File.Exists(path));

        public async Task<string> ReadAllText(string fileName)
            => await Task.Run(() => File.ReadAllText(fileName, Encoding.GetEncoding(1252)));

        public async Task WriteAllText(string fileName, string text)
            => await Task.Run(() => File.WriteAllText(fileName, text));

        #endregion
    }
}
