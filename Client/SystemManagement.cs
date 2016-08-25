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
            ActionType message;

            while (true)
            {
                int bytesCount = client.Socket.Receive(client.Buffer);

                if (Enum.TryParse(Encoding.ASCII.GetString(client.Buffer, 0, bytesCount), out message))
                {
                    switch (message)
                    {
                        case ActionType.Shutdown:
                            action = new Shutdown();
                            break;

                        case ActionType.ChangeWallpaper:
                            action = new ChangeWallpaper();
                            break;

                        case ActionType.SystemBeep:
                            action = new SystemBeep();
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