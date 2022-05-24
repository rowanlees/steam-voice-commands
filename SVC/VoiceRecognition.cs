using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace SVC
{
    internal class VoiceRecognition
    {

        SpeechRecognitionEngine recognizer;

        public void loadSpeechRecognition()
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

        public void cancel()
        {
            recognizer.RecognizeAsyncCancel();
        }

        public void start(){
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
    }
}
