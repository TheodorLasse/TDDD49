using ChatApp.Model;
using System.Threading.Tasks;

namespace ChatApp.ViewModel
{
    public class LoginPageViewModel
    {
        private NetworkManager networkManager;

        public string ErrorMessage { get; set; } = "Hello World!";


        public LoginPageViewModel(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
        }

        public void Login(string ip, int port)
        {
            networkManager.Connect(ip, port).ContinueWith((task) =>
            {
                if (task.Result)
                {
                    ErrorMessage = "Connected!";
                }
                else
                {
                    ErrorMessage = "Failed to connect!";
                }
            });
        }

        public void StartListening(string ip, int port)
        {
            networkManager.StartListening(ip, port).Start();
        }
    }
}
