using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
        public event DataEventHandler MessageReceived;
        public event DataEventHandler CanvasReceived;
        public event DataEventHandler LogicReceived;

        private TcpClient client;
        private NetworkStream stream;
        private string username;



        public Client(string host, string port)
        {         
            client = new TcpClient(host, int.Parse(port));
            //client = new TcpClient("127.0.0.1", port);

            stream = client.GetStream();
        }

        public void SetUsername(string username)
        {
            this.username = username;
        }

        public void SendMessage(string message)
        {
            message = username + ": " + message;
            byte[] data = DataParser.MakeDataFromString(message);
            stream.Write(data, 0, data.Length);
        }

        public void SendCanvas(Bitmap bmp)
        {
            byte[] data = DataParser.MakeDataFromBitmap(bmp);
            stream.Write(data, 0, data.Length);
        }

        public void SendLogic(byte logic)
        {
            byte[] data = DataParser.MakeDataFromLogic(logic);
            stream.Write(data, 0, data.Length);
        }

        //public void SendCanvas(Bitmap bmp)
        //{
        //    byte[] data = DataParser.MakeDataFromBitmap(bmp);
        //    stream.Write(data, 0, data.Length);
        //}

        public void StartListeningForData()
        {       
            new Thread(ListenForData).Start();        
        }

        public void ListenForData()
        {
            byte[] buffer = new byte[Int32.MaxValue / 2];
            try
            {
                while (true)
                {
                    int receivedBytes = stream.Read(buffer, 0, buffer.Length);
                    if (receivedBytes < 1)
                        break;

                    byte[] bytes = new byte[receivedBytes - 1];
                    Array.Copy(buffer, 1, bytes, 0, receivedBytes - 1);

                    if (buffer[0] == DataParser.messageDataCode)
                    {
                        MessageReceived?.Invoke(this, new DataEventArgs(bytes));
                    }
                    else if (buffer[0] == DataParser.canvasDataCode)
                    {
                        CanvasReceived?.Invoke(this, new DataEventArgs(bytes));
                    }
                    else if (buffer[0] == DataParser.logicDataCode)
                    {
                        LogicReceived?.Invoke(this, new DataEventArgs(bytes));
                    }

                }
            }
            catch (IOException) { }
            catch (ObjectDisposedException) { }
            //
            //Disconnected?.Invoke(this, EventArgs.Empty);

        }




    }
}
