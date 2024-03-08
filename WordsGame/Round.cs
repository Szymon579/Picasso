using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
    class Round
    {
        Worker artist;
        string word;
        //System.Timers.Timer timer;

        public Round()
        {
            
        }

        public void chooseWord()
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
