using System.Collections.Generic;

namespace SVC.src.Services.Interfaces
{
    public interface IGameManifestParser
    {
        void ParseAcfFile(string acfContent, Dictionary<string, string> dictionary);
    }
}
