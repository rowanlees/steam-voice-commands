using System.Collections.Generic;
using System.IO;

namespace SVC.src.Services.Interfaces
{
    public interface IFileSystem
    {
        IEnumerable<string> ReadLines(string path);
        string GetCurrentDirectory();
        TextWriter CreateTextWriter(string path);
    }
}
