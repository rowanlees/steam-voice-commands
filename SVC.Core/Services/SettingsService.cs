using SVC.Core.Events;
using SVC.Properties; // Adjust namespace if needed
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;

namespace SVC.Core.Services
{
    public class SettingsService
    {
        private static readonly Lazy<SettingsService> _instance = new Lazy<SettingsService>(() => new SettingsService());
        public static SettingsService Instance => _instance.Value;

        public event EventHandler<KeybindChangedEventArgs> KeybindChanged;
        public event EventHandler<KeybindDeletedEventArgs> KeybindDeleted;

        private SettingsService()
        {
            // Private constructor to prevent instantiation
        }
        public void SaveKeybind(List<Key> modifierKeys, List<Key> normalKeys)
        {
            var modifierCollection = new StringCollection();
            foreach (var key in modifierKeys)
            {
                modifierCollection.Add(key.ToString());
            }
            Settings.Default.VoiceActivateKeybindModifiers = modifierCollection;

            var keyCollection = new StringCollection();
            foreach (var key in normalKeys)
            {
                keyCollection.Add(key.ToString());
            }
            Settings.Default.VoiceActivateKeybindKey = keyCollection;

            Settings.Default.Save();

            KeybindChanged?.Invoke(this, new KeybindChangedEventArgs(modifierKeys, normalKeys));
        }

        public void DeleteKeybind()
        {
            Settings.Default.VoiceActivateKeybindModifiers = new StringCollection();
            Settings.Default.VoiceActivateKeybindKey = new StringCollection();
            Settings.Default.Save();
            KeybindDeleted?.Invoke(this, new KeybindDeletedEventArgs());
        }

        public (List<Key> Modifiers, List<Key> Keys) LoadKeybind()
        {
            var modifierKeys = new List<Key>();
            var keybindKeys = new List<Key>();

            if (Settings.Default.VoiceActivateKeybindModifiers != null)
            {
                foreach (var keyString in Settings.Default.VoiceActivateKeybindModifiers)
                {
                    if (Enum.TryParse(keyString, out Key parsedKey))
                    {
                        modifierKeys.Add(parsedKey);
                    }
                }
            }

            if (Settings.Default.VoiceActivateKeybindKey != null)
            {
                foreach (var keyString in Settings.Default.VoiceActivateKeybindKey)
                {
                    if (Enum.TryParse(keyString, out Key parsedKey))
                    {
                        keybindKeys.Add(parsedKey);
                    }
                }
            }

            return (modifierKeys, keybindKeys);
        }

        public bool GetAutoStartListening()
        {
            return Settings.Default.AutoListenOnLaunch;
        }

        public void SetAutoStartListening(bool value)
        {
            Settings.Default.AutoListenOnLaunch = value;
            Settings.Default.Save();
        }
    }
}