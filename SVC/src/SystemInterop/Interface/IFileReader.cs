using System.Collections.Generic;

namespace SVC.src.Services.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<string> ReadLines(string path);
    }
}
