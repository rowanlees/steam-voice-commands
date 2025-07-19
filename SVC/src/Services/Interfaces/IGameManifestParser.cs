using System.Collections.Generic;

namespace SVC.src.Services.Interfaces
{
    internal interface IGameManifestParser
    {
        Dictionary<string, string> ParseAcfFile(string acfContent);
    }
}
