using Client.Actions;
using Client.Models;
using Common;
using System;
using System.Text;

namespace Client
{
    internal static class SystemManagement
    {
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        public static void ManageCommands(SocketModel client)
        {
            IAction action = null;
            MessageType message;

            while (true)
            {
                int bytesCount = client.Socket.Receive(client.Buffer);

                if (Enum.TryParse(Encoding.ASCII.GetString(client.Buffer, 0, bytesCount), out message))
                {
                    switch (message)
                    {
                        case MessageType.Shutdown:
                            action = new Shutdown();
                            break;

                        case MessageType.ChangeWallpaper:
                            action = new ChangeWallpaper();
                            break;

                        default:
                            break;
                    }

                    action?.Invoke();
                }
            }
        }
    }
}