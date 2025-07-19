using SVC.src.Model;
using SVC.src.Services.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace SVC.src.Services.Implementations
{
    public class GameRepository : IGameRepository
    {
        public const string GamesListFileName = "gameslist.txt";
        private readonly IFileSystem _fileSystem;

        public GameRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }
        public void SaveGames(List<Game> games)
        {
            var path = Path.Combine(_fileSystem.GetCurrentDirectory(), GamesListFileName);
            using (var file = _fileSystem.CreateTextWriter(path))
            {
                foreach (var game in games)
                {
                    file.WriteLine("Game Name: " + game.GameName);
                    file.WriteLine("App ID: " + game.AppId);
                }
            }
        }
    }
}
