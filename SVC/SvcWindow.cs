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
            if (Properties.Settings.Default.VoiceActivateKeybindModifiers != null)
            {
                RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 6, (int)Keys.F12);
            }
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
            if (ActivateButton.Text == "Stop voice commands")
            {
                ActivateButton.Text = "Start voice commands";
                voiceRecognition.stop();
            }
            else if (ActivateButton.Text == "Start voice commands")
            {
                ActivateButton.Text = "Stop voice commands";
                voiceRecognition.start();
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

            }
            if (keybindTextBox.Text.Equals(""))
            {
                keybindTextBox.AppendText(e.KeyCode.ToString());
            }
            if (!e.Modifiers.ToString().Equals("None"))
            {
                keyBindModifierValues.Add(e.KeyValue);
            }
            if (e.Modifiers.ToString().Equals("None"))
            {
                keyBindValue.Add(e.KeyValue);
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
            Properties.Settings.Default.VoiceActivateKeybindModifiers = keyBindModifierValues;
            Properties.Settings.Default.VoiceActivateKeybindKey = keyBindValue;
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
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

