using SVC.Core.Services.Implementations;

namespace SVC.Core.Tests.Services.Implementations
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

            var game = _gameManifestParser.ParseAcfFile(acfContent);

            Assert.IsNotNull(game);
            Assert.AreEqual("Test Game", game.GameName);
            Assert.AreEqual("987654", game.AppId);
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

            var game = _gameManifestParser.ParseAcfFile(acfContent);

            Assert.AreEqual("Another Game", game.GameName);
            Assert.AreEqual("987654", game.AppId);
        }

        [TestMethod]
        public void ParseAcfFile_EmptyInput_DoesNotAdd()
        {
            var acfContent = "";
            var gamesList = new Dictionary<string, string>();

            var game = _gameManifestParser.ParseAcfFile(acfContent);

            Assert.IsNull(game);
        }
    }
}