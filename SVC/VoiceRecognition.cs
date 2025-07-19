using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Recognition;

namespace SVC
{
    internal class VoiceRecognition
    {
        private SpeechRecognitionEngine _recognizer;
        private bool _voiceRecognitionActive = true;
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private readonly List<string> _gamesList = new List<string>();

        public bool GetVoiceRecognitionActive()
        {
            return _voiceRecognitionActive;
        }

        public void LoadSpeechRecognition()
        {
            _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            var c = GetChoiceLibrary();
            var gb = new GrammarBuilder(c);
            var g = new Grammar(gb);
            _recognizer.LoadGrammar(g);

            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Recognizer_SpeechRecognized);

            _recognizer.SetInputToDefaultAudioDevice();

            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

            _gamesList.AddRange(File.ReadAllLines(_currentDirectory + "/gameslist.txt"));
        }

        public void Cancel()
        {
            _recognizer.RecognizeAsyncCancel();
        }

        public void Start()
        {
            _voiceRecognitionActive = true;
        }

        public void Stop()
        {
            _voiceRecognitionActive = false;
        }


        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs speechArgs)
        {

            SvcWindow.currentForm.SetCurrentVoiceCommandLabelText("Current voice command: " + speechArgs.Result.Text);

            if (_voiceRecognitionActive)
            {

                switch (speechArgs.Result.Text)
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
                        _voiceRecognitionActive = false;
                        SvcWindow.currentForm.SetActivateButtonText("Start voice commands");
                        break;
                    case "stop voice commands":
                        _voiceRecognitionActive = false;
                        SvcWindow.currentForm.SetActivateButtonText("Start voice commands");
                        break;
                    default:
                        int forEachIndexNo = 0;
                        foreach (String line in _gamesList)
                        {
                            if (line.Contains("Game Name: "))
                            {
                                String gameName = line;
                                gameName = gameName.TextAfter("Game Name: ");
                                if (speechArgs.Result.Text.Equals("open " + gameName))
                                {
                                    String appid = (string)_gamesList[forEachIndexNo + 1];
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
            if (!_voiceRecognitionActive)
            {
                switch (speechArgs.Result.Text)
                {
                    case "start voice recognition":
                        _voiceRecognitionActive = true;
                        SvcWindow.currentForm.SetActivateButtonText("Stop voice commands");
                        break;
                    case "start voice commands":
                        _voiceRecognitionActive = true;
                        SvcWindow.currentForm.SetActivateButtonText("Stop voice commands");
                        break;
                }

            }
        }

        private Choices GetChoiceLibrary()
        {
            Choices choices = new Choices();
            var lines = File.ReadAllLines(_currentDirectory + "/gameslist.txt");
            foreach (String line in lines)
            {
                if (line.Contains("Game Name: "))
                {
                    String gameName = line;
                    gameName = gameName.TextAfter("Game Name: ");
                    choices.Add("open " + gameName);
                }
            }
            choices.Add("open library");
            choices.Add("open store");
            choices.Add("open friends");
            choices.Add("open settings");
            choices.Add("open downloads");
            choices.Add("start voice recognition");
            choices.Add("start voice commands");
            choices.Add("stop voice recognition");
            choices.Add("stop voice commands");
            return choices;
        }
    }
}
