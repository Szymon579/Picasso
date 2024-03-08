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
    }
}
