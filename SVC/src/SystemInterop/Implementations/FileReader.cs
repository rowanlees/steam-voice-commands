using SVC.src.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace SVC.src.Services
{
    internal class FileReader : IFileReader
    {
        public IEnumerable<string> ReadLines(string path)
        {
            return File.ReadLines(path);
        }
    }
}
