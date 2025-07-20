using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using SVC.Properties; // Adjust namespace if needed

namespace SVC.Core.Services
{
    public class SettingsService
    {
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