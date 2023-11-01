using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChatApp.ViewModel;

namespace ChatApp.ViewModel.Command
{
    internal class StartChatCommand : ICommand
    {
        private MainWindowViewModel parent = null;


        public StartChatCommand(MainWindowViewModel parent)
        {
            this.parent = parent;

        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            parent.showLoginPage();
        }
    }
}
