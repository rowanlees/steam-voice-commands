using System;

namespace SVC.src.Services.Exceptions
{
    [Serializable]
    public class SteamInstallationLocatorException : Exception
    {
        public SteamInstallationLocatorException(string message) : base(message)
        {
        }
    }
}
