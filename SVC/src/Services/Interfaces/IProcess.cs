using System.Diagnostics;
using System.IO;

namespace SVC.src.Services.Interfaces
{
    public interface IProcess
    {
        bool WaitForExit(int milliseconds);
        string StandardOutputReadToEnd();
        ProcessStartInfo StartInfo { get; }
        void Start();
    }
}
