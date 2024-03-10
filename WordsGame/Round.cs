using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WordsGame
{
    class Round
    {
        List<Worker> players;
        Worker artist;
        List<Worker> remainingArtists;
        

        string word;
        static System.Timers.Timer timer;

        public Round(List<Worker> players)
        {
            this.remainingArtists = new List<Worker>(players);
            this.players = new List<Worker>(players);

            SetTimer();
        }

        private static void SetTimer()
        {
            timer = new System.Timers.Timer(10000);
 
            timer.Elapsed += OnTimeoutEvent;
            timer.Enabled = true;
        }

        private static void OnTimeoutEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer timeout");
        }
        public Worker ChooseArtist()
        {
            if (remainingArtists.Count < 1)
            {
                return null;
            }

            artist = remainingArtists[remainingArtists.Count - 1];
            remainingArtists.Remove(artist);

            return artist;
        }
        public List<Worker> GetGuessers()
        {
            List<Worker> guessers = new List<Worker>();
            
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

        public void ChooseWord()
        {

        }
        
        public List<string> GetRandomWords(int count)
        {
            List<string> words = new List<string>();

            words.Add("POL");
            words.Add("ENG");
            words.Add("FRA");
            //TODO: handle getting words from file

            return words;
        }

        public bool isGuessed(string guess)
        {
            return guess == word;
        }

    }
}
