using SVC.src.Model;
using SVC.src.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace SVC
{
    public class GameLocationsService
    {
        public const string GamesListFileName = "gameslist.txt";
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
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
            List<string> libraryFolders = _steamLibraryReader.GetLibraryFolders(steamFolderPath);
            ReadManifestFiles(libraryFolders);
        }

        private void ReadManifestFiles(List<string> libraryFolders)
        {
            List<Game> games = new List<Game>();
            foreach (string library in libraryFolders)
            {
                string[] acfFiles = Directory.GetFiles(library + Path.DirectorySeparatorChar + "steamapps" + Path.DirectorySeparatorChar, "*.acf");
                foreach (string acfFile in acfFiles)
                {
                    var acfFileLines = File.ReadAllText(acfFile);
                    var game = _gameManifestParser.ParseAcfFile(acfFileLines);
                    if (game != null)
                    {
                        games.Add(game);
                    }
                }
            }
            using (StreamWriter file = new StreamWriter(_currentDirectory + Path.DirectorySeparatorChar + GamesListFileName))
                foreach (var game in games)
                    file.WriteLine("{0}\n{1}", "Game Name: " + game.GameName, "App ID: " + game.AppId);
        }
    }
}
