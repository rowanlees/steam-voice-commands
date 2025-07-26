using SVC.WPF.ViewModels;
using System.Windows;
using System.Windows.Interop;

namespace SVC.WPF.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly SettingsViewModel _viewModel;
        public SettingsWindow()
        {
            _viewModel = new SettingsViewModel();
            InitializeComponent();
            KeybindTextBox.PreviewKeyDown += KeybindTextBox_PreviewKeyDown;
            KeybindTextBox.PreviewKeyUp += KeybindTextBox_PreviewKeyUp;
            DataContext = _viewModel;
            InitializeComponent();
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
                _viewModel.AutoStartListening = (bool)checkBox.IsChecked;

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
