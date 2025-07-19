using System.Collections.Generic;

namespace SVC.src.Services.Interfaces
{
    internal interface ISteamLibraryReader
    {
        List<string> GetLibraryFolders(string steamFolderPath);
    }
}
