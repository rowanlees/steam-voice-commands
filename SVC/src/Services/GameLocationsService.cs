using SVC.src.Model;
using SVC.src.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace SVC
{
    public class GameLocationsService
    {
        private readonly IGameManifestParser _gameManifestParser;
        private readonly ISteamInstallationLocator _steamInstallationLocator;
        private readonly ISteamLibraryReader _steamLibraryReader;
        private readonly IGameRepository _gameRepository;

        public GameLocationsService(
            IGameManifestParser gameManifestParser,
            ISteamInstallationLocator steamInstallationLocator,
            ISteamLibraryReader steamLibraryReader,
            IGameRepository gameRepository
            )
        {
            _gameManifestParser = gameManifestParser;
            _steamInstallationLocator = steamInstallationLocator;
            _steamLibraryReader = steamLibraryReader;
            _gameRepository = gameRepository;
        }

        public void BuildGameList()
        {
            var steamFolderPath = _steamInstallationLocator.GetSteamFolderPath();
            List<string> libraryFolders = _steamLibraryReader.GetLibraryFolders(steamFolderPath);
            var games = GetGamesList(libraryFolders);
            _gameRepository.SaveGames(games);
        }

        private List<Game> GetGamesList(List<string> libraryFolders)
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
            return games;
        }
    }
}
