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


namespace SVC
{
    public partial class SvcWindow : Form
    {
        public static SvcWindow currentForm;


        public SvcWindow()
        {
            currentForm = this;
            InitializeComponent();
        }

        public void setActivateButtonTextToStart()
        {
            this.ActivateButton.Text = "Start voice commands";
        }

        public void setActivateButtonTextToStop()
        {
            this.ActivateButton.Text = "Stop voice commands";
        }

        VoiceRecognition voiceRecognition = new VoiceRecognition();
        private void activateButton_Click(object sender, EventArgs e)
        {
            if (ActivateButton.Text == "Stop voice commands")
            {
                ActivateButton.Text = "Start voice commands";
                voiceRecognition.cancel();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "Currently known voice commands:\nopen library\nopen store\nopen friends\nopen settings\nopen downloads";
            MessageBox.Show(message);
        }
    }
}

