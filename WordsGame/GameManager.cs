using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    public class GameManager
    {
        int noRounds;
        Worker host;
        Worker artist;
        Round round;

        GameManager() 
        {
            noRounds = 0;
            host = null;
            artist = null;
            round = null;
        }
        GameManager(int noRounds, Worker host, Worker aritst, Round round)
        {
            this.noRounds = noRounds;
            this.host = host;
            this.artist = aritst;
            this.round = round;
        }

        void StartGame()
        {
            round = new Round(artist);
        }




        public void MakeScores()
        {
            //TODO: socring system
        }


    }
}
