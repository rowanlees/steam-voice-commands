using System;
using System.Runtime.InteropServices;

namespace SVC.Core.Services
{
    public class HotkeyService
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public void RegisterHotkey(IntPtr handle, int id, int modifiers, int key) =>
            RegisterHotKey(handle, id, modifiers, key);

        public void UnregisterHotkey(IntPtr handle, int id) =>
            UnregisterHotKey(handle, id);
    }
}
