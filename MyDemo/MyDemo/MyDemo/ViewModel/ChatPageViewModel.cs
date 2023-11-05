using ChatApp.Model;
using ChatApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
    public partial class ChatPageViewModel : INotifyPropertyChanged
    {
        ProtocolManager protocolManager;

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
            EnterCommand = new EnterCommand(enterKeyPressed);
            this.ChatSession = chatSession;
            Task.Run(() => protocolManager.ReadMessages(ChatSession));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void enterKeyPressed()
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

        public void Shake()
        {
            protocolManager.SendShake();
        }
    }
}
