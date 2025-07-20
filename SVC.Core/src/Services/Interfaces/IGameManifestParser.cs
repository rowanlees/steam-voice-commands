using SVC.src.Model;

namespace SVC.src.Services.Interfaces
{
    public interface IGameManifestParser
    {
        Game ParseAcfFile(string acfContent);
    }
}
