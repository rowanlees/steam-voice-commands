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
        private const int HOTKEY_ID = 9000; // Any unique ID
        private HotkeyService _hotkeyService = new HotkeyService();
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = new MainViewModel();
            InitializeComponent();
            KeybindTextBox.PreviewKeyDown += KeybindTextBox_PreviewKeyDown;
            KeybindTextBox.PreviewKeyUp += KeybindTextBox_PreviewKeyUp;
            Loaded += (s, e) =>
            {
                RegisterGlobalHotkey();
                ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
            };
            Unloaded += (s, e) =>
            {
                UnregisterGlobalHotkey();
                ComponentDispatcher.ThreadPreprocessMessage -= ThreadPreprocessMessageMethod;
            };
            DataContext = _viewModel;
        }

        private void KeybindTextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _viewModel.OnKeyUp(e);
        }

        private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            if (msg.message == WM_HOTKEY && (int)msg.wParam == HOTKEY_ID)
            {
                _viewModel.ToggleVoiceRecognitionCommand.Execute(null);
                handled = true;
            }
        }

        private void RegisterGlobalHotkey()
        {
            var helper = new System.Windows.Interop.WindowInteropHelper(this);
            var modifiers = GetModifierKeys(_viewModel.InputModifierKeys);
            if (_viewModel.InputKeybindKeys.Count > 0)
            {
                var mainKey = KeyInterop.VirtualKeyFromKey(_viewModel.InputKeybindKeys[0]);
                _hotkeyService.RegisterHotkey(helper.Handle, HOTKEY_ID, modifiers, mainKey);
            }
        }

        private void UnregisterGlobalHotkey()
        {
            var helper = new System.Windows.Interop.WindowInteropHelper(this);
            _hotkeyService.UnregisterHotkey(helper.Handle, HOTKEY_ID);
        }

        private int GetModifierKeys(ObservableCollection<Key> modifiers)
        {
            int result = 0;
            foreach (var key in modifiers)
            {
                switch (key)
                {
                    case Key.LeftCtrl:
                    case Key.RightCtrl:
                        result |= 0x0002; // MOD_CONTROL
                        break;
                    case Key.LeftAlt:
                    case Key.RightAlt:
                        result |= 0x0001; // MOD_ALT
                        break;
                    case Key.LeftShift:
                    case Key.RightShift:
                        result |= 0x0004; // MOD_SHIFT
                        break;
                    case Key.LWin:
                    case Key.RWin:
                        result |= 0x0008; // MOD_WIN
                        break;
                }
            }
            return result;
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
            UnregisterGlobalHotkey();
            RegisterGlobalHotkey();
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

        private void DeleteSavedKeybind_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteSavedKeybindCommand.Execute(null);
            UnregisterGlobalHotkey();
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
    }
}