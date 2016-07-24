using Server.Models;
using System.Collections.Generic;

namespace Server.ViewModels
{
    internal class ServerViewModel : BaseViewModel
    {
        private List<ClientModel> _clients;
        private ClientModel _selectedClient;

        public ServerViewModel()
        {
            Clients = new List<ClientModel>()
            {
                new ClientModel() { ComputerName = "ddyrcz" },
                new ClientModel() { ComputerName = "OSKAR" },
                new ClientModel() { ComputerName = "MARKUS" },
                new ClientModel() { ComputerName = "SERVER" },
            };
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

        public ClientModel SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                NotifyOfPropertyChange();
            }
        }
    }
}