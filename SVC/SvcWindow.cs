using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.Collections;
using SVC.Properties;

namespace SVC
{
    public partial class SvcWindow : Form
    {

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public static SvcWindow currentForm;

        const int MYACTION_HOTKEY_ID = 1;

        private ArrayList keyBindValue = new ArrayList();
        private ArrayList keyBindModifierValues = new ArrayList();


        public SvcWindow()
        {
            currentForm = this;
            InitializeComponent();
            setGlobalHotkey();
            if(ActivateButton.Text.Equals("Stop voice commands"))
            {
                this.Icon = Resources.SVCRecording;
            }
            if (Settings.Default.AutoListenOnLaunch == false)
            {
                autoListenCheckBox.Checked = false;
                ActivateButton.Text = "Start voice commands";
                voiceRecognition.stop();
                this.Icon = Resources.SVCIcon;
            }
            if(Settings.Default.AutoListenOnLaunch == true)
            {
                autoListenCheckBox.Checked = true;
                ActivateButton.Text = "Stop voice commands";
                voiceRecognition.start();
                this.Icon = Resources.SVCRecording;
            }
            if (Settings.Default.VoiceActivateKeybindModifiers != null && Settings.Default.VoiceActivateKeybindKey != null && Settings.Default.KeyBindLabel != null)
            {
                savedKeybindLabel.Text = Settings.Default.KeyBindLabel;
            }
        }

        private void setGlobalHotkey()
        {
            if (Settings.Default.VoiceActivateKeybindModifiers != null && Settings.Default.VoiceActivateKeybindKey != null)
            {
                int key = (int)Settings.Default.VoiceActivateKeybindKey[0];
                int modifierSumValue = 0;
                KeysConverter keysConverter = new KeysConverter();
                foreach (var item in keyBindModifierValues)
                {
                    Keys keyconverted = (Keys)keysConverter.ConvertFromString(item.ToString());    
                    switch (keyconverted)
                    {
                        case (Keys.Alt):
                            modifierSumValue += 1;
                            break;
                        case (Keys.Menu):
                            modifierSumValue += 1;
                            break;
                        case (Keys.LMenu):
                            modifierSumValue += 1;
                            break;
                        case (Keys.RMenu):
                            modifierSumValue += 1;
                            break;
                        case (Keys.ControlKey):
                            modifierSumValue += 2;
                            break;
                        case (Keys.Control):
                            modifierSumValue += 2;
                            break;
                        case (Keys.LControlKey):
                            modifierSumValue += 2;
                            break;
                        case (Keys.RControlKey):
                            modifierSumValue += 2;
                            break;
                        case (Keys.Shift):
                            modifierSumValue += 4;
                            break;
                        case (Keys.ShiftKey): 
                            modifierSumValue += 4;
                            break;
                        case (Keys.LShiftKey):
                            modifierSumValue += 4;
                            break;
                        case (Keys.RShiftKey):
                            modifierSumValue += 4;
                            break;
                        default:
                            break;
                    }
                    
                }
                RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, modifierSumValue, key);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == MYACTION_HOTKEY_ID)
            {
                // My hotkey has been typed
                toggleStopAndStartVoiceCommands();

            }
            base.WndProc(ref m);
        }

        public void setActivateButtonText(String text)
        {
            ActivateButton.Text = text;
        }

        public void SetCurrentVoiceCommandLabelText(String text)
        {
            currentVoiceCommandLabel.Text = text;
        }

        VoiceRecognition voiceRecognition = new VoiceRecognition();
        private void activateButton_Click(object sender, EventArgs e)
        {
            toggleStopAndStartVoiceCommands();

        }

        private void toggleStopAndStartVoiceCommands()
        {
            if (ActivateButton.Text == "Stop voice commands")
            {
                ActivateButton.Text = "Start voice commands";
                voiceRecognition.stop();
                this.Icon = Resources.SVCIcon;
            }
            else if (ActivateButton.Text == "Start voice commands")
            {
                ActivateButton.Text = "Stop voice commands";
                voiceRecognition.start();
                this.Icon = Resources.SVCRecording;
                
            }
        }

        private void svcWindow_Load(object sender, EventArgs e)
        {
            voiceRecognition.loadSpeechRecognition();
        }

        private void svcWindow_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void buttonListVoiceCommands_Click(object sender, EventArgs e)
        {
            string message = "Currently implemented voice commands:\nopen library\nopen store\nopen friends\nopen settings\nopen downloads\nopen gamename\n" +
                "start voice recognition/start voice commands\nstop voice recognition/stop voice commands";
            MessageBox.Show(message);
        }

        private void buttonListInstalledGames_Click(object sender, EventArgs e)
        {
            FormListOfInstalledGames formListOfInstalledGames = new FormListOfInstalledGames();
            formListOfInstalledGames.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void keybindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var currentKeys = keybindTextBox.Text.Split('+');
            if (currentKeys.Length == 3)
            {
                return;
            }
            if (currentKeys.Contains(e.KeyCode.ToString()))
            {
                return;
            }
            if (keyBindValue.Count == 1 && e.Modifiers.ToString().Equals("None"))
            {
                return;
            }
            if (!keybindTextBox.Text.Equals(""))
            {
                keybindTextBox.AppendText("+" + e.KeyCode.ToString());
                if (keyIsNotModifier(e.KeyCode.ToString()))
                {
                    keyBindValue.Add(e.KeyCode);
                }
                else
                {
                    keyBindModifierValues.Add(e.KeyCode);
                }

            }
            if (keybindTextBox.Text.Equals(""))
            {
                keybindTextBox.AppendText(e.KeyCode.ToString());
                if (keyIsNotModifier(e.KeyCode.ToString()))
                {
                    keyBindValue.Add(e.KeyCode);
                }
                else
                {
                    keyBindModifierValues.Add(e.KeyCode);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            keybindTextBox.Clear();
            keyBindValue.Clear();
            keyBindModifierValues.Clear();
        }

        private void saveKeybindButton_Click(object sender, EventArgs e)
        {
            
            if (keybindTextBox.Text.Equals(""))
            {
                return;
            }
            if (keyBindModifierValues.Count == 0 || keyBindValue.Count == 0)
            {
                savedKeybindLabel.Text = "Keybind must contain at least one modifier key (e.g. SHIFT) and one regular key";
                return;
            }
            saveKeybindKeyAndModifiersToProperties();
            getSavedKeybindPropertiesAndUpdateSavedKeybindLabel();
            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_ID);
            setGlobalHotkey();
        }

        private void getSavedKeybindPropertiesAndUpdateSavedKeybindLabel()
        {
            ArrayList convertedSavedKeybind = new ArrayList();
            KeysConverter keysConverter = new KeysConverter();
            foreach (var modifier in Properties.Settings.Default.VoiceActivateKeybindModifiers)
            {
                convertedSavedKeybind.Add(keysConverter.ConvertToString(modifier));
            }
            foreach (var value in Properties.Settings.Default.VoiceActivateKeybindKey)
            {
                convertedSavedKeybind.Add(keysConverter.ConvertToString(value));
            }
            savedKeybindLabel.Text = "Saved keybind: ";
            foreach (var key in convertedSavedKeybind)
            {
                if (!savedKeybindLabel.Text.Equals("Saved keybind: "))
                {
                    savedKeybindLabel.Text = savedKeybindLabel.Text + "+ " + key;
                }
                if (savedKeybindLabel.Text.Equals("Saved keybind: "))
                {
                    savedKeybindLabel.Text = savedKeybindLabel.Text + key;
                }
            }
            Settings.Default.KeyBindLabel = savedKeybindLabel.Text;
            Settings.Default.Save();
        }

        private bool keyIsNotModifier(string item)
        {
            KeysConverter keysConverter= new KeysConverter();
            Keys key = (Keys)keysConverter.ConvertFromString(item);
            switch (key)
            {
                case (Keys.Alt):
                    return false;
                case (Keys.Menu):
                    return false;
                case (Keys.LMenu):
                    return false;
                case (Keys.RMenu):
                    return false;
                case (Keys.ControlKey):
                    return false;
                case (Keys.Control):
                    return false;
                case (Keys.LControlKey):
                    return false;
                case (Keys.RControlKey):
                    return false;
                case (Keys.Shift):
                    return false;
                case (Keys.ShiftKey):
                    return false;
                case (Keys.LShiftKey):
                    return false;
                case (Keys.RShiftKey):
                    return false;
                default:
                    return true;
            }
        }

        private void saveKeybindKeyAndModifiersToProperties()
        {
                Settings.Default.VoiceActivateKeybindModifiers = keyBindModifierValues;
                Settings.Default.VoiceActivateKeybindKey = keyBindValue;
                Settings.Default.Save();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void autoListenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckState checkState = autoListenCheckBox.CheckState;
            switch (checkState)
            {
                case CheckState.Unchecked:
                    Settings.Default.AutoListenOnLaunch = false;
                    Settings.Default.Save();
                    break;
                case CheckState.Checked:
                    Settings.Default.AutoListenOnLaunch = true;
                    Settings.Default.Save();
                    break;
                case CheckState.Indeterminate:
                    break;
                default:
                    break;
            }
        }
    }
}

