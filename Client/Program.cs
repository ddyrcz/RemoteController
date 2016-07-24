using Common;
using System.Diagnostics;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var message = (MessageType)1;

            switch (message)
            {
                case MessageType.Shutdown:
                    Process.Start("shutdown", "/s //t 0");
                    break;

                case MessageType.ChangeWallpaper:
                    break;

                default:
                    break;
            }
        }
    }
}