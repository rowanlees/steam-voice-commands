using SVC.Core.Services;
using SVC.Core.Services.Implementations;
using SVC.Core.SystemInterop.Implementations;
using System;
using System.Diagnostics;
using System.Windows;

namespace SVC.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize services and load game locations
            var gameLocationsService = new GameLocationsService(
                new GameManifestParser(),
                new SteamInstallationLocator(new ProcessWrapper(new Process()), 10000), // 10 seconds timeout
                new SteamLibraryReader(new FileSystem()),
                new GameRepository(new FileSystem())
            );

            try
            {
                gameLocationsService.BuildGameList();
            }
            catch (Exception ex)
            {
                // Log or show error but don’t crash app
            }

            // Then continue loading main window, etc.
        }
    }
}
