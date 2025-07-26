using System;

namespace SVC.Core.Services.Exceptions
{
    [Serializable]
    public class SteamInstallationLocatorException : Exception
    {
        public SteamInstallationLocatorException(string message) : base(message)
        {
        }
    }
}
