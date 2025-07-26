using Microsoft.Win32;
using SVC.Core.Repositories.Implementations;
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
            DetectThemeAndMergeResourceDictionary();
            BuildGamesList();
            var mainWindow = new Views.MainWindow();
            mainWindow.Show();
        }

        private static void BuildGamesList()
        {
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
            catch (Exception)
            {
                // TODO: Log or show error but don’t crash app
            }

            // Then continue loading main window, etc.
        }

        private void DetectThemeAndMergeResourceDictionary()
        {
            bool isLightTheme = true;
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("AppsUseLightTheme");
                        if (value is int intValue)
                            isLightTheme = intValue == 1;
                    }
                }
            }
            catch { /* Handle exceptions if needed */ }

            string themeDict = isLightTheme ? "Themes/LightTheme.xaml" : "Themes/DarkTheme.xaml";
            var dict = new ResourceDictionary { Source = new Uri(themeDict, UriKind.Relative) };
            Resources.MergedDictionaries.Add(dict);
        }
    }
}
