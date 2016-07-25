using System;
using System.Net.Sockets;

namespace Server.Models
{
    internal class ClientModel : Caliburn.Micro.PropertyChangedBase
    {
        public static int BUFFER_SIZE = 1024;
        public byte[] Buffer = new byte[BUFFER_SIZE];

        private string _computerName;

        public string ComputerName
        {
            get { return _computerName; }
            set
            {
                _computerName = value;
                NotifyOfPropertyChange();
            }
        }

        public Socket Socket { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ClientModel)
            {
                var client = obj as ClientModel;

                return
                    (Socket?.RemoteEndPoint?.ToString() ?? Guid.NewGuid().ToString()) == (client?.Socket?.RemoteEndPoint?.ToString() ?? Guid.NewGuid().ToString());
            }
            return base.Equals(obj);
        }
    }
}