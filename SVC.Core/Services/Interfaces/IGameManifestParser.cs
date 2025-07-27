using SVC.Core.Model;

namespace SVC.Core.Services.Interfaces
{
    public interface IGameManifestParser
    {
        Game ParseAcfFile(string acfContent);
    }
}
