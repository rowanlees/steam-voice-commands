using SVC.Core.Model;
using SVC.Core.Repositories.Implementations;
using SVC.Core.SystemInterop.Implementations;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SVC.WPF.Views
{
    /// <summary>
    /// Interaction logic for InstalledGamesWindow.xaml
    /// </summary>
    public partial class InstalledGamesWindow : Window
    {
        public InstalledGamesWindow()
        {
            InitializeComponent();

            // Setup service and load games
            var fileSystem = new FileSystem(); // Your IFileSystem implementation
            var gameRepo = new GameRepository(fileSystem);

            List<Game> games = gameRepo.LoadGames();
            games.Sort((x, y) => string.Compare(x.GameName, y.GameName, StringComparison.OrdinalIgnoreCase));

            GamesListBox.ItemsSource = games;
        }
    }
}
