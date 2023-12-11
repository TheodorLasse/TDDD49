using ChatApp.Model;
using ChatApp.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace ChatApp.ViewModel
{
    public partial class ConnectPageViewModel : INotifyPropertyChanged
    {
        private ProtocolManager protocolManager;

        public event PropertyChangedEventHandler PropertyChanged;

        private ChatHistory chatHistory;

        public ObservableCollection<string> ChatHistoryStrings { get; set; } = new();

        private void UpdateHistoryList()
        {
            ChatHistoryStrings = new ObservableCollection<string>(
                chatHistory.HistoryStrings.Where(x => x.Contains(SearchText))
            );
            OnPropertyChanged(nameof(ChatHistoryStrings));
        }

        private string searchText = "";

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                UpdateHistoryList();
                OnPropertyChanged();
            }
        }

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

        public ConnectPageViewModel(ProtocolManager protocolManager, ChatHistory chatHistory)
        {
            this.protocolManager = protocolManager;
            this.chatHistory = chatHistory;
            UpdateHistoryList();

            // Update history list on back navigation
            MainWindow.MainFrame.NavigationService.Navigated += (sender, e) =>
            {
                if (e.Content is ConnectPageView)
                {
                    UpdateHistoryList();
        }
            };  
        }

        public async void ConnectServer(string ip, int port, string username)
        {
            ErrorMessage = "Trying to connect to server...";
            try
            {
                protocolManager.username = username;
                string? otherUser = await protocolManager.ConnectServer(ip, port);
                if (otherUser != null)
                {
                    ErrorMessage = "Connected to server!";

                    ChatSession newChatSession = chatHistory.StartNewSession(otherUser);

                    MainWindow.MainFrame.NavigationService.Navigate(
                        new ChatPageView(new ChatPageViewModel(protocolManager, newChatSession))
                    );
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
                string? otherUser = await protocolManager.StartServer(ip, port);
                if (otherUser != null)
                {
                    ErrorMessage = "Connected as server!";

                    ChatSession newChatSession = chatHistory.StartNewSession(otherUser);

                    MainWindow.MainFrame.NavigationService.Navigate(
                        new ChatPageView(new ChatPageViewModel(protocolManager, newChatSession))
                    );
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

        internal void ShowHistory(string chatId)
        {
            int index = chatHistory.HistoryStrings.IndexOf(chatId);
            MainWindow.MainFrame.NavigationService.Navigate(
                new ChatHistoryPageView(new ChatHistoryPageViewModel(chatHistory.History[index]))
            );
        }
    }
}
