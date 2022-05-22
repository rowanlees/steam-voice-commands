using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SVC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameLocations gameLocations = new GameLocations();
            gameLocations.querySteamInstallLocation();
            gameLocations.writeSteamInstallLocaiton();
            gameLocations.readLibraryFolders();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new svcWindow());

        }

    }
}
