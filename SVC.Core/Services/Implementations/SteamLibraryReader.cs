using SVC.Core.Extensions;
using SVC.Core.Services.Interfaces;
using SVC.Core.SystemInterop.Interface;
using System.Collections.Generic;

namespace SVC.Core.Services.Implementations
{
    public class SteamLibraryReader : ISteamLibraryReader
    {
        private readonly IFileSystem _fileReader;
        public SteamLibraryReader(IFileSystem fileReader)
        {
            _fileReader = fileReader;
        }
        public List<string> GetLibraryFolders(string steamFolderPath)
        {
            List<string> libraryFolders = new List<string>();
            var lines = _fileReader.ReadLines(steamFolderPath + "/steamapps/libraryfolders.vdf");
            foreach (string line in lines)
            {
                if (line.Contains("path"))
                {
                    string path = line.TextAfter("path").TextAfter("\"").Trim().Replace("\"", "");
                    libraryFolders.Add(path);
                }
            }
            return libraryFolders;
        }
    }
}
