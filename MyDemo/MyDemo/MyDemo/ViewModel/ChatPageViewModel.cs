using ChatApp.Model;
using ChatApp.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace ChatApp.ViewModel
{
    public partial class ChatPageViewModel : INotifyPropertyChanged
    {
        readonly ProtocolManager protocolManager;

        public ObservableCollection<string> Messages
        {
            get { return ChatSession.ChatText; }
        }

        public ChatSession ChatSession { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string textInput;

        public string TextInput
        {
            get { return textInput; }
            set
            {
                textInput = value;
                OnPropertyChanged();
            }
        }

        public EnterCommand EnterCommand { get; set; }

        public ChatPageViewModel(ProtocolManager protocolManager, ChatSession chatSession)
        {
            this.protocolManager = protocolManager;
            EnterCommand = new EnterCommand(EnterKeyPressed);
            ChatSession = chatSession;
            Task.Run(() => protocolManager.ReadMessages(ChatSession));

            protocolManager.ShakeEvent += ShakeScreen;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void EnterKeyPressed()
        {
            if (TextInput == string.Empty)
                return;
            protocolManager.SendMessage(TextInput);
            ChatSession.Add(TextInput, protocolManager.username);
            TextInput = "";
        }

        public void GoBack()
        {
            MainWindow.MainFrame.NavigationService.GoBack();
            protocolManager.CloseConnection();
        }

        public void SendShake()
        {
            protocolManager.SendShake();
        }

        public static void ShakeScreen()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var storyboard = (Storyboard)mainWindow.FindResource("shakeKey");
            storyboard.Begin(mainWindow);
        }
    }
}
