using SVC.Core.Constants;
using SVC.Core.Services;
using SVC.WPF.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace SVC.WPF.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly SettingsService _settingsService;
        private readonly KeybindService _keybindService;
        private bool _wasKeyUp = true; // tracks if keys were released
        private readonly DispatcherTimer _saveMessageTimer;
        private readonly HotkeyService _hotkeyService;

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

        public SettingsViewModel()
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
            _settingsService = SettingsService.Instance;
            _keybindService = new KeybindService();
            _hotkeyService = new HotkeyService();

            SaveKeybindCommand = new RelayCommand(_ => SaveKeybind());
            ClearKeybindCommand = new RelayCommand(_ => ClearKeybind());
            DeleteSavedKeybindCommand = new RelayCommand(_ => DeleteSavedKeybind());

            LoadSettings();
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
                SavedModifierKeys.Add(key);
            }
            foreach (var key in keys)
            {
                SavedKeybindKeys.Add(key);
            }
            UpdateKeybindDisplayText();
            UpdatePrefixSavedKeybindText();
            UpdateSavedKeybindDisplayText();
            UpdateCanSaveKeybind();
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
            UpdatePrefixSavedKeybindText();
            UpdateSavedKeybindDisplayText();
            KeybindSaveMessage = "Keybind saved!";
            _saveMessageTimer.Stop();
            _saveMessageTimer.Start();
        }

        public void DeleteSavedKeybind()
        {
            _settingsService.DeleteKeybind();
            SavedModifierKeys.Clear();
            SavedKeybindKeys.Clear();
            UpdatePrefixSavedKeybindText();
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
