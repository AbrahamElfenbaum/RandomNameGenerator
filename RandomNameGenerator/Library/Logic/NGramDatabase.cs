using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    public class NGramDatabase
    {
        public static Dictionary<char, double> FirstLetters { get; private set; } = [];
        public static Dictionary<NGramKey, double> Bigrams { get; private set; } = [];
        public static Dictionary<NGramKey, double> Trigrams { get; private set; } = [];
        public static Dictionary<NGramKey, double> Quadgrams { get; private set; } = [];

        public static void Train(IEnumerable<string> names)
        {
            FirstLetters = new Dictionary<char, double>(Probability.FirstLetters(names));
            Bigrams = new Dictionary<NGramKey, double>(Probability.Bigrams(names));
            Trigrams = new Dictionary<NGramKey, double>(Probability.Trigrams(names));
            Quadgrams = new Dictionary<NGramKey, double>(Probability.Quadgrams(names));
        }
    }
}
