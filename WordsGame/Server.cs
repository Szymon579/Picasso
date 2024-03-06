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
        //public event DataEventHandler IncomingMessage;
        private TcpListener serverSocket;
        private List<Worker> workers = new List<Worker>();
        private Worker hostWorker;

        public Server(string host, string port)
        {
            serverSocket = new TcpListener(IPAddress.Parse(host), int.Parse(port));
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

        private void Worker_DataReceived(object sender, DataEventArgs e)
        {
            RouteData(sender as Worker, e.data);           
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
                worker.DataReceived += Worker_DataReceived;
            }
        }

        private void RemoveWorker(Worker worker)
        {
            lock (this)
            {
                worker.Disconnected -= Worker_Disconnected;
                worker.DataReceived -= Worker_DataReceived;
                workers.Remove(worker);
                worker.Close();
            }
        }

        private void RouteData(Worker from, byte[] data)
        {
            lock (this)
            {
                //message = string.Format("{0}: {1}", from.Username, message);               
                //IncomingMessage?.Invoke(this, new DataEventArgs(data));

                if (data[0] == DataParser.logicDataCode)
                {
                    logicRouter(from, data);
                    return;
                }

                
                for (int i = 0; i < workers.Count; i++)
                {
                    Worker worker = workers[i];
                    if (!worker.Equals(from))
                    {
                        try
                        {
                            worker.Send(data);
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

        private void logicRouter(Worker from, byte[] data)
        {
            byte logicCode = data[1];

            if(logicCode == LogicController.playerConnected)
            {
                from.Send(DataParser.MakeDataFromString("player conn"));
            }
            else if (logicCode == LogicController.setAsHost)
            {
                hostWorker = from;
                Console.WriteLine("Worker set as host");
            }
            else if(logicCode == LogicController.setAsArtist)
            {

            }
            else if (logicCode == LogicController.setAsGuesser)
            {

            }





        }



    }
}
