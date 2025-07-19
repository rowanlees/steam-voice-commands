using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SVC
{
    internal class GameLocations
    {
        public const string GamesListFileName = "gameslist.txt";
        private const string SteamInstallLocationFileName = "steaminstalllocation.txt";
        private string _steamExeLocation;
        private string _steamFolderLocation;
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private bool _fileExists = false;
        private readonly string _steamInstallRegQuery = "cmd /c REG QUERY HKCU\\SOFTWARE\\Valve\\Steam /f SteamExe >steaminstalllocation.txt";
        private readonly Dictionary<string, string> _gamesList = new Dictionary<string, string>();
        private readonly List<string> libraryFolders = new List<string>();

        public void QuerySteamInstallLocation()
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WorkingDirectory = _currentDirectory;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = _steamInstallRegQuery;
            process.Start();
            while (_fileExists == false)
            {
                if (File.Exists(_currentDirectory + Path.DirectorySeparatorChar + SteamInstallLocationFileName))
                {
                    _fileExists = true;
                    process.WaitForExit();
                }
            }
            WriteSteamInstallLocation();
            ReadLibraryFolders();
            ReadManifestFiles();
        }

        private void WriteSteamInstallLocation()
        {
            _steamExeLocation = File.ReadAllText(_currentDirectory + Path.DirectorySeparatorChar + SteamInstallLocationFileName);
            _steamExeLocation = _steamExeLocation.TextAfter("SZ");
            _steamExeLocation = _steamExeLocation.GetUntilOrEmpty("End of search");
            _steamExeLocation = _steamExeLocation.Trim();
            _steamFolderLocation = _steamExeLocation.GetUntilOrEmpty("/steam.exe");
            File.WriteAllText(_currentDirectory + "\\steamfolderlocation.txt", _steamFolderLocation);
        }

        private void ReadLibraryFolders()
        {
            var lines = File.ReadLines(_steamFolderLocation + "/steamapps/libraryfolders.vdf");
            foreach (string line in lines)
            {
                if (line.Contains("path"))
                {
                    string path = line.TextAfter("path");
                    path = path.TextAfter("\"");
                    path = path.Trim();
                    path = path.Replace("\"", "");
                    libraryFolders.Add(path);
                }
            }
            string libraryFoldersCombined = string.Join(",", libraryFolders);
            File.WriteAllText(_currentDirectory + "\\steamlibraryfolders.txt", libraryFoldersCombined);
        }

        private void ReadManifestFiles()
        {
            foreach (string library in libraryFolders)
            {
                string[] acfFiles = Directory.GetFiles(library + "\\\\" + "steamapps" + "\\\\", "*.acf");
                foreach (string acfFile in acfFiles)
                {
                    var lines = File.ReadAllText(acfFile);
                    string appid = lines.TextAfter("appid");
                    appid = appid.GetUntilOrEmpty("Universe");
                    appid = appid.Trim();
                    appid = appid.Replace("\"", "");
                    appid = appid.Trim();
                    appid = appid.Insert(0, "App ID: ");
                    string gameName = lines.TextAfter("steam.exe");
                    gameName = gameName.TextAfter("name");
                    gameName = gameName.GetUntilOrEmpty("StateFlags");
                    gameName = gameName.Replace("\"", "");
                    gameName = gameName.Trim();
                    gameName = gameName.Insert(0, "Game Name: ");

                    _gamesList.Add(gameName, appid);
                }
            }
            using (StreamWriter file = new StreamWriter(_currentDirectory + Path.DirectorySeparatorChar + GamesListFileName))
                foreach (var entry in _gamesList)
                    file.WriteLine("{0}\n{1}", entry.Key, entry.Value);
        }

    }
}
