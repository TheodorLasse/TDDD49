using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.ViewModel.Commands
{
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
