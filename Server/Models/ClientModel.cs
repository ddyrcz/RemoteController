using System.Net.Sockets;

namespace Server.Models
{
    internal class ClientModel
    {
        public string ComputerName { get; set; }

        public Socket Socket { get; set; }
    }
}