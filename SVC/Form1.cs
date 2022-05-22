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
    public partial class svcWindow : Form
    {
        public svcWindow()
        {
            InitializeComponent();
        }

        SpeechRecognitionEngine recognizer;
        private void activateButton_Click(object sender, EventArgs e)
        {
            if (ActivateButton.Text == "Stop voice commands")
            {
                ActivateButton.Text = "Start voice commands";
                recognizer.RecognizeAsyncCancel();
            }
            else if (ActivateButton.Text == "Start voice commands")
            {
                ActivateButton.Text = "Stop voice commands";
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }

        }

        private void loadSpeechRecognition()
        {
            recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            var c = getChoiceLibrary();
            var gb = new GrammarBuilder(c);
            var g = new Grammar(gb);
            recognizer.LoadGrammar(g);

            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            recognizer.SetInputToDefaultAudioDevice();

            recognizer.RecognizeAsync(RecognizeMode.Multiple);


        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "open library":
                    System.Diagnostics.Process.Start(@"steam://open/games");
                    break;
                case "open store":
                    System.Diagnostics.Process.Start(@"steam://store");
                    break;
                case "open friends":
                    System.Diagnostics.Process.Start(@"steam://open/friends");
                    break;
                case "open settings":
                    System.Diagnostics.Process.Start(@"steam://open/settings");
                    break;
                case "open downloads":
                    System.Diagnostics.Process.Start(@"steam://open/downloads");
                    break;
            }
        }

        

        private Choices getChoiceLibrary()
        {
            Choices myChoices = new Choices();
            myChoices.Add("open library");
            myChoices.Add("open store");
            myChoices.Add("open friends");
            myChoices.Add("open settings");
            myChoices.Add("open downloads");
            return myChoices;
        }

        private void svcWindow_Load(object sender, EventArgs e)
        {
            loadSpeechRecognition();
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

