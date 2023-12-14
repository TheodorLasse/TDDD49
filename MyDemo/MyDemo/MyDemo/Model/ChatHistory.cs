using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ChatApp.Model
{
    public class ChatHistory
    {
        public List<ChatSession> History;
        private readonly string fileName = "History.txt";

        public ChatHistory()
        {
            History = new List<ChatSession>();
            Load();
        }

        public ObservableCollection<string> HistoryStrings
        {
            get
            {
                ObservableCollection<string> strings = new ObservableCollection<string>();

                var temp = History.OrderBy(x => x.DateTime);

                foreach (ChatSession session in temp)
                {
                    strings.Add($"{session.OtherUser}, {session.DateTime}");
                }
                return strings;
            }
        }

        public ChatSession StartNewSession(string otherUser)
        {
            ChatSession newChatSession = new ChatSession(Save);
            newChatSession.OtherUser = otherUser;
            History.Add(newChatSession);

            return newChatSession;
        }

        public void Save()
        {
            try
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.Write(JsonSerializer.Serialize(History));
                }
            }
            catch (IOException)
            {
                return;
            }
        }

        private void Load()
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string fileContents = sr.ReadToEnd();
                    var temp = JsonSerializer.Deserialize<List<ChatSession>>(fileContents);
                    if (temp != null)
                    {
                        History = temp;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                return;
            }
        }
    }
}
