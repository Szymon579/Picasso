using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    delegate void DataEventHandler(object sender, DataEventArgs e);

    class DataEventArgs : EventArgs
    {
        public byte[] data { get; private set; }

        public DataEventArgs(byte[] data)
        {
            this.data = data;
        }
    }

    class Worker
    {
        public event DataEventHandler DataReceived;
        public event EventHandler Disconnected;
        private readonly TcpClient socket;
        private readonly Stream stream;
        public string? Username { get; private set; } = null;

        public Worker(TcpClient socket)
        {
            this.socket = socket;
            this.stream = socket.GetStream();
        }

        public void Send(byte[] data)
        {
            //byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void Start()
        {
            new Thread(Run).Start();
        }

        private void Run()
        {
            byte[] buffer = new byte[Int32.MaxValue / 2];
            try
            {
                while (true)
                {
                    int receivedBytes = stream.Read(buffer, 0, buffer.Length);
                    if (receivedBytes < 1)
                        break;
                    string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                    
                    byte[] bytes = new byte[receivedBytes];
                    Array.Copy(buffer, bytes, receivedBytes);

                    if (Username == null)
                        Username = message;
                    else
                        DataReceived?.Invoke(this, new DataEventArgs(bytes));
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
