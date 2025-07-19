using SVC.src.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SVC
{
    public class GameLocationsService
    {
        public const string GamesListFileName = "gameslist.txt";
        private string _steamExeLocation;
        private string _steamFolderLocation;
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private readonly Dictionary<string, string> _gamesList = new Dictionary<string, string>();
        private readonly List<string> _libraryFolders = new List<string>();
        private readonly IGameManifestParser _gameManifestParser;
        private readonly ISteamInstallationLocator _steamInstallationLocator;

        public GameLocationsService(IGameManifestParser gameManifestParser, ISteamInstallationLocator steamInstallationLocator)
        {
            _gameManifestParser = gameManifestParser;
            _steamInstallationLocator = steamInstallationLocator;
        }

        public void QuerySteamInstallLocation()
        {
            WriteSteamInstallLocation();
            ReadLibraryFolders();
            ReadManifestFiles();
        }

        private void WriteSteamInstallLocation()
        {
            _steamExeLocation = _steamInstallationLocator.GetSteamFolderPath();
            _steamExeLocation = _steamExeLocation.TextAfter("SZ");
            _steamExeLocation = _steamExeLocation.GetUntilOrEmpty("End of search");
            _steamExeLocation = _steamExeLocation.Trim();
            _steamFolderLocation = _steamExeLocation.GetUntilOrEmpty("/steam.exe");
            File.WriteAllText(_currentDirectory + Path.DirectorySeparatorChar + "steamfolderlocation.txt", _steamFolderLocation);
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
                    _libraryFolders.Add(path);
                }
            }
            string libraryFoldersCombined = string.Join(",", _libraryFolders);
            File.WriteAllText(_currentDirectory + Path.DirectorySeparatorChar + "steamlibraryfolders.txt", libraryFoldersCombined);
        }

        private void ReadManifestFiles()
        {
            foreach (string library in _libraryFolders)
            {
                string[] acfFiles = Directory.GetFiles(library + Path.DirectorySeparatorChar + "steamapps" + Path.DirectorySeparatorChar, "*.acf");
                foreach (string acfFile in acfFiles)
                {
                    var acfFileLines = File.ReadAllText(acfFile);
                    _gameManifestParser.ParseAcfFile(acfFileLines, _gamesList);
                }
            }
            using (StreamWriter file = new StreamWriter(_currentDirectory + Path.DirectorySeparatorChar + GamesListFileName))
                foreach (var entry in _gamesList)
                    file.WriteLine("{0}\n{1}", entry.Key, entry.Value);
        }
    }
}
