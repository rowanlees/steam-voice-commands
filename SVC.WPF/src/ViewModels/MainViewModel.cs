using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SVC.Core.Services;
using SVC.WPF.src.Views;
using SVC.WPF.src;
using System.ComponentModel;

namespace SVC.WPF.src.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly VoiceRecognitionService _voiceRecognitionService;
        private readonly SettingsService _settingsService;
        private readonly KeybindService _keybindService;

        public ObservableCollection<Key> ModifierKeys { get; } = new ObservableCollection<Key>();
        public ObservableCollection<Key> KeybindKeys { get; } = new ObservableCollection<Key>();

        private string _recognizedCommand;
        public string RecognizedCommand
        {
            get => _recognizedCommand;
            set
            {
                if (_recognizedCommand != value)
                {
                    _recognizedCommand = value;
                    OnPropertyChanged(nameof(RecognizedCommand));
                }
            }
        }

        private bool _isVoiceRecognitionActive;
        public bool IsVoiceRecognitionActive
        {
            get => _isVoiceRecognitionActive;
            set
            {
                if (_isVoiceRecognitionActive != value)
                {
                    _isVoiceRecognitionActive = value;
                    OnPropertyChanged(nameof(IsVoiceRecognitionActive));

                    if (value)
                        _voiceRecognitionService.Start();
                    else
                        _voiceRecognitionService.Stop();
                }
            }
        }

        private bool _autoStartListening;
        public bool AutoStartListening
        {
            get => _autoStartListening;
            set
            {
                if (_autoStartListening != value)
                {
                    _autoStartListening = value;
                    _settingsService.SetAutoStartListening(value);
                    OnPropertyChanged(nameof(AutoStartListening));
                }
            }
        }

        public ICommand SaveKeybindCommand { get; }
        public ICommand ClearKeybindCommand { get; }
        public ICommand ToggleVoiceRecognitionCommand { get; }
        public ICommand ShowInstalledGamesCommand { get; }
        public ICommand ShowVoiceCommandsCommand { get; }

        public MainViewModel()
        {
            _voiceRecognitionService = new VoiceRecognitionService();
            _settingsService = new SettingsService();
            _keybindService = new KeybindService();

            _voiceRecognitionService.CommandRecognized += OnVoiceCommandRecognized;

            SaveKeybindCommand = new RelayCommand(_ => SaveKeybind());
            ClearKeybindCommand = new RelayCommand(_ => ClearKeybind());
            ToggleVoiceRecognitionCommand = new RelayCommand(_ => ToggleVoiceRecognition());
            ShowInstalledGamesCommand = new RelayCommand(_ => ShowInstalledGames());
            ShowVoiceCommandsCommand = new RelayCommand(_ => ShowVoiceCommands());

            LoadSettings();

            if (AutoStartListening)
            {
                IsVoiceRecognitionActive = true;
                _voiceRecognitionService.Start();
            }

            _voiceRecognitionService.LoadSpeechRecognition();
        }

        private void LoadSettings()
        {
            AutoStartListening = _settingsService.GetAutoStartListening();

            var (modifiers, keys) = _settingsService.LoadKeybind();
            ModifierKeys.Clear();
            KeybindKeys.Clear();
            foreach (var key in modifiers)
                ModifierKeys.Add(key);
            foreach (var key in keys)
                KeybindKeys.Add(key);
        }

        private void OnVoiceCommandRecognized(string command)
        {
            RecognizedCommand = $"Current voice command: {command}";
        }

        private void SaveKeybind()
        {
            _settingsService.SaveKeybind(ModifierKeys.ToList(), KeybindKeys.ToList());
        }

        private void ClearKeybind()
        {
            ModifierKeys.Clear();
            KeybindKeys.Clear();
        }

        private void ToggleVoiceRecognition()
        {
            IsVoiceRecognitionActive = !IsVoiceRecognitionActive;
        }

        private void ShowInstalledGames()
        {
            var gamesWindow = new InstalledGamesWindow();
            gamesWindow.Show();
        }

        private void ShowVoiceCommands()
        {
            // Open your voice command list window (not yet implemented here)
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            if (_keybindService.IsModifier(e.Key))
            {
                if (!ModifierKeys.Contains(e.Key))
                    ModifierKeys.Add(e.Key);
            }
            else
            {
                if (!KeybindKeys.Contains(e.Key))
                    KeybindKeys.Add(e.Key);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
