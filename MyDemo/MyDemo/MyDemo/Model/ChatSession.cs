using System;
using System.Collections.ObjectModel;

namespace ChatApp.Model
{
    public class ChatSession
    {
        public ObservableCollection<string> ChatText { get; set; }
        public string OtherUser { get; set; }
        public DateTime DateTime { get; set; }
        private Action saveCallback;

        public ChatSession(Action save)
        {
            ChatText = new ObservableCollection<string>();
            this.DateTime = DateTime.Now;
            this.saveCallback = save;
        }

        public ChatSession() { }

        public void Add(string input, string sender)
        {
            ChatText.Add($"{sender}: {input}");
            saveCallback();
        }

        public void ConnectionLost()
        {
            ChatText.Add("Connection lost");
        }
    }
}
