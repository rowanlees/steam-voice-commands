using SVC.src.Services.Interfaces;
using System.Diagnostics;

namespace SVC.Core.SystemInterop.Implementations
{
    public class ProcessWrapper : IProcess
    {
        private readonly Process _process;

        public ProcessWrapper(Process process)
        {
            _process = process;
        }

        public ProcessStartInfo StartInfo => _process.StartInfo;

        public string StandardOutputReadToEnd()
        {
            return _process.StandardOutput.ReadToEnd();
        }

        public void Start()
        {
            _process.Start();
        }

        public bool WaitForExit(int milliseconds)
        {
            return _process.WaitForExit(milliseconds);
        }
    }
}
