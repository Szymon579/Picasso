using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    delegate void MessageEventHandler(object sender, MessageEventArgs e);

    class MessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
    }

    class Worker
    {
        public event MessageEventHandler MessageReceived;
        public event EventHandler Disconnected;
        private readonly TcpClient socket;
        private readonly Stream stream;
        public string? Username { get; private set; } = null;

        public Worker(TcpClient socket)
        {
            this.socket = socket;
            this.stream = socket.GetStream();
        }

        public void Send(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }

        public void Start()
        {
            new Thread(Run).Start();
        }

        private void Run()
        {
            byte[] buffer = new byte[2018];
            try
            {
                while (true)
                {
                    int receivedBytes = stream.Read(buffer, 0, buffer.Length);
                    if (receivedBytes < 1)
                        break;
                    string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                    if (Username == null)
                        Username = message;
                    else
                        MessageReceived?.Invoke(this, new MessageEventArgs(message));
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        public void Close()
        {
            socket.Close();
        }
    }
}
