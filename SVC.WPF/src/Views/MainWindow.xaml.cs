using SVC.WPF.src.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SVC.WPF
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        // Attach this to the TextBox that receives keybind input
        private void KeybindTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; // Prevent input from showing in TextBox
            _viewModel.OnKeyDown(e);
        }

        private void ToggleRecognition_Click(object sender, RoutedEventArgs e)
        {
            // Your toggle voice recognition logic here
        }

        private void SaveKeybind_Click(object sender, RoutedEventArgs e)
        {
            // Your save keybind logic here
        }

        private void ClearKeybind_Click(object sender, RoutedEventArgs e)
        {
            // Your clear keybind logic here
        }

        private void ShowCommands_Click(object sender, RoutedEventArgs e)
        {
            // Your show voice commands logic here
        }

        private void ShowGames_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowInstalledGamesCommand.Execute(null);
        }
    }
}