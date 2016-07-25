using Common;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public class SocketModel
    {
        public static int BUFFER_SIZE = 1024;
        public byte[] Buffer = new byte[BUFFER_SIZE];
        public Socket Socket { get; set; }
    }

    internal class Program
    {
        private static SocketModel _client;

        private static IPEndPoint _serverEndpoint;

        public static IPEndPoint ServerEndpoint
        {
            get
            {
                if (_serverEndpoint == null)
                {
                    string serverAddress = "192.168.0.108";
                    int port = 8000;

                    IPAddress ipAddress = IPAddress.Parse(serverAddress);

                    _serverEndpoint = new IPEndPoint(ipAddress, port);
                }

                return _serverEndpoint;
            }
        }

        private static SocketModel Client
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

        private static bool ConnectWithServer()
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
            catch (SocketException ex)
            {
                MessageBox.Show("An error occured while connecting to the server");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured");
                return false;
            }

            return true;
        }

        private static void Main(string[] args)
        {
            MessageType message;

            if (ConnectWithServer())
            {
                byte[] buffer = new byte[1024];
                int bytesCount = Client.Socket.Receive(buffer);

                if (Enum.TryParse(Encoding.ASCII.GetString(buffer, 0, bytesCount), out message))
                {
                    switch (message)
                    {
                        case MessageType.Shutdown:
                            Process.Start("Shutdown", "-s -t 0");
                            break;

                        case MessageType.ChangeWallpaper:
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}