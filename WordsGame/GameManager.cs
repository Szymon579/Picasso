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
        Worker artist;
        Round round;
        List<Worker> players;
        List<Worker> remainingArtists;

        GameManager() 
        {
            noRounds = 0;
            host = null;
            artist = null;
            round = null;
            players = null;
        }
        public GameManager(int noRounds, Worker host, ref List<Worker> players)
        {
            this.noRounds = noRounds;
            this.host = host;
            this.players = players;
            this.remainingArtists = players;
        }

        public Worker chooseArtist()
        {
            if(remainingArtists.Count < 1) 
            {
                return null;
            }

            artist = remainingArtists[remainingArtists.Count - 1];
            remainingArtists.Remove(artist);

            return artist;
        }

        public List<Worker> getGuessers() 
        {
            //List<Worker> guessers = new List<Worker>();
            //foreach(Worker worker in players) 
            //{ 
            //    if(!artist.Equals(worker)) 
            //        guessers.Add(worker);
            //}
            //return guessers;

            return remainingArtists;
        }

        public void StartGame()
        {
            round = new Round();
            
            //for (int i = 0; i < noRounds; i++)
            //{
            //    round = new Round();
            //}

            //FinishGame();
        }

        public byte[] getWords()
        {
            List<string> words = round.GetRandomWords(3);
            string str = String.Join(" ", words);
            Console.WriteLine("String: ");
            Console.WriteLine(str);
            //str = "SIEMA";
            
            byte[] bytes = DataParser.MakeDataFromString(str);
            bytes[0] = LogicController.emitWords;

            //byte[] readyBytes = new byte[bytes.Length + 1];
            //Array.Copy(bytes, 0, readyBytes, 1, bytes.Length);

            //readyBytes[0] = DataParser.logicDataCode;

            Console.WriteLine("getWords");
            return bytes;
            
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
