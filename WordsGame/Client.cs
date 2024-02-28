using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WordsGame
{
    class Client
    {
        public event MessageEventHandler MessageReceived;
        private TcpClient client;
        private NetworkStream stream;


        public Client(int port)
        {
            client = new TcpClient("127.0.0.1", port);
            stream = client.GetStream();
        }

        public void sendMessage(string message)
        {
            Byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void startListenieng()
        {       
            new Thread(listenForMessage).Start();        
        }
        
        public void listenForMessage()
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
                    MessageReceived?.Invoke(this, new MessageEventArgs(message));
                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
            //
            //Disconnected?.Invoke(this, EventArgs.Empty);

        }
       

    }
}
