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
                //message = string.Format("{0}: {1}", from.Username, message);               
                //IncomingMessage?.Invoke(this, new DataEventArgs(data));

                try
                {
                    logicRouter(from, data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
        }

        private void logicRouter(Worker sender, byte[] data)
        {
            byte logicCode = data[0];

            if(logicCode == LogicController.playerConnected)
            {
                sender.Send(DataTypeHandler.MakeDataFromMessage("player conn"));
            }
            else if (logicCode == LogicController.setAsHost)
            {
                hostWorker = sender;
                Console.WriteLine("Worker set as host");
            }
            else if(logicCode == LogicController.setAsArtist)
            {

            }
            else if (logicCode == LogicController.setAsGuesser)
            {

            }
            else if (logicCode == LogicController.gameStart)
            {
                gameManager = new GameManager(3, null, ref workers);
                gameManager.StartGame();
                
                Worker artist = gameManager.GetArtist();

                if (artist != null)
                {
                    byte[] buf = DataTypeHandler.MakeDataFromLogic(LogicController.setAsArtist);
                    artist.Send(buf);

                    byte[] words = DataTypeHandler.MakeDataFromWords(gameManager.GetRandomWords());
                    artist.Send(words);
                }

                byte[] bufer = DataTypeHandler.MakeDataFromLogic(LogicController.setAsGuesser);

                List<Worker> guessers = gameManager.getGuessers();
                for (int i = 0; i < guessers.Count; i++)
                {
                    guessers[i].Send(bufer);
                    Console.WriteLine("Set as guesser");
                }

            }
            else if (logicCode == LogicController.sendChoosenWord)
            {
                //TODO: send _ _ _ _ _ (letters count) to guessers
                string word = DataTypeHandler.MakeMessageFromData(data);
                string hint = String.Concat(Enumerable.Repeat("_ ", word.Length));

                byte[] hintBytes = DataTypeHandler.MakeDataFromLogic(LogicController.sendHint, hint);

                SendToAllExceptSender(sender, hintBytes);
            }
            else if (logicCode == LogicController.sendMessage)
            {
                //TODO: check for potential guess
                SendToAllExceptSender(sender, data);
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



    }
}
