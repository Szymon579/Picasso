using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WordsGame
{
    class Round
    {
        Worker artist;
        List<Worker> guessers;
        
        string word;
        public event EventHandler endOfRound;

        static System.Timers.Timer roundTimer;
        static System.Timers.Timer chooseWordTimer;
        static System.Timers.Timer scoreboardTimer;

        public Round(Worker artist, List<Worker> guessers)
        {
            this.artist = artist;
            this.guessers = guessers;

            SetTimers(15, 10, 5);
        }

        public void StartChooseWordTimer()
        {
            chooseWordTimer.Enabled = true;
        }

        public void SetWord(string word)
        {
            this.word = word;
            roundTimer.Enabled = true;
            
        }
        public Worker GetArtist()
        {
            return artist;
        }

        public List<Worker> GetGuesers()
        {
            return guessers;
        }

        public List<string> GetRandomWords(int count)
        {
            List<string> words = new List<string>();

            words.Add("POL");
            words.Add("ENG");
            words.Add("FRA");

            return words;
        }

        public bool CheckForGuess(Worker sender, string guess)
        {

            if (word == guess && guessers.Contains(sender))
            {
                guessers.Remove(sender);

                if (guessers.Count == 0)
                    endOfRound?.Invoke(this, null);

                return true;
            }
            return false;
        }

        private static void SetTimers(int roundTime, int chooseWordTime, int scoreboardTime)
        {
            roundTime *= 1000;
            chooseWordTime *= 1000;
            scoreboardTime *= 1000;

            roundTimer = new System.Timers.Timer(roundTime);
            roundTimer.Enabled = false;
            roundTimer.Elapsed += OnTurnTimeoutEvent;

            chooseWordTimer = new System.Timers.Timer(chooseWordTime);
            chooseWordTimer.Enabled = false;
            chooseWordTimer.Elapsed += OnChooseWordTimeoutEvent;

            scoreboardTimer = new System.Timers.Timer(scoreboardTime);
            scoreboardTimer.Enabled = false;
            scoreboardTimer.Elapsed += OnScoreboardTimeoutEvent;
        }

        private static void OnTurnTimeoutEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Round timeout");
        }

        private static void OnChooseWordTimeoutEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Choose word timeout");

        }

        private static void OnScoreboardTimeoutEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Scoreboard timeout");
        }

    }
}
