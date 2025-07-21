using SVC.Core.Services;
using SVC.WPF.src;
using SVC.WPF.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace SVC.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly VoiceRecognitionService _voiceRecognitionService;
        private readonly SettingsService _settingsService;
        private readonly KeybindService _keybindService;
        private bool _wasKeyUp = true; // tracks if keys were released
        private readonly DispatcherTimer _saveMessageTimer;

        public ObservableCollection<Key> InputModifierKeys { get; } = new ObservableCollection<Key>();
        public ObservableCollection<Key> InputKeybindKeys { get; } = new ObservableCollection<Key>();
        public ObservableCollection<Key> SavedModifierKeys { get; } = new ObservableCollection<Key>();
        public ObservableCollection<Key> SavedKeybindKeys { get; } = new ObservableCollection<Key>();

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

        private string _keybindDisplayText;
        public string KeybindDisplayText
        {
            get => _keybindDisplayText;
            set
            {
                if (_keybindDisplayText != value)
                {
                    _keybindDisplayText = value;
                    OnPropertyChanged(nameof(KeybindDisplayText));
                }
            }
        }

        private string _savedKeybindDisplayText;
        public string SavedKeybindDisplayText
        {
            get => _savedKeybindDisplayText;
            set
            {
                if (_savedKeybindDisplayText != value)
                {
                    _savedKeybindDisplayText = value;
                    OnPropertyChanged(nameof(SavedKeybindDisplayText));
                }
            }
        }

        private string _keybindSaveMessage;
        public string KeybindSaveMessage
        {
            get => _keybindSaveMessage;
            set
            {
                _keybindSaveMessage = value;
                OnPropertyChanged(nameof(KeybindSaveMessage));
            }
        }

        private bool _canSaveKeybind;
        public bool CanSaveKeybind
        {
            get => _canSaveKeybind;
            set
            {
                if (_canSaveKeybind != value)
                {
                    _canSaveKeybind = value;
                    OnPropertyChanged(nameof(CanSaveKeybind));
                }
            }
        }

        public ICommand SaveKeybindCommand { get; }
        public ICommand ClearKeybindCommand { get; }
        public ICommand ToggleVoiceRecognitionCommand { get; }
        public ICommand ShowInstalledGamesCommand { get; }
        public ICommand ShowVoiceCommandsCommand { get; }
        public ICommand DeleteSavedKeybindCommand { get; }

        public MainViewModel()
        {
            _saveMessageTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            _saveMessageTimer.Tick += (s, e) =>
            {
                KeybindSaveMessage = string.Empty;
                _saveMessageTimer.Stop();
            };
            _voiceRecognitionService = new VoiceRecognitionService();
            _settingsService = new SettingsService();
            _keybindService = new KeybindService();

            _voiceRecognitionService.CommandRecognized += OnVoiceCommandRecognized;

            SaveKeybindCommand = new RelayCommand(_ => SaveKeybind());
            ClearKeybindCommand = new RelayCommand(_ => ClearKeybind());
            ToggleVoiceRecognitionCommand = new RelayCommand(_ => ToggleVoiceRecognition());
            ShowInstalledGamesCommand = new RelayCommand(_ => ShowInstalledGames());
            DeleteSavedKeybindCommand = new RelayCommand(_ => DeleteSavedKeybind());

            LoadSettings();

            if (AutoStartListening)
            {
                IsVoiceRecognitionActive = true;
                _voiceRecognitionService.Start();
            }

            _voiceRecognitionService.LoadSpeechRecognition();
        }

        private void UpdateCanSaveKeybind()
        {
            // You can tweak the logic to enforce minimum or maximum modifiers
            CanSaveKeybind = InputModifierKeys.Any() && InputKeybindKeys.Count == 1;
        }

        private void LoadSettings()
        {
            AutoStartListening = _settingsService.GetAutoStartListening();

            var (modifiers, keys) = _settingsService.LoadKeybind();
            InputModifierKeys.Clear();
            InputKeybindKeys.Clear();
            foreach (var key in modifiers)
            {
                InputModifierKeys.Add(key);
                SavedModifierKeys.Add(key);
            }
            foreach (var key in keys)
            {
                InputKeybindKeys.Add(key);
                SavedModifierKeys.Add(key);
            }

            UpdateKeybindDisplayText();
            UpdateSavedKeybindDisplayText();
            UpdateCanSaveKeybind();
        }

        private void OnVoiceCommandRecognized(string command)
        {
            RecognizedCommand = $"Current voice command: {command}";
        }

        private void SaveKeybind()
        {
            _settingsService.SaveKeybind(InputModifierKeys.ToList(), InputKeybindKeys.ToList());
            SavedModifierKeys.Clear();
            foreach (var key in InputModifierKeys)
            {
                SavedModifierKeys.Add(key);
            }
            SavedKeybindKeys.Clear();
            foreach (var key in InputKeybindKeys)
            {
                SavedKeybindKeys.Add(key);
            }
            UpdateSavedKeybindDisplayText();
            KeybindSaveMessage = "Keybind saved!";
            _saveMessageTimer.Stop();
            _saveMessageTimer.Start();
        }

        public void DeleteSavedKeybind()
        {
            SavedKeybindDisplayText = string.Empty;
            KeybindSaveMessage = "Saved keybind deleted.";
            _saveMessageTimer.Stop();
            _saveMessageTimer.Start();
        }

        private void ClearKeybind()
        {
            InputModifierKeys.Clear();
            InputKeybindKeys.Clear();
            UpdateKeybindDisplayText();
            UpdateCanSaveKeybind();
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

        public void OnKeyDown(KeyEventArgs e)
        {
            if (_wasKeyUp)
            {
                InputModifierKeys.Clear();
                InputKeybindKeys.Clear();
                _wasKeyUp = false;
            }

            if (_keybindService.IsModifier(e.Key))
            {
                if (!InputModifierKeys.Contains(e.Key) && InputModifierKeys.Count < 3)
                    InputModifierKeys.Add(e.Key);
            }
            else if (!InputKeybindKeys.Contains(e.Key))
            {
                InputKeybindKeys.Clear();
                InputKeybindKeys.Add(e.Key);
            }
            else
            {
                InputKeybindKeys.Clear(); // only allow 1 main key
                InputKeybindKeys.Add(e.Key);
            }

            UpdateKeybindDisplayText();
            UpdateCanSaveKeybind();
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            _wasKeyUp = true;
        }

        private void UpdateKeybindDisplayText()
        {
            KeybindDisplayText = _keybindService.FormatKeybindLabel(InputModifierKeys.ToList(), InputKeybindKeys.ToList());
        }

        private void UpdateSavedKeybindDisplayText()
        {
            SavedKeybindDisplayText = _keybindService.FormatKeybindLabel(SavedModifierKeys.ToList(), SavedKeybindKeys.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
