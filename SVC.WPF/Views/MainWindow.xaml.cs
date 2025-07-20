using SVC.WPF.ViewModels;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace SVC.WPF.Views
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

        private void KeybindTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = true;
            _viewModel.OnKeyDown(e);
        }

        private void ToggleRecognition_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleVoiceRecognitionCommand.Execute(null);
        }

        private void SaveKeybind_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveKeybindCommand.Execute(null);
        }

        private void ClearKeybind_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearKeybindCommand.Execute(null);
        }

        private void ShowCommands_Click(object sender, RoutedEventArgs e)
        {
            var window = new VoiceCommandsWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            // Get the current screen where MainWindow is
            var screen = Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle);
            var workingArea = screen.WorkingArea;

            // Convert screen pixels to WPF units (96 DPI is default for WPF)
            double screenRight = workingArea.Right / (96.0 / VisualTreeHelper.GetDpi(this).PixelsPerInchX);
            double screenLeft = workingArea.Left / (96.0 / VisualTreeHelper.GetDpi(this).PixelsPerInchX);

            double targetLeft = this.Left + this.Width + 10;

            // If the window would overflow the screen width, open to the left instead
            if (targetLeft + window.Width > screenRight)
            {
                targetLeft = this.Left - window.Width - 10;

                // Ensure it doesn't go off the left edge either
                if (targetLeft < screenLeft)
                {
                    targetLeft = screenLeft; // fallback: just snap to screen edge
                }
            }

            window.Left = targetLeft;
            window.Top = this.Top;

            window.Show();
        }

        private void ShowGames_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowInstalledGamesCommand.Execute(null);
        }
    }
}