using Moq;
using Newtonsoft.Json;
using SVC.Core.Model;
using SVC.Core.Repositories.Implementations;
using SVC.Core.SystemInterop.Interface;

namespace SVC.Core.Tests.Repositories.Implementations
{
    [TestClass]
    public class GameRepositoryTests
    {
        [TestMethod]
        public void SaveGames_CallsWriteLineWithCorrectArguments()
        {
            // Arrange
            var mockFileSystem = new Mock<IFileSystem>();
            var mockWriter = new Mock<TextWriter>();

            mockWriter.As<IDisposable>().Setup(m => m.Dispose());

            mockFileSystem.Setup(fs => fs.GetCurrentDirectory()).Returns("C:\\Test");
            mockFileSystem.Setup(fs => fs.CreateTextWriter(It.IsAny<string>()))
                .Returns(mockWriter.Object);

            var repository = new GameRepository(mockFileSystem.Object);
            var game = new Game(appId: "123", gameName: "Test Game");
            var games = new List<Game> { game };

            // Act
            repository.SaveGames(games);

            // Assert
            mockFileSystem.Verify(fs => fs.CreateTextWriter("C:\\Test\\gameslist.txt"), Times.Once);
            mockWriter.Verify(w => w.WriteLine(JsonConvert.SerializeObject(game)), Times.Once);
        }

        [TestMethod]
        public void ReadGameFromJsonObject_ParsesJsonStringCorrectly()
        {
            // Arrange
            var repository = new GameRepository(Mock.Of<IFileSystem>());
            var json = "{\"GameName\": \"Test Game: A Game Name Containing \\\"Symbols\\\"!£$%^&*()-_=+{[}]:;@'~#|<,>.?/\", \"AppID\": \"123\"}";
            // Act
            var game = repository.ReadGameFromJsonObject(json);
            // Assert
            Assert.AreEqual("123", game.AppId);
            Assert.AreEqual("Test Game: A Game Name Containing \"Symbols\"!£$%^&*()-_=+{[}]:;@'~#|<,>.?/", game.GameName);
        }
    }
}
