using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    class GameManager
    {
        int noRounds;
        Worker host;
        Round round;
        List<Worker> players;

        GameManager() 
        {
            noRounds = 0;
            host = null;
            round = null;
            players = null;
        }
        public GameManager(int noRounds, Worker host, ref List<Worker> players)
        {
            this.noRounds = noRounds;
            this.host = host;
            this.players = players;
        }
        public void StartGame()
        {
            round = new Round(players);
        }

        public void GetArtist()
        {
            Worker artist = round.ChooseArtist();

            if (artist != null)
            {
                byte[] buf = DataTypeHandler.MakeDataFromLogic(LogicController.setAsArtist);
                artist.Send(buf);

                byte[] words = DataTypeHandler.MakeDataFromWords(GetRandomWords());
                artist.Send(words);
            }
        }

        public void GetGuessers() 
        {
            byte[] bufer = DataTypeHandler.MakeDataFromLogic(LogicController.setAsGuesser);

            List<Worker> guessers = round.GetGuessers();
            for (int i = 0; i < guessers.Count; i++)
            {
                guessers[i].Send(bufer);
                Console.WriteLine("Set as guesser");
            }
        }

        public List<string> GetRandomWords()
        {
            List<string> words = round.GetRandomWords(3);
            return words;          
        }

        public void SetWord(byte[]data) 
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
            if (round.CheckForGuess(message))
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



        public void FinishGame() 
        {
            //TODO: implement game end/restart
        }

        public void MakeScores()
        {
            //TODO: socring system
        }


    }
}
