using System.Collections.Generic;
using System.IO;

namespace SVC.Core.SystemInterop.Interface
{
    public interface IFileSystem
    {
        IEnumerable<string> ReadLines(string path);
        string GetCurrentDirectory();
        TextWriter CreateTextWriter(string path);
    }
}
