using Moq;
using SVC.src.Model;
using SVC.src.Services.Implementations;
using SVC.src.Services.Interfaces;

namespace SVCTests.src.Services.Implementations
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
            var games = new List<Game> { new(appId: "123", gameName: "Test Game") };

            // Act
            repository.SaveGames(games);

            // Assert
            mockFileSystem.Verify(fs => fs.CreateTextWriter("C:\\Test\\gameslist.txt"), Times.Once);
            mockWriter.Verify(w => w.WriteLine("Game Name: Test Game"), Times.Once);
            mockWriter.Verify(w => w.WriteLine("App ID: 123"), Times.Once);
        }
    }
}
