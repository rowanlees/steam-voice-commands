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
            GameLocationsService gameLocations = new GameLocationsService(
                new GameManifestParser(),
                //10 second timeout looking for steam installation directory
                new SteamInstallationLocator(new ProcessWrapper(new System.Diagnostics.Process()), 10000)
            );
            gameLocations.QuerySteamInstallLocation();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new SvcWindow());
        }

    }
}
