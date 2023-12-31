﻿using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.Model
{
    public class ProtocolManager
    {
        private readonly NetworkManager networkManager;
        public string username;
        private string otherUser;
        
        public delegate void ShakeEventHandler();
        public event ShakeEventHandler ShakeEvent;

        public delegate void MessageEventHandler(string message);
        public event MessageEventHandler MessageEvent;

        public ProtocolManager()
        {
            networkManager = new NetworkManager();
        }

        public async Task<string?> ConnectServer(string ip, int port)
        {
            if (username == string.Empty)
            {
                return null;
            }

            bool result = await networkManager.ConnectServer(ip, port);
            if (!result)
            {
                return null;
            }

            string payload = JsonSerializer.Serialize(
                new ProtocolMessage(ProtocolType.Connect, username)
            );

            networkManager.SendMessage(payload);

            ProtocolMessage? response = JsonSerializer.Deserialize<ProtocolMessage>(
                networkManager.ReadMessage()
            );

            if (response == null)
            {
                return null;
            }

            if (response.RequestType == ProtocolType.Accept)
            {
                otherUser = response.Message;
                return otherUser;
            }
            else
            {
                MessageEvent?.Invoke("User did not accept your connection.");
                networkManager.CloseConnection();
                return null;
            }
        }

        public async Task<string?> StartServer(string ip, int port, Func<string, bool> connectionRequest)
        {
            if (username == string.Empty)
            {
                return null;
            }
            bool result = await networkManager.StartServer(ip, port);
            if (!result)
            {
                return null;
            }

            ProtocolMessage? connectingUser = JsonSerializer.Deserialize<ProtocolMessage>(
                networkManager.ReadMessage()
            );

            if (connectingUser == null)
                return null;

            bool acceptConnection = connectionRequest(connectingUser.Message);
                
            ProtocolType response = ProtocolType.Deny;

            if (acceptConnection)
            {
                response = ProtocolType.Accept;
            }

            networkManager.SendMessage(
                JsonSerializer.Serialize(new ProtocolMessage(response, username))
            );

            if (response == ProtocolType.Accept)
            {
                otherUser = connectingUser.Message;
                return connectingUser.Message;
            }
            else
            {
                networkManager.CloseConnection();
                return null;
            }
        }

        public async void SendMessage(string message)
        {
            if (networkManager.IsConnected)
            {
                await Task.Run(() => networkManager.SendMessage(JsonSerializer.Serialize(new ProtocolMessage(ProtocolType.Message, message))));
            }
        }

        public async void SendShake()
        {
            if (networkManager.IsConnected)
            {
                await Task.Run(() => networkManager.SendMessage(JsonSerializer.Serialize(new ProtocolMessage(ProtocolType.Shake, ""))));
            }
        }

        public void ReadMessages(ChatSession chatSession)
        {
            while (networkManager.IsConnected)
            {
                string readMessage = networkManager.ReadMessage();
                if (readMessage == string.Empty)
                {
                    Application.Current.Dispatcher.Invoke(
                        delegate
                        {
                            chatSession.ConnectionLost();
                        }
                    );
                    networkManager.CloseConnection();
                    return;
                }

                ProtocolMessage? message = JsonSerializer.Deserialize<ProtocolMessage>(readMessage);
                if (message != null && message.RequestType == ProtocolType.Message)
                {
                    Application.Current.Dispatcher.Invoke(
                        delegate
                        {
                            chatSession.Add(message.Message, otherUser);
                        }
                    );
                }
                else if (message != null && message.RequestType == ProtocolType.Shake)
                {
                    Application.Current.Dispatcher.Invoke(
                        delegate
                        {
                            ShakeEvent?.Invoke();
                        }
                    );
                }
            }
        }

        public void CloseConnection()
        {
            networkManager.CloseConnection();
        }
    }
}
