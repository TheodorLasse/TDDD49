using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Model
{
    public class ProtocolManager
    {
        private NetworkManager networkManager;
        public string username;
        public ProtocolManager() {
            networkManager = new NetworkManager();
        }


        public async Task<bool> ConnectServer(string ip, int port)
        {
            if (username == string.Empty)
            {
                return false;
            }

            bool result = await networkManager.ConnectServer(ip, port);
            if (!result) { return false; }

            networkManager.SendMessage(username);
            string response = networkManager.ReadMessage();

            if (response == "accept")
            {
                return true;
            }
            else
            {
                MessageBox.Show("User did not accept your connection.");
                networkManager.CloseConnection();
                return false;
            }
        }

        public async Task<bool> StartServer(string ip, int port)
        {
            if (username == string.Empty)
            {
                return false;
            }
            bool result = await networkManager.StartServer(ip, port);
            if (!result) { return false; }

            string connectingUser = networkManager.ReadMessage();

            bool acceptConnection = MessageBox.Show($"{connectingUser} is trying to connect to you, do you accept?", "Connection!", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

            string response = "denied";

            if(acceptConnection) { response = "accept"; }

            networkManager.SendMessage(response);

            if(response == "accept")
            {
                return true;
            }
            else
            {
                networkManager.CloseConnection();
                return false;
            }
        }

        public void SendMessage(string message)
        {
            networkManager.SendMessage(message);

        }

        public void ReadMessages(ObservableCollection<string> messages)
        {
            while (networkManager.IsConnected)
            {
                string readMessage = networkManager.ReadMessage();
                if (readMessage != string.Empty)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        messages.Add(readMessage);
                    });
                }
            }
        }
    }
}
