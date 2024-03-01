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
        private TcpClient client;
        private NetworkStream stream;

        private byte messageCode = 100;
        private byte canvasCode = 200;


        public Client(int port)
        {
            client = new TcpClient("127.0.0.1", port);
            stream = client.GetStream();
        }

        public void SendMessage(string message)
        {
            Byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            Byte[] readyBytes = new Byte[messageBytes.Length + 1];
            readyBytes[0] = messageCode;
            messageBytes.CopyTo(readyBytes, 1);
            
   
            stream.Write(readyBytes, 0, readyBytes.Length);
            //stream.Write(messageBytes, 0, messageBytes.Length);
        }

        public void SendCanvasUpdate(Bitmap bmp)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Bmp);

                byte[] bmpData = ms.ToArray();

                byte[] dataLength = BitConverter.GetBytes(bmpData.Length);

                byte[] readyBytes = new byte[1 + dataLength.Length + bmpData.Length];
                
                readyBytes[0] = canvasCode;
                Array.Copy(dataLength, 0, readyBytes, 1, dataLength.Length);
                Array.Copy(bmpData, 0, readyBytes, 5, bmpData.Length);


                stream.Write(readyBytes, 0, readyBytes.Length);

                Console.WriteLine("BMP image sent successfully.");
            }

            
        }

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

                    if (buffer[0] == messageCode)
                    {
                        //string message = Encoding.UTF8.GetString(buffer, 1, receivedBytes - 1);
                        byte[] bytes = new byte[receivedBytes - 1];
                        Array.Copy(buffer, 1, bytes, 0, receivedBytes - 1);
                        MessageReceived?.Invoke(this, new DataEventArgs(bytes));
                    }
                    else if (buffer[0] == canvasCode)
                    {
                        //string message = Encoding.UTF8.GetString(buffer, 1, receivedBytes - 1);
                        byte[] bytes = new byte[receivedBytes - 1];
                        Array.Copy(buffer, 1, bytes, 0, receivedBytes - 1);
                        CanvasReceived?.Invoke(this, new DataEventArgs(bytes));
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
