using SVC.Core.Services.Implementations;
using SVC.Core.SystemInterop.Implementations;
using SVC.src.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SVC.WPF.src.Views
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
