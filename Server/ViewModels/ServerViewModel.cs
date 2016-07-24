using Server.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Server.ViewModels
{
    internal class ServerViewModel : BaseViewModel
    {
        private List<ClientModel> _clients;
        private ClientModel _selectedClient;

        private IPEndPoint _serverEndpoint;

        public ServerViewModel()
        {
            Clients = new List<ClientModel>();

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(EndPoint);
            server.Listen(100);

            server.BeginAccept(new AsyncCallback(AcceptCallback), server);
        }

        public List<ClientModel> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                NotifyOfPropertyChange();
            }
        }

        public IPEndPoint EndPoint
        {
            get
            {
                if (_serverEndpoint == null)
                {
                    string serverAddress = "127.0.0.1";
                    int port = 8000;

                    IPAddress ipAddress = IPAddress.Parse(serverAddress);

                    _serverEndpoint = new IPEndPoint(ipAddress, port);
                }

                return _serverEndpoint;
            }
        }

        public ClientModel SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                NotifyOfPropertyChange();
            }
        }

        public void AcceptCallback(IAsyncResult result)
        {
            var server = result.AsyncState as Socket;

            // start listening for another connection
            server.BeginAccept(new AsyncCallback(AcceptCallback), server);

            Socket clientSocket = server.EndAccept(result);

            var client = new ClientModel()
            {
                ComputerName = "unknow",
                Socket = clientSocket,
            };

            Clients.Add(client);
        }

        public void OnShutdown()
        {

        }
    }
}