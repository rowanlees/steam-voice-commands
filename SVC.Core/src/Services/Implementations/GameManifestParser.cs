using SVC.src.Model;
using SVC.src.Services.Interfaces;

namespace SVC.Core.Services.Implementations
{
    public class GameManifestParser : IGameManifestParser
    {
        public Game ParseAcfFile(string acfContent)
        {
            if (string.IsNullOrWhiteSpace(acfContent))
            {
                return null;
            }
            string appid = acfContent.TextAfter("appid");
            appid = appid.GetUntilOrEmpty("\n");
            appid = appid.Trim();
            appid = appid.Replace("\"", "");
            appid = appid.Trim();
            string gameName = acfContent.TextAfter("steam.exe");
            gameName = gameName.TextAfter("name");
            gameName = gameName.GetUntilOrEmpty("\n");
            gameName = gameName.Replace("\"", "");
            gameName = gameName.Trim();

            return new Game(appid, gameName);
        }
    }
}
