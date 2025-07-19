using System;
using System.Collections;
using System.IO;
using System.Speech.Recognition;

namespace SVC
{
    internal class VoiceRecognition
    {
        private SpeechRecognitionEngine recognizer;
        private bool voiceRecognitionActive = true;
        private readonly String currentDirectory = Directory.GetCurrentDirectory();
        private readonly ArrayList gamesList = new ArrayList();

        public bool GetVoiceRecognitionActive()
        {
            return voiceRecognitionActive;
        }

        public void LoadSpeechRecognition()
        {
            recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            var c = GetChoiceLibrary();
            var gb = new GrammarBuilder(c);
            var g = new Grammar(gb);
            recognizer.LoadGrammar(g);

            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Recognizer_SpeechRecognized);

            recognizer.SetInputToDefaultAudioDevice();

            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            gamesList.AddRange(File.ReadAllLines(currentDirectory + "/gameslist.txt"));
        }

        public void Cancel()
        {
            recognizer.RecognizeAsyncCancel();
        }

        public void Start()
        {
            voiceRecognitionActive = true;
        }

        public void Stop()
        {
            voiceRecognitionActive = false;
        }


        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            SvcWindow.currentForm.SetCurrentVoiceCommandLabelText("Current voice command: " + e.Result.Text);

            if (voiceRecognitionActive)
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
                    case "stop voice recognition":
                        voiceRecognitionActive = false;
                        SvcWindow.currentForm.SetActivateButtonText("Start voice commands");
                        break;
                    case "stop voice commands":
                        voiceRecognitionActive = false;
                        SvcWindow.currentForm.SetActivateButtonText("Start voice commands");
                        break;
                    default:
                        int forEachIndexNo = 0;
                        foreach (String line in gamesList)
                        {
                            if (line.Contains("Game Name: "))
                            {
                                String gameName = line;
                                gameName = gameName.TextAfter("Game Name: ");
                                if (e.Result.Text.Equals("open " + gameName))
                                {
                                    String appid = (string)gamesList[forEachIndexNo + 1];
                                    appid = appid.TextAfter("App ID: ");
                                    appid = appid.Trim();
                                    System.Diagnostics.Process.Start(@"steam://run/" + appid);
                                    break;
                                }
                            }
                            ++forEachIndexNo;
                        }
                        break;
                }


            }
            if (!voiceRecognitionActive)
            {
                switch (e.Result.Text)
                {
                    case "start voice recognition":
                        voiceRecognitionActive = true;
                        SvcWindow.currentForm.SetActivateButtonText("Stop voice commands");
                        break;
                    case "start voice commands":
                        voiceRecognitionActive = true;
                        SvcWindow.currentForm.SetActivateButtonText("Stop voice commands");
                        break;
                }

            }
        }

        private Choices GetChoiceLibrary()
        {
            Choices myChoices = new Choices();
            var lines = File.ReadAllLines(currentDirectory + "/gameslist.txt");
            foreach (String line in lines)
            {
                if (line.Contains("Game Name: "))
                {
                    String gameName = line;
                    gameName = gameName.TextAfter("Game Name: ");
                    myChoices.Add("open " + gameName);
                }
            }
            myChoices.Add("open library");
            myChoices.Add("open store");
            myChoices.Add("open friends");
            myChoices.Add("open settings");
            myChoices.Add("open downloads");
            myChoices.Add("start voice recognition");
            myChoices.Add("start voice commands");
            myChoices.Add("stop voice recognition");
            myChoices.Add("stop voice commands");
            return myChoices;
        }
    }
}
