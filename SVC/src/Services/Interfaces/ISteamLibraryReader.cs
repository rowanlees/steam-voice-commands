using System.Collections.Generic;

namespace SVC.src.Services.Interfaces
{
    public interface ISteamLibraryReader
    {
        List<string> GetLibraryFolders(string steamFolderPath);
    }
}
