using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatApp.Model
{

    internal class NetworkManager
    {
        class NoConnectionException : Exception { }

        class AlreadyConnectedException : Exception { }

        private TcpClient? tcpClient;

        public bool IsConnected => tcpClient != null && tcpClient.Connected;

        public void StartListening(string ip, int port)
        {
            if (IsConnected)
            {
                throw new AlreadyConnectedException();
            }

            Console.WriteLine("Listening...");

            // Start listening
            IPAddress ipAddress = IPAddress.Parse(ip);
            TcpListener listener = new (ipAddress, port);
            listener.Start();

            // Wait for connection
            tcpClient = listener.AcceptTcpClient();
            Console.WriteLine("Connected!");
        }

        public bool Connect(string ip, int port)
        {
            if (IsConnected)
            {
                throw new AlreadyConnectedException();
            }

            // Connect to server
            try
            {
                tcpClient = new TcpClient(ip, port);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public string ReadMessage()
        {
            if (!IsConnected)
            {
                throw new NoConnectionException();
            }

            // Get a stream object for reading and writing
            NetworkStream networkStream = tcpClient.GetStream();

            // Read data to buffer
            byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
            int bytesRead = networkStream.Read(buffer, 0, tcpClient.ReceiveBufferSize);

            // Convert to string
            string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine("Received : " + dataReceived);

            return dataReceived;
        }


        public void SendMessage(string str)
        {
            if (!IsConnected)
            {
                throw new NoConnectionException();
            }

            // Get a stream object for reading and writing
            NetworkStream networkStream = tcpClient.GetStream();

            // Convert to bytes
            byte[] buffer = Encoding.UTF8.GetBytes(str);

            // Send data
            networkStream.Write(buffer, 0, buffer.Length);

            Console.WriteLine("Sent : " + str); 
        }

        public void CloseConnection()
        {
            tcpClient?.Close();
            tcpClient = null;
        }
    }
}
