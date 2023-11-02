using ChatApp.Model;
using ChatApp.View;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChatApp.ViewModel
{
    public partial class ConnectPageViewModel : INotifyPropertyChanged
    {
        private ProtocolManager protocolManager;

        public event PropertyChangedEventHandler PropertyChanged;

        private string errorMessage = "";

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ConnectPageViewModel(ProtocolManager protocolManager)
        {
            this.protocolManager = protocolManager;
        }


        public async void ConnectServer(string ip, int port, string username)
        {
            ErrorMessage = "Trying to connect to server...";
            try
            {
                protocolManager.username = username;
                if (await protocolManager.ConnectServer(ip, port))
                {
                    ErrorMessage = "Connected to server!";
                    MainWindow.MainFrame.NavigationService.Navigate(
                        new ChatPageView(new ChatPageViewModel(protocolManager)));
                }
                else
                {
                    ErrorMessage = "Failed to connect to server!";
                }
            }
            catch
            {
                ErrorMessage = "Failed to connect to server!";
            }
        }

        public async void StartServer(string ip, int port, string username)
        {
            ErrorMessage = "Trying to start server...";
            try
            {
                protocolManager.username = username;
                if (await protocolManager.StartServer(ip, port))
                {
                    ErrorMessage = "Connected as server!";
                    MainWindow.MainFrame.NavigationService.Navigate(
                        new ChatPageView(new ChatPageViewModel(protocolManager)));
                }
                else
                {
                    ErrorMessage = "Failed to start server!";
                }
            }
            catch
            {
                ErrorMessage = "Failed to start server!";
            }
        }
    }
}
