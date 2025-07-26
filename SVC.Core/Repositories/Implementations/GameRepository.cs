using Newtonsoft.Json;
using SVC.Core.Model;
using SVC.Core.Repositories.Interfaces;
using SVC.Core.SystemInterop.Interface;
using System.Collections.Generic;
using System.IO;

namespace SVC.Core.Repositories.Implementations
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
                    file.WriteLine(JsonConvert.SerializeObject(game));
                }
            }
        }

        public List<Game> LoadGames()
        {
            var path = Path.Combine(_fileSystem.GetCurrentDirectory(), GamesListFileName);
            var lines = _fileSystem.ReadLines(path);
            var games = new List<Game>();
            {
                foreach (var line in lines)
                {
                    games.Add(ReadGameFromJsonObject(line));
                }
            }
            return games;
        }

        public Game ReadGameFromJsonObject(string json)
        {
            return JsonConvert.DeserializeObject<Game>(json);
        }
    }
}
