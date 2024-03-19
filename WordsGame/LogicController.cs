using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsGame
{
     class LogicController
    {
        public static readonly byte playerConnected = 100;
        public static readonly byte setAsHost       = 101;
        public static readonly byte setAsArtist     = 102;
        public static readonly byte setAsGuesser    = 103;
        public static readonly byte sendWordsToChoose = 105;
        public static readonly byte gameStart       = 106;
        public static readonly byte sendChoosenWord = 107;
        public static readonly byte sendMessage = 108;
        public static readonly byte sendBitmap = 110;
        public static readonly byte sendNumOfLetters = 111;
        public static readonly byte updateLobby = 112;
        public static readonly byte displayScores = 113;
        public static readonly byte endOfRound = 114;


    }
}
