﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ChatApp.Model;
using ChatApp.View;
using ChatApp.ViewModel.Command;

namespace ChatApp.ViewModel
{
    internal class MainWindowViewModel
    {

        private NetworkManager NetworkManager { get; set; }
        private ICommand startChat;
        private string text;
        
        public MainWindowViewModel(NetworkManager networkManager)
        {
            NetworkManager = networkManager;
        }
        
        public string MyText { 
            get {

                return text; } 
            set 
            { 
                text = value;
            
            } 
        }


        public ICommand StartChat
        {
            get
            {
                if (startChat == null)
                    startChat = new StartChatCommand(this);
                return startChat;
            }
            set
            {
                startChat = value;
            }
        }

        private void startConnection()
        {
            NetworkManager.startConnection();
        }

        private ICommand enterCommand;
        public ICommand EnterCommand
        {
            get {
                if (enterCommand == null)
                {
                    return new KeyEnterCommand(this);
                }
                else {
                    return enterCommand;

                }
            }
            set { enterCommand = value; }
        }

        public void sendMessage()
        {
            NetworkManager.sendMessage(MyText);
        }

        public void showLoginPage()
        {
            if (NetworkManager.startConnection())
            {
                LoginPageView loginPage = new LoginPageView();
                loginPage.DataContext = this;
                
            }
   
        }
    }
}
