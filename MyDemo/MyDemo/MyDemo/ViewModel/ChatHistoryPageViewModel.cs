using ChatApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChatApp.ViewModel
{
    public class ChatHistoryPageViewModel
    {
        ChatSession chatSession;

        public ObservableCollection<string> Messages { get { return chatSession.ChatText; } }

        public ChatHistoryPageViewModel(ChatSession chatSession)
        {
            this.chatSession = chatSession;
        }

        public void GoBack()
        {
            MainWindow.MainFrame.NavigationService.GoBack();
        }
    }
}
