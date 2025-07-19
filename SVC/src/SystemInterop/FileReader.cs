using SVC.src.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
