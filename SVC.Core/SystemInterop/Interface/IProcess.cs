using System.Diagnostics;

namespace SVC.Core.SystemInterop.Interface
{
    public interface IProcess
    {
        bool WaitForExit(int milliseconds);
        string StandardOutputReadToEnd();
        ProcessStartInfo StartInfo { get; }
        void Start();
    }
}
