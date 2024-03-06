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
        System.Timers.Timer timer;


        public Round(Worker artist)
        {
            this.artist = artist;
        }

        public void chooseWord()
        {
            word = "Car";
        }
        
        public List<string> GetRandomWords(int count)
        {
            List<string> words = new List<string>(count);

            //TODO: handle getting words from file
            words[0] = "Policeman";
            words[1] = "Orange";
            words[2] = "School";

            return words;
        }

        public bool isGuessed(string guess)
        {
            return guess == word;
        }

    }
}
