using SVC.src.Model;
using System.Collections.Generic;

namespace SVC.src.Services.Interfaces
{
    public interface IGameRepository
    {
        void SaveGames(List<Game> games);
    }
}
