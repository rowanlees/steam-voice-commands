﻿using SVC.Core.Extensions;
using SVC.Core.Services.Interfaces;
using SVC.Core.SystemInterop.Interface;
using SVC.Core.Services.Exceptions;
using System.IO;

namespace SVC.Core.Services.Implementations
{
    public class SteamInstallationLocator : ISteamInstallationLocator
    {
        private readonly IProcess _process;
        private readonly int _timeoutMs;

        public SteamInstallationLocator(IProcess process, int timeoutMs)
        {
            _process = process;
            _timeoutMs = timeoutMs;
        }

        public string GetSteamFolderPath()
        {
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            _process.StartInfo.FileName = "cmd.exe";
            _process.StartInfo.Arguments = "/C REG QUERY HKCU\\SOFTWARE\\Valve\\Steam /f SteamExe";
            _process.Start();
            var isProcessExited = _process.WaitForExit(_timeoutMs);
            if (!isProcessExited)
            {
                throw new SteamInstallationLocatorException("Process timed out when trying to get Steam installation location from registry.");
            }
            var result = _process.StandardOutputReadToEnd();
            return result.TextAfter("SZ").GetUntilOrEmpty("End of search").Trim().GetUntilOrEmpty("/steam.exe");
        }
    }
}
