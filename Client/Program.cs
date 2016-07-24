using Common;
using System.Diagnostics;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var message = (MessageType)0;

            switch (message)
            {
                case MessageType.Shutdown:
                    Process.Start("Shutdown", "-s -t 10");
                    break;

                case MessageType.ChangeWallpaper:
                    break;

                default:
                    break;
            }
        }
    }
}