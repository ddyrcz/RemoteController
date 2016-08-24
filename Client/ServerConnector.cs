using Client.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    internal static class ServerConnector
    {
        private static SocketModel _client;

        private static IPEndPoint _serverEndpoint;

        public static SocketModel Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new SocketModel
                    {
                        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp),
                    };
                }

                return _client;
            }
        }

        public static IPEndPoint ServerEndpoint
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

        public static bool ConnectWithServer()
        {
            try
            {
                Client?.Socket?.Connect(ServerEndpoint);
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("An error occured while reading configuration file");
                return false;
            }
            catch (SocketException)
            {
                MessageBox.Show("An error occured while connecting to the server");
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured");
                return false;
            }

            return true;
        }

        public static void SendMessage(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);

            Client?.Socket?.Send(bytes);
        }
    }
}