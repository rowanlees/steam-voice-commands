using SVC.src.Services;
using System;


namespace SVC
{
    public static class Application
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameLocationsService gameLocations = new GameLocationsService(new GameManifestParser());
            gameLocations.QuerySteamInstallLocation();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new SvcWindow());
        }

    }
}
