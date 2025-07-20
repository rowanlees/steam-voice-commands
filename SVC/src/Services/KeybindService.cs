using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SVC.Core.Services
{
    public class KeybindService
    {
        private readonly List<Key> _modifierKeys = new List<Key>
        {
            Key.LeftCtrl,
            Key.RightCtrl,
            Key.LeftShift,
            Key.RightShift,
            Key.LeftAlt,
            Key.RightAlt,
            Key.LWin,
            Key.RWin
        };

        public bool IsModifier(Key key)
        {
            return _modifierKeys.Contains(key);
        }

        public bool IsRegularKey(Key key)
        {
            return !IsModifier(key);
        }

        // Converts a list of Keys into a display string like "Ctrl + Shift + A"
        public string FormatKeybind(List<Key> modifiers, Key mainKey)
        {
            var parts = new List<string>();

            foreach (var mod in modifiers)
            {
                parts.Add(KeyToString(mod));
            }

            parts.Add(KeyToString(mainKey));

            return string.Join(" + ", parts);
        }

        private string KeyToString(Key key)
        {
            switch (key)
            {
                case Key.LeftCtrl:
                case Key.RightCtrl:
                    return "Ctrl";
                case Key.LeftShift:
                case Key.RightShift:
                    return "Shift";
                case Key.LeftAlt:
                case Key.RightAlt:
                    return "Alt";
                case Key.LWin:
                case Key.RWin:
                    return "Win";
                default:
                    return key.ToString();
            }
        }

        public List<Key> ConvertFromSettingsList(System.Collections.ArrayList settingsList)
        {
            var keys = new List<Key>();
            foreach (var obj in settingsList)
            {
                if (obj is Key key)
                {
                    keys.Add(key);
                }
                else if (obj is string str)
                {
                    if (Enum.TryParse<Key>(str, out var parsedKey))
                    {
                        keys.Add(parsedKey);
                    }
                }
                else
                {
                    // Try to convert from int or other types if needed
                }
            }
            return keys;
        }

        public string FormatKeybindLabel(List<Key> modifiers, List<Key> keys)
        {
            var parts = new List<string>();
            foreach (var mod in modifiers)
            {
                parts.Add(KeyToString(mod));
            }
            foreach (var key in keys)
            {
                parts.Add(KeyToString(key));
            }
            return string.Join(" + ", parts);
        }
    }
}
