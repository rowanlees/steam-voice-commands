using SVC.Core.Constants;
using SVC.Core.Events;
using SVC.Core.Services;
using SVC.WPF.ViewModels;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;

namespace SVC.WPF.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = new MainViewModel();
            InitializeComponent();
            SettingsService.Instance.KeybindChanged += SettingsService_KeybindChanged;
            SettingsService.Instance.KeybindDeleted += SettingsService_KeybindDeleted;
            DataContext = _viewModel;
            Loaded += (s, e) =>
            {
                var handle = new WindowInteropHelper(this).Handle;
                _viewModel.RegisterGlobalHotkey(handle);
                ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
            };
            Unloaded += (s, e) =>
            {
                var handle = new WindowInteropHelper(this).Handle;
                _viewModel.UnregisterGlobalHotkey(handle);
                ComponentDispatcher.ThreadPreprocessMessage -= ThreadPreprocessMessageMethod;
            };
        }

        private void SettingsService_KeybindDeleted(object sender, KeybindDeletedEventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            _viewModel.UnregisterGlobalHotkey(handle);
            _viewModel.DeleteSavedKeybindFields();
        }

        private void SettingsService_KeybindChanged(object sender, KeybindChangedEventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            _viewModel.RegisterGlobalHotkey(handle);
            _viewModel.UpdateSavedKeybindFields(e.ModifierKeys, e.NormalKeys);
        }

        private void ToggleRecognition_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleVoiceRecognitionCommand.Execute(null);
        }

        private void ShowCommands_Click(object sender, RoutedEventArgs e)
        {
            var window = new VoiceCommandsWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            // Get the current screen where MainWindow is
            var screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
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

        private void LoadedHandler(object sender, RoutedEventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            _viewModel.RegisterGlobalHotkey(handle);
        }

        private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            if (msg.message == WM_HOTKEY && (int)msg.wParam == HotkeyIds.VoiceRecognitionToggle)
            {
                _viewModel.ToggleVoiceRecognitionCommand.Execute(null);
                handled = true;
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new SettingsWindow
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.Manual
            };

            // Get the current screen where MainWindow is
            var screen = Screen.FromHandle(new WindowInteropHelper(this).Handle);
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
    }
}