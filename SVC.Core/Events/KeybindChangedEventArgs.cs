using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SVC.Core.Events
{
    public class KeybindChangedEventArgs : EventArgs
    {
        public List<Key> ModifierKeys { get; }
        public List<Key> NormalKeys { get; }

        public KeybindChangedEventArgs(List<Key> modifierKeys, List<Key> normalKeys)
        {
            ModifierKeys = modifierKeys;
            NormalKeys = normalKeys;
        }
    }
}
