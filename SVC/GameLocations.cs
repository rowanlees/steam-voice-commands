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
        String[] libraryFolders;

        public void querySteamInstallLocation()
        {
            try
            {
                String currentDirectory = Directory.GetCurrentDirectory();
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = currentDirectory;
                process.StartInfo.FileName = "steam_install_query.bat";
                process.Start();
                process.WaitForExit(100);
            }
            catch (Exception ex)
            {
              
            }
        }

        public void writeSteamInstallLocaiton()
        {
            try
            {
                steamExeLocation = File.ReadAllText("steam_install_location.txt");
                steamExeLocation = steamExeLocation.TextAfter("SZ");
                steamExeLocation = steamExeLocation.GetUntilOrEmpty("End of search");
                steamExeLocation = steamExeLocation.Trim();
                steamFolderLocation = steamExeLocation.GetUntilOrEmpty("/steam.exe");
            }
            catch (Exception ex)
            {

            }
        }

        public void readLibraryFolders()
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
            String currentDirectory = Directory.GetCurrentDirectory();
            String libraryFoldersCombined = String.Join(",", libraryFolders);
            File.WriteAllText(libraryFoldersCombined, currentDirectory + "libraryfolders.txt");
        }

    }
}
