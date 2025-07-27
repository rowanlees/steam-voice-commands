using SVC.Core.Model;
using System.Collections.Generic;

namespace SVC.Core.Repositories.Interfaces
{
    public interface IGameRepository
    {
        void SaveGames(List<Game> games);
        List<Game> LoadGames();
    }
}
