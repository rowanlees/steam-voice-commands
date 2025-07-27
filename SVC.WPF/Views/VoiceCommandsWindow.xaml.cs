using System.Windows;

namespace SVC.WPF.Views
{
    /// <summary>
    /// Interaction logic for VoiceCommandsWindow.xaml
    /// </summary>
    public partial class VoiceCommandsWindow : Window
    {
        public VoiceCommandsWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
