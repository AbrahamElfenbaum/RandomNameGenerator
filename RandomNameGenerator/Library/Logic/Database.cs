using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    class DataBase
    {
        public Dictionary<char, double> FirstLetters { get; private set; } = [];
        public Dictionary<NGramKey, double> Bigrams { get; private set; } = [];
        public Dictionary<NGramKey, double> Trigrams { get; private set; } = [];
        public Dictionary<NGramKey, double> Quadgrams { get; private set; } = [];

        public void Train(IEnumerable<string> names)
        {
            FirstLetters = new Dictionary<char, double>(Probability.FirstLetters(names));
            Bigrams = new Dictionary<NGramKey, double>(Probability.Bigrams(names));
            Trigrams = new Dictionary<NGramKey, double>(Probability.Trigrams(names));
            Quadgrams = new Dictionary<NGramKey, double>(Probability.Quadgrams(names));
        }
    }
}
