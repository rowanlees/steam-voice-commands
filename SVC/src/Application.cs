using SVC.src.Services;
using SVC.src.Services.Implementations;
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
            var fileSystem = new FileSystem();
            GameLocationsService gameLocationsService = new GameLocationsService(
                new GameManifestParser(),
                //10 second timeout looking for steam installation directory
                new SteamInstallationLocator(new ProcessWrapper(new System.Diagnostics.Process()), 10000),
                new SteamLibraryReader(fileSystem),
                new GameRepository(fileSystem)
            );
            gameLocationsService.BuildGameList();
            VoiceRecognitionService voiceRecognition = new VoiceRecognitionService();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new SvcWindow(voiceRecognition));
        }

    }
}
