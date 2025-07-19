using SVC.src.Services;

namespace SVCTests.src.Services
{
    [TestClass]
    public class GameManifestParserTests
    {
        private readonly GameManifestParser _gameManifestParser = new GameManifestParser();

        [TestMethod]
        public void ParseAcfFile_ParsesAppIdAndGameName_Correctly()
        {
            var acfContent = @"
                ""appid""		""987654""
	            ""universe""		""1""
	            ""LauncherPath""		""C:\\Program Files (x86)\\Steam\\steam.exe""
	            ""name""		""Test Game""
            ";
            var gamesList = new Dictionary<string, string>();

            _gameManifestParser.ParseAcfFile(acfContent, gamesList);

            Assert.AreEqual(1, gamesList.Count);
            Assert.IsTrue(gamesList.ContainsKey("Game Name: Test Game"));
            Assert.AreEqual("App ID: 987654", gamesList["Game Name: Test Game"]);
        }

        [TestMethod]
        public void ParseAcfFile_HandlesExtraWhitespaceAndQuotes()
        {
            var acfContent = @"
                ""appid""    "" 987654 ""
                ""universe""		""1""
	            ""LauncherPath""		""C:\\Program Files (x86)\\Steam\\steam.exe""
                ""name""    ""  Another Game  ""
            ";
            var gamesList = new Dictionary<string, string>();

            _gameManifestParser.ParseAcfFile(acfContent, gamesList);

            Assert.AreEqual(1, gamesList.Count);
            Assert.IsTrue(gamesList.ContainsKey("Game Name: Another Game"));
            Assert.AreEqual("App ID: 987654", gamesList["Game Name: Another Game"]);
        }

        [TestMethod]
        public void ParseAcfFile_EmptyInput_DoesNotAdd()
        {
            var acfContent = "";
            var gamesList = new Dictionary<string, string>();

            _gameManifestParser.ParseAcfFile(acfContent, gamesList);

            Assert.IsFalse(gamesList.ContainsKey("Game Name: "));
            Assert.AreEqual(0, gamesList.Count);
        }
    }
}