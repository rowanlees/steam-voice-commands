using Moq;
using SVC.src.Services;
using SVC.src.Services.Exceptions;
using SVC.src.Services.Interfaces;
using System.Diagnostics;

namespace SVCTests
{
    [TestClass]
    public class SteamInstallationLocatorTests
    {
        private readonly int _timeoutMs = 1;
        private Mock<IProcess> _mockProcess;
        private SteamInstallationLocator _steamInstallationLocator;

        [TestInitialize]
        public void Setup()
        {
            _mockProcess = new Mock<IProcess>();
            _steamInstallationLocator = new SteamInstallationLocator(_mockProcess.Object, 1);
            _mockProcess.Setup(p => p.StartInfo).Returns(new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WorkingDirectory = Directory.GetCurrentDirectory(),
                FileName = "test",
                Arguments = "test"
            });
        }

        [TestMethod]
        public void GetSteamInstallationPath_ReturnsPath_WhenProcessDoesNotTimeOut()
        {
            var content = "\r\nHKEY_CURRENT_USER\\SOFTWARE\\Valve\\Steam\r\n    SteamExe    REG_SZ    g:/steam/steam.exe\r\n\r\nEnd of search: 1 match(es) found.\r\n";
            MemoryStream memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
            _mockProcess.Setup(p => p.WaitForExit(_timeoutMs)).Returns(true);
            _mockProcess.Setup(p => p.StandardOutputReadToEnd()).Returns(content);

            var steamPath = _steamInstallationLocator.GetSteamFolderPath();

            Assert.IsFalse(string.IsNullOrEmpty(steamPath));
            Assert.AreEqual("g:/steam", steamPath);
        }

        [TestMethod]
        public void GetSteamInstallationPath_ThrowsOnTimeout()
        {
            var content = "\r\nHKEY_CURRENT_USER\\SOFTWARE\\Valve\\Steam\r\n    SteamExe    REG_SZ    g:/steam/steam.exe\r\n\r\nEnd of search: 1 match(es) found.\r\n";
            MemoryStream memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
            _mockProcess.Setup(p => p.WaitForExit(_timeoutMs)).Returns(false);

            Assert.ThrowsException<SteamInstallationLocatorException>(_steamInstallationLocator.GetSteamFolderPath);
        }
    }
}
