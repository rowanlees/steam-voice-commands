using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVC.src.Services.Interfaces
{
    public interface IGameRepository
    {
        void SaveGames(Dictionary<string, string> games);
    }
}
