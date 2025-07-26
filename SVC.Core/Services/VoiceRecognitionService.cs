using SVC.Core.Extensions;
using SVC.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Speech.Recognition;

namespace SVC.Core.Services
{
    public class VoiceRecognitionService
    {
        private readonly SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        private bool _voiceRecognitionActive = false;
        private readonly string _currentDirectory = Directory.GetCurrentDirectory();
        private readonly List<string> _gamesList = new List<string>();
        public event Action<string> CommandRecognized;

        /*
         * Loads the speech recognition engine with the grammar for voice commands.
         * It does not set _voiceRecognitionActive to true, that is done in Start().
         * However, it is still listening, as it will still accept start and stop commands, just not any other commands.
         */
        public void LoadSpeechRecognition()
        {
            var c = GetChoiceLibrary();
            var gb = new GrammarBuilder(c);
            var g = new Grammar(gb);
            _recognizer.LoadGrammar(g);

            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(OnSpeechRecognized);

            _recognizer.SetInputToDefaultAudioDevice();

            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

            _gamesList.AddRange(File.ReadAllLines(_currentDirectory + Path.DirectorySeparatorChar + GameRepository.GamesListFileName));
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


        private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs speechArgs)
        {

            if (_voiceRecognitionActive)
            {

                switch (speechArgs.Result.Text)
                {
                    case "open library":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        System.Diagnostics.Process.Start(@"steam://open/games");
                        break;
                    case "open store":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        System.Diagnostics.Process.Start(@"steam://store");
                        break;
                    case "open friends":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        System.Diagnostics.Process.Start(@"steam://open/friends");
                        break;
                    case "open settings":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        System.Diagnostics.Process.Start(@"steam://open/settings");
                        break;
                    case "open downloads":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        System.Diagnostics.Process.Start(@"steam://open/downloads");
                        break;
                    case "stop voice recognition":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        _voiceRecognitionActive = false;
                        break;
                    case "stop voice commands":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        _voiceRecognitionActive = false;
                        break;
                    default:
                        int forEachIndexNo = 0;
                        foreach (string line in _gamesList)
                        {
                            if (line.Contains("Game Name: "))
                            {
                                string gameName = line;
                                gameName = gameName.TextAfter("Game Name: ");
                                if (speechArgs.Result.Text.Equals("open " + gameName))
                                {
                                    CommandRecognized?.Invoke(speechArgs.Result.Text);
                                    string appid = (string)_gamesList[forEachIndexNo + 1];
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
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        _voiceRecognitionActive = true;
                        break;
                    case "start voice commands":
                        CommandRecognized?.Invoke(speechArgs.Result.Text);
                        _voiceRecognitionActive = true;
                        break;
                }

            }
        }

        private Choices GetChoiceLibrary()
        {
            Choices choices = new Choices();
            var lines = File.ReadAllLines(_currentDirectory + Path.DirectorySeparatorChar + GameRepository.GamesListFileName);
            foreach (string line in lines)
            {
                if (line.Contains("Game Name: "))
                {
                    string gameName = line;
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
