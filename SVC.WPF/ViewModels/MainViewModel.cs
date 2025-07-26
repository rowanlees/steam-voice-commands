using SVC.Core.Constants;
using SVC.Core.Services;
using SVC.WPF.src;
using SVC.WPF.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SVC.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly VoiceRecognitionService _voiceRecognitionService;
        private readonly SettingsService _settingsService;
        private readonly KeybindService _keybindService;
        private readonly HotkeyService _hotkeyService;

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
                    {
                        _voiceRecognitionService.Start();
                        VoiceRecognitionActiveLabel = "ACTIVE";
                    }
                    else
                    {
                        _voiceRecognitionService.Stop();
                        VoiceRecognitionActiveLabel = "INACTIVE";
                    }

                }
            }
        }

        private string _voiceRecognitionActiveLabel;
        public string VoiceRecognitionActiveLabel
        {
            get => _voiceRecognitionActiveLabel;
            set
            {
                if (_voiceRecognitionActiveLabel != value)
                {
                    _voiceRecognitionActiveLabel = value;
                    OnPropertyChanged(nameof(VoiceRecognitionActiveLabel));
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

        private string _prefixSavedKeybindDisplayText;
        public string PrefixSavedKeybindDisplayText
        {
            get => _prefixSavedKeybindDisplayText;
            set
            {
                if (_prefixSavedKeybindDisplayText != value)
                {
                    _prefixSavedKeybindDisplayText = value;
                    OnPropertyChanged(nameof(PrefixSavedKeybindDisplayText));
                }
            }
        }
        public ICommand ToggleVoiceRecognitionCommand { get; }
        public ICommand ShowInstalledGamesCommand { get; }
        public ICommand ShowVoiceCommandsCommand { get; }

        public MainViewModel()
        {
            _voiceRecognitionService = new VoiceRecognitionService();
            _settingsService = SettingsService.Instance;
            _keybindService = new KeybindService();
            _hotkeyService = new HotkeyService();

            _voiceRecognitionService.CommandRecognized += OnVoiceCommandRecognized;

            ToggleVoiceRecognitionCommand = new RelayCommand(_ => ToggleVoiceRecognition());
            ShowInstalledGamesCommand = new RelayCommand(_ => ShowInstalledGames());

            LoadSettings();

            if (AutoStartListening)
            {
                IsVoiceRecognitionActive = true;
                _voiceRecognitionService.Start();
                VoiceRecognitionActiveLabel = "ACTIVE";
            }
            else
            {
                IsVoiceRecognitionActive = false;
                VoiceRecognitionActiveLabel = "INACTIVE";
            }
            _voiceRecognitionService.LoadSpeechRecognition();
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

        public void RegisterGlobalHotkey(IntPtr windowHandle)
        {
            var modifiers = GetModifierKeys(SavedModifierKeys);
            if (SavedKeybindKeys.Count > 0)
            {
                var mainKey = KeyInterop.VirtualKeyFromKey(SavedKeybindKeys[0]);
                _hotkeyService.RegisterHotkey(windowHandle, HotkeyIds.VoiceRecognitionToggle, modifiers, mainKey);
            }
        }

        public void UnregisterGlobalHotkey(IntPtr windowHandle)
        {
            _hotkeyService.UnregisterHotkey(windowHandle, HotkeyIds.VoiceRecognitionToggle);
        }

        private void LoadSettings()
        {
            AutoStartListening = _settingsService.GetAutoStartListening();

            var (modifiers, keys) = _settingsService.LoadKeybind();
            UpdateSavedKeybindFields(modifiers, keys);
        }

        public void UpdateSavedKeybindFields(System.Collections.Generic.List<Key> modifiers, System.Collections.Generic.List<Key> keys)
        {
            UpdateSavedKeybinds(modifiers, keys);
            UpdatePrefixSavedKeybindText();
            UpdateSavedKeybindDisplayText();
        }

        private void UpdateSavedKeybinds(System.Collections.Generic.List<Key> modifiers, System.Collections.Generic.List<Key> keys)
        {
            SavedModifierKeys.Clear();
            SavedKeybindKeys.Clear();
            foreach (var key in modifiers)
            {
                SavedModifierKeys.Add(key);
            }
            foreach (var key in keys)
            {
                SavedKeybindKeys.Add(key);
            }
        }

        private void UpdatePrefixSavedKeybindText()
        {
            if (SavedModifierKeys.Count > 0 || SavedKeybindKeys.Count > 0)
            {
                PrefixSavedKeybindDisplayText = "Current saved keybind:";
            }
            else
            {
                PrefixSavedKeybindDisplayText = "No saved keybind.";
            }
        }

        private void OnVoiceCommandRecognized(string command)
        {
            RecognizedCommand = command;
            var normalized = command.Trim().ToLowerInvariant();

            if (normalized.Equals(VoiceCommands.StopVoiceRecognition) || normalized.Equals(VoiceCommands.StopVoiceCommands))
            {
                IsVoiceRecognitionActive = false;
            }
            else if (normalized.Equals(VoiceCommands.StartVoiceCommands) || normalized.Equals(VoiceCommands.StartVoiceRecognition))
            {
                IsVoiceRecognitionActive = true;
            }
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

        private void UpdateSavedKeybindDisplayText()
        {
            SavedKeybindDisplayText = _keybindService.FormatKeybindLabel(SavedModifierKeys.ToList(), SavedKeybindKeys.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void DeleteSavedKeybindFields()
        {
            SavedModifierKeys.Clear();
            SavedKeybindKeys.Clear();
            UpdatePrefixSavedKeybindText();
            UpdateSavedKeybindDisplayText();
        }
    }
}
