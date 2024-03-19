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
        private TcpListener serverSocket;
        private List<Worker> workers = new List<Worker>();
        private GameManager gameManager;
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
                try
                {
                    LogicRouter(from, data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }


        private void LogicRouter(Worker sender, byte[] data)
        {
            byte logicCode = data[0];

            if (logicCode == LogicController.playerConnected)
            {
                SendToAllExceptSender(null, DataTypeHandler.MakeDataFromLogic(
                    LogicController.updateLobby, GetConnectedUsernames()));
            }
            else if (logicCode == LogicController.setAsHost)
            {
                Console.WriteLine("Worker set as host");
            }
            else if (logicCode == LogicController.gameStart)
            {
                gameManager = new GameManager(3, sender, ref workers);
                gameManager.StartNewRound();
            }
            else if (logicCode == LogicController.sendChoosenWord)
            {
                gameManager.SetWord(data);
            }
            else if (logicCode == LogicController.sendMessage)
            {
                gameManager.CheckForGuess(sender, data);
            }
            else if (logicCode == LogicController.sendBitmap)
            {
                SendToAllExceptSender(sender, data);
            }

        }

        private void SendToAllExceptSender(Worker sender, byte[] data)
        {
            for (int i = 0; i < workers.Count; i++)
            {
                Worker worker = workers[i];
                if (!worker.Equals(sender))
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

        private string GetConnectedUsernames()
        {
            string usernames = "";

            foreach (Worker worker in workers)
            {
                usernames += worker.username + '\n';
            }

            return usernames;
        }


    }
}
