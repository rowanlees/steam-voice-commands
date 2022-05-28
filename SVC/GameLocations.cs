using System;
using System.Collections;
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
        String steamInstallQueryBat = "cmd /c REG QUERY HKCU\\SOFTWARE\\Valve\\Steam /f SteamExe >steaminstalllocation.txt";
        Dictionary<String, String> gamesList = new Dictionary<String, String>();
        List<String> libraryFolders = new List<String>();

        public void querySteamInstallLocation()
        {
            File.WriteAllText(currentDirectory + "\\steam_install_query.bat", steamInstallQueryBat);
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = currentDirectory;
                process.StartInfo.FileName = "steam_install_query.bat";
                process.Start();
                while(fileExists == false)
                {
                    if(File.Exists(currentDirectory + "\\steaminstalllocation.txt"))
                    {
                        fileExists = true;
                        process.WaitForExit();
                    }
                }
                writeSteamInstallLocation();
                readLibraryFolders();
            readManifestFiles();
        }

        private void writeSteamInstallLocation()
        {
                steamExeLocation = File.ReadAllText(currentDirectory + "\\steaminstalllocation.txt");
                steamExeLocation = steamExeLocation.TextAfter("SZ");
                steamExeLocation = steamExeLocation.GetUntilOrEmpty("End of search");
                steamExeLocation = steamExeLocation.Trim();
                steamFolderLocation = steamExeLocation.GetUntilOrEmpty("/steam.exe");
                File.WriteAllText(currentDirectory + "\\steamfolderlocation.txt", steamFolderLocation);
        }

        private void readLibraryFolders()
        {
            var lines = File.ReadLines(steamFolderLocation + "/steamapps/libraryfolders.vdf");
            foreach(String line in lines)
            {
                if (line.Contains("path"))
                {
                    String path = line.TextAfter("path");
                    path = path.TextAfter("\"");
                    path = path.Trim();
                    path = path.Replace("\"", "");
                    libraryFolders.Add(path);
                }
            }
            String libraryFoldersCombined = String.Join(",", libraryFolders);
            File.WriteAllText(currentDirectory + "\\steamlibraryfolders.txt", libraryFoldersCombined);
        }

        private void readManifestFiles()
        { 
            foreach (String library in libraryFolders)
            {
                string[] acfFiles = Directory.GetFiles(library + "\\\\" + "steamapps" + "\\\\", "*.acf");
                foreach (String acfFile in acfFiles)
                {
                    var lines = File.ReadAllText(acfFile);
                    String appid = lines.TextAfter("appid");
                    appid = appid.GetUntilOrEmpty("Universe");
                    appid = appid.Trim();
                    appid = appid.Replace("\"", "");
                    appid = appid.Trim();
                    appid = appid.Insert(0, "App ID: ");
                    String gameName = lines.TextAfter("steam.exe");
                    gameName = gameName.TextAfter("name");
                    gameName = gameName.GetUntilOrEmpty("StateFlags");
                    gameName = gameName.Replace("\"", "");
                    gameName = gameName.Trim();
                    gameName = gameName.Insert(0, "Game Name: ");

                    gamesList.Add(gameName, appid);
                }
            }
            using (StreamWriter file = new StreamWriter(currentDirectory + "\\gameslist.txt"))
                foreach (var entry in gamesList)
                    file.WriteLine("{0}\n{1}", entry.Key, entry.Value);
        }

    }
}
