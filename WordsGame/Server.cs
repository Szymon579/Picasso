using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    class Server
    {
        public event MessageEventHandler IncomingMessage;
        private TcpListener serverSocket;
        private List<Worker> workers = new List<Worker>();

        public Server(int port)
        {
            serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            serverSocket.Start();
        }

        public void WaitForConnection()
        {
            while (true)
            {
                TcpClient socket = serverSocket.AcceptTcpClient();
                Worker worker = new Worker(socket);
                AddWorker(worker);
                worker.Start();
            }
        }

        private void Worker_MessageReceived(object sender, MessageEventArgs e)
        {
            BroadcastMessage(sender as Worker, e.Message);
            
        }

        private void Worker_Disconnected(object sender, EventArgs e)
        {
            RemoveWorker(sender as Worker);
        }

        private void AddWorker(Worker worker)
        {
            lock (this)
            {
                workers.Add(worker);
                worker.Disconnected += Worker_Disconnected;
                worker.MessageReceived += Worker_MessageReceived;
            }
        }

        private void RemoveWorker(Worker worker)
        {
            lock (this)
            {
                worker.Disconnected -= Worker_Disconnected;
                worker.MessageReceived -= Worker_MessageReceived;
                workers.Remove(worker);
                worker.Close();
            }
        }

        private void BroadcastMessage(Worker from, String message)
        {
            lock (this)
            {
                message = string.Format("{0}: {1}", from.Username, message);
                
                IncomingMessage?.Invoke(this, new MessageEventArgs(message));
                
                for (int i = 0; i < workers.Count; i++)
                {
                    Worker worker = workers[i];
                    if (!worker.Equals(from))
                    {
                        try
                        {
                            worker.Send(message);
                        }
                        catch (Exception)
                        {
                            workers.RemoveAt(i--);
                            worker.Close();
                        }
                    }
                }
            }


        }
    }
}
