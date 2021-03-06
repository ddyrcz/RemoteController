﻿using Caliburn.Micro;
using Common;
using Server.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.ViewModels
{
    internal class ServerViewModel : BaseViewModel
    {
        private BindableCollection<ClientModel> _clients;
        private ClientModel _selectedClient;

        private IPEndPoint _serverEndpoint;

        public ServerViewModel()
        {
            Clients = new BindableCollection<ClientModel>();

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(EndPoint);
            server.Listen(100);

            server.BeginAccept(new AsyncCallback(AcceptCallback), server);
        }

        public BindableCollection<ClientModel> Clients
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
                    string serverAddress = ConfigurationManager.AppSettings["ServerAddress"].ToString();
                    int port = int.Parse(ConfigurationManager.AppSettings["ServerPort"].ToString());

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
                ComputerName = "Unknow",
                Socket = clientSocket,
            };

            client.Socket.BeginReceive(client.Buffer, 0, ClientModel.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(RecieveCallback), client);

            Clients.Add(client);
        }

        public void OnShutdown()
        {
            byte[] message = Encoding.ASCII.GetBytes(((int)ActionType.Shutdown).ToString());
            Clients.ToList().ForEach(x => x.Socket.Send(message));
        }

        public void OnBeep()
        {
            byte[] message = Encoding.ASCII.GetBytes(((int)ActionType.SystemBeep).ToString());
            Clients.ToList().ForEach(x => x.Socket.Send(message));
        }

        public void RecieveCallback(IAsyncResult result)
        {
            var client = result.AsyncState as ClientModel;

            int bytesCount = client.Socket.EndReceive(result);

            if (bytesCount > 0)
            {
                string message = Encoding.ASCII.GetString(client.Buffer, 0, bytesCount);

                Clients.FirstOrDefault(x => x.Equals(client)).ComputerName = message;
            }
        }
    }
}