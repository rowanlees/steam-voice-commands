using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVC
{
    internal class GameLocations
    {
        String steamExeLocation;
        String steamFolderLocation;
        String currentDirectory = Directory.GetCurrentDirectory();
        bool fileExists = false;

        public void querySteamInstallLocation()
        {
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = currentDirectory;
                process.StartInfo.FileName = "steam_install_query.bat";
                process.Start();
                while(fileExists == false)
                {
                    if(File.Exists(currentDirectory + "\\steam_install_location.txt"))
                    {
                        fileExists = true;
                    }
                }
                writeSteamInstallLocaiton();
                readLibraryFolders();

        }

        private void writeSteamInstallLocaiton()
        {
                steamExeLocation = File.ReadAllText(currentDirectory + "\\steam_install_location.txt");
                steamExeLocation = steamExeLocation.TextAfter("SZ");
                steamExeLocation = steamExeLocation.GetUntilOrEmpty("End of search");
                steamExeLocation = steamExeLocation.Trim();
                steamFolderLocation = steamExeLocation.GetUntilOrEmpty("/steam.exe");
                File.WriteAllText(currentDirectory + "\\steam_folder_location.txt", steamFolderLocation);
        }

        private void readLibraryFolders()
        {
            var lines = File.ReadLines(steamFolderLocation + "/steamapps/libraryfolders.vdf");
            List<String> libraryFolders = new List<String>();
            foreach(String line in lines)
            {
                if (line.Contains("path"))
                {
                    libraryFolders.Add(line);
                }
            }
            String libraryFoldersCombined = String.Join(",", libraryFolders);
            File.WriteAllText(currentDirectory + "\\libraryfolders.txt", libraryFoldersCombined);
        }

    }
}
