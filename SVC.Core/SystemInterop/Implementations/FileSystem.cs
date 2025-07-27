using SVC.Core.SystemInterop.Interface;
using System.Collections.Generic;
using System.IO;

namespace SVC.Core.SystemInterop.Implementations
{
    public class FileSystem : IFileSystem
    {
        public TextWriter CreateTextWriter(string path)
        {
            return new StreamWriter(path);
        }

        public string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }
    }
}
