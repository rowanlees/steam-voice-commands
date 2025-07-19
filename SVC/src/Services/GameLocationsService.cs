using SVC.src.Services;
using SVC.src.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SVC
{
    public class GameLocationsService
    {
        public const string GamesListFileName = "gameslist.txt";
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private readonly Dictionary<string, string> _gamesList = new Dictionary<string, string>();
        private readonly IGameManifestParser _gameManifestParser;
        private readonly ISteamInstallationLocator _steamInstallationLocator;
        private readonly ISteamLibraryReader _steamLibraryReader;

        public GameLocationsService(IGameManifestParser gameManifestParser, ISteamInstallationLocator steamInstallationLocator, ISteamLibraryReader steamLibraryReader)
        {
            _gameManifestParser = gameManifestParser;
            _steamInstallationLocator = steamInstallationLocator;
            _steamLibraryReader = steamLibraryReader;
        }

        public void QuerySteamInstallLocation()
        {
            var steamFolderPath = _steamInstallationLocator.GetSteamFolderPath();
            List<string> libraryFolders =  _steamLibraryReader.GetLibraryFolders(steamFolderPath);
            ReadManifestFiles(libraryFolders);
        }

        private void ReadManifestFiles(List<string> libraryFolders)
        {
            foreach (string library in libraryFolders)
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
