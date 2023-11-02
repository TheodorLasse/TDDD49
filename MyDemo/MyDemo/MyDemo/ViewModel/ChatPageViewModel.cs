using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
    public partial class ChatPageViewModel : INotifyPropertyChanged
    {
        ProtocolManager protocolManager;
        
        public ObservableCollection<string> Messages;

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

        public EnterCommand EnterCommand
        {
            get; set;
        }

        public ChatPageViewModel(ProtocolManager protocolManager) {
            this.protocolManager = protocolManager;
            Messages = new ObservableCollection<string>();
            EnterCommand = new EnterCommand(enterKeyPressed);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void enterKeyPressed()
        {
            protocolManager.SendMessage(TextInput);
            TextInput = "";
        }
    }

    public class EnterCommand : ICommand
    {
        Action enterKeyPressed;
        public EnterCommand(Action sendMessage)
        {
            this.enterKeyPressed = sendMessage;
        }

        public event EventHandler? CanExecuteChanged;

        bool ICommand.CanExecute(object? parameter)
        {
            return true;
        }

        void ICommand.Execute(object? parameter)
        {
            enterKeyPressed();
        }
    }

}
