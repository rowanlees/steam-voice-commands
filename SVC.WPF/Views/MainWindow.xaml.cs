using SVC.Core.Constants;
using SVC.Core.Services;
using SVC.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
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
            KeybindTextBox.PreviewKeyDown += KeybindTextBox_PreviewKeyDown;
            KeybindTextBox.PreviewKeyUp += KeybindTextBox_PreviewKeyUp;
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

        private void KeybindTextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _viewModel.OnKeyUp(e);
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
            var handle = new WindowInteropHelper(this).Handle;
            _viewModel.UnregisterGlobalHotkey(handle);
            _viewModel.RegisterGlobalHotkey(handle);
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

        private void DeleteSavedKeybind_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteSavedKeybindCommand.Execute(null);
            var handle = new WindowInteropHelper(this).Handle;
            _viewModel.UnregisterGlobalHotkey(handle);
        }

        private void AutoStartCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is System.Windows.Controls.CheckBox checkBox)
            {
                _viewModel.AutoStartListening = (bool) checkBox.IsChecked;

            }
        }

        private void AutoStartCheckbox_Initialized(object sender, System.EventArgs e)
        {
            if (sender is System.Windows.Controls.CheckBox checkBox)
            {
                checkBox.IsChecked = _viewModel.AutoStartListening;
            }

        }

        private void LoadedHandler(object sender, RoutedEventArgs e)
        {
            var handle = new System.Windows.Interop.WindowInteropHelper(this).Handle;
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
    }
}