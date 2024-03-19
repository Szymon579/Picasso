using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WordsGame
{
    class GameManager
    {
        Worker host;
        int noRounds;
        Round round;
        List<Worker> players;
        List<Worker> remainingArtists;
        Worker artist;
        List<Worker> guessers;



        public GameManager(int noRounds, Worker host, ref List<Worker> players)
        {
            this.noRounds = noRounds;
            this.host = host;
            this.players = players;
            this.remainingArtists = new List<Worker>(players);

            
        }
 
        private void EndOfRound(object? sender, EventArgs e)
        {
            round.endOfRound -= EndOfRound;
            SendToAllExcept(null, DataTypeHandler.MakeDataFromLogic(LogicController.endOfRound));

            StartNewRound();
        }

        public void StartNewRound()
        {
            noRounds--;
            if (noRounds < 0)
                return;
            
            artist = GetArtist();
            guessers = GetGuessers();
            
            round = new Round(artist, guessers);

            SetPlayerAsArtist(artist);
            SetPlayersAsGuessers(guessers);

            round.StartChooseWordTimer();
            round.endOfRound += EndOfRound;
        }

        public Worker GetArtist()
        {
            if (remainingArtists.Count < 1)
            {
                return null;
            }

            artist = remainingArtists[remainingArtists.Count - 1];
            remainingArtists.Remove(artist);

            return artist;       
        }

        public void SetPlayerAsArtist(Worker artist)
        {
            if (artist != null)
            {
                byte[] buf = DataTypeHandler.MakeDataFromLogic(LogicController.setAsArtist);
                artist.Send(buf);

                byte[] words = DataTypeHandler.MakeDataFromWords(round.GetRandomWords(3));
                artist.Send(words);
            }
        }

        public List<Worker> GetGuessers() 
        {
            guessers = new List<Worker>();

            for (int i = 0; i < players.Count; i++)
            {
                Worker worker = players[i];
                if (!worker.Equals(artist))
                {
                    guessers.Add(worker);
                }
            }

            return guessers;    
        }

        public void SetPlayersAsGuessers(List<Worker> guessers)
        {
            byte[] bufer = DataTypeHandler.MakeDataFromLogic(LogicController.setAsGuesser);

            for (int i = 0; i < guessers.Count; i++)
            {
                guessers[i].Send(bufer);
                Console.WriteLine("Set as guesser");
            }
        }

        public void SetWord(byte[] data)
        {
            string word = DataTypeHandler.MakeMessageFromData(data);

            round.SetWord(word);

            string hint = String.Concat(Enumerable.Repeat("_ ", word.Length));
            byte[] hintBytes = DataTypeHandler.MakeDataFromLogic(LogicController.sendNumOfLetters, hint);

            SendToAllExcept(round.GetArtist(), hintBytes);

        }

        public void CheckForGuess(Worker sender, byte[] data)
        {
            string message = DataTypeHandler.MakeMessageFromData(data);
            if (round.CheckForGuess(sender, message))
            {
                SendToAllExcept(sender,
                    DataTypeHandler.MakeDataFromMessage(sender.username + " has guessed!"));
            }
            else
            {
                SendToAllExcept(sender,
                    DataTypeHandler.MakeDataFromMessage(sender.username + ": " + message));
            }
        }




        private void SendToAllExcept(Worker sender, byte[] data)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Worker worker = players[i];
                if (!worker.Equals(sender))
                {
                    try
                    {
                        worker.Send(data);
                    }
                    catch (Exception)
                    {
                        players.RemoveAt(i--);
                        worker.Close();
                    }
                }
            }
        }



    }
}
