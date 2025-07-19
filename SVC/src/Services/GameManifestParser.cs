using SVC.src.Services.Interfaces;
using System.Collections.Generic;

namespace SVC.src.Services
{
    public class GameManifestParser : IGameManifestParser
    {
        public void ParseAcfFile(string acfContent, Dictionary<string, string> dictionary)
        {
            if (string.IsNullOrWhiteSpace(acfContent))
            {
                return; // No content to process
            }
            string appid = acfContent.TextAfter("appid");
            appid = appid.GetUntilOrEmpty("\n");
            appid = appid.Trim();
            appid = appid.Replace("\"", "");
            appid = appid.Trim();
            appid = appid.Insert(0, "App ID: ");
            string gameName = acfContent.TextAfter("steam.exe");
            gameName = gameName.TextAfter("name");
            gameName = gameName.GetUntilOrEmpty("\n");
            gameName = gameName.Replace("\"", "");
            gameName = gameName.Trim();
            gameName = gameName.Insert(0, "Game Name: ");

            dictionary.Add(gameName, appid);
        }
    }
}
