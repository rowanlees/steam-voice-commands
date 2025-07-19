using Moq;
using SVC.src.Services;
using SVC.src.Services.Interfaces;

namespace SVCTests.src.Services.Implementations
{
    [TestClass]
    public class SteamLibraryReaderTests
    {
        [TestMethod]
        public void GetLibraryFolders_ReturnsCorrectPaths()
        {
            // Arrange
            var mockFileReader = new Mock<IFileReader>();
            var sampleFile = """
                                "libraryfolders"
                {
                	"0"
                	{
                		"path"		"C:\\Steam"
                		"label"		""
                		"contentid"		"1"
                		"totalsize"		"1"
                		"update_clean_bytes_tally"		"1"
                		"time_last_update_verified"		"1"
                		"apps"
                		{
                			"1"
                		}
                	}
                	"1"
                	{
                		"path"		"E:\\SteamLibrary"
                		"label"		""
                		"contentid"		"1"
                		"totalsize"		"1"
                		"update_clean_bytes_tally"		"1"
                		"time_last_update_verified"		"1"
                		"apps"
                		{
                			"2"
                		}
                	}
                }
                """;
            sampleFile.Replace("\r", "");
            var list = sampleFile.Split('\n').ToList();
            mockFileReader.Setup(fr => fr.ReadLines(It.IsAny<string>())).Returns(list);
            var steamLibraryReader = new SteamLibraryReader(mockFileReader.Object);
            // Act
            var libraryFolders = steamLibraryReader.GetLibraryFolders("");
            // Assert
            Assert.AreEqual(2, libraryFolders.Count);
            Assert.AreEqual("C:\\\\Steam", libraryFolders[0]);
            Assert.AreEqual("E:\\\\SteamLibrary", libraryFolders[1]);
        }
    }
}
