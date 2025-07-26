namespace SVC.Core.Model
{
    public class Game
    {
        public string AppId { get; set; }
        public string GameName { get; set; }
        public Game(string appId, string gameName)
        {
            AppId = appId;
            GameName = gameName;
        }
        public Game() { }
    }
}
