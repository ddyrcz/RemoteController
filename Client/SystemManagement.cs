using Client.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    internal static class SystemManagement
    {
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        public static void ManageActions(SocketModel client)
        {
            ActionType actionType;

            while (true)
            {
                int bytesCount = client.Socket.Receive(client.Buffer);

                if (Enum.TryParse(Encoding.ASCII.GetString(client.Buffer, 0, bytesCount), out actionType))
                {
                    switch (actionType)
                    {
                        case ActionType.Shutdown:
                            InvokePlugins(PluginManager.GetAvailablePlugins(), ActionType.Shutdown);
                            break;

                        case ActionType.ChangeWallpaper:
                            InvokePlugins(PluginManager.GetAvailablePlugins(), ActionType.ChangeWallpaper);
                            break;

                        case ActionType.SystemBeep:
                            InvokePlugins(PluginManager.GetAvailablePlugins(), ActionType.SystemBeep);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        private static void InvokePlugins(IEnumerable<IPlugin> availablePlugins, ActionType type)
        {
            availablePlugins
                .Where(x => x.GetType() == type)
                .OrderBy(x => x.GetPriority())
                .ToList()
                .ForEach(x => x.Invoke());
        }
    }
}