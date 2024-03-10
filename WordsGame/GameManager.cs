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

        public Worker GetArtist()
        {
            return round.ChooseArtist();
        }

        public List<Worker> getGuessers() 
        {
            return round.GetGuessers();
        }

        public List<string> GetRandomWords()
        {
            List<string> words = round.GetRandomWords(3);
            return words;          
        }

        public void SetWord(string word) 
        {
            round.SetWord(word);
        }
        public bool CheckForGuess(string guess)
        {
            return round.CheckForGuess(guess);
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
