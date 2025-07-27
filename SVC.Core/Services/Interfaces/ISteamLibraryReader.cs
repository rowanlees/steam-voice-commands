using System.Collections.Generic;

namespace SVC.Core.Services.Interfaces
{
    public interface ISteamLibraryReader
    {
        List<string> GetLibraryFolders(string steamFolderPath);
    }
}
