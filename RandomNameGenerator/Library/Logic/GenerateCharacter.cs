using Library.Models;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    public class GenerateCharacter
    {
        static readonly Character[] Characters = new Character[54];
        static readonly double bigramWeight = 0.2;
        static readonly double trigramWeight = 0.3;
        static readonly double quadgramWeight = 0.5;

        public static Character SelectCharacter(string prefix)
        {
            //If the prefix is empty or null, return a completely random character
            if (string.IsNullOrEmpty(prefix))
            {
                Console.WriteLine("Prefix is empty or null. Returning a completely random character.");
                return GenerateRandomCharacter();
            }

            //Collect n-gram data based on the prefix length
            var probabilities = GetCharacterProbabilities(prefix);

            if (probabilities.Count == 0)
            {
                Console.WriteLine("No data found for the given prefix. Returning a completely random character.");
                return GenerateRandomCharacter();//If no data is found, return a completely random character
            }

            //Generate a random double between 0 and 1
            var randomDouble = Util.RandomDouble();

            //Select a character based on the random double (weighted probability selection)
            double cumulativeProbability = 0;
            foreach (var kvp in probabilities)
            {
                cumulativeProbability += kvp.Value;
                if (randomDouble <= cumulativeProbability)
                    return kvp.Key;
            }

            //Return a random character if no character is selected
            return GenerateRandomCharacter();
        }

        public static Character GenerateRandomCharacter(bool consonant = true, bool vowel = true, bool specialCharacter = false)
        {
            var validCharacters = Characters
                .Where(character => (consonant && character.isConsonant) ||
                                    (vowel && character.isVowel) ||
                                    (specialCharacter && character.isSpecialCharacter))
                .ToArray();

            return validCharacters.Length > 0 ? validCharacters[Util.RandomInt(validCharacters.Length)] : default;
        }

        public static Dictionary<Character, double> GetCharacterProbabilities(string prefix)
        {
            if (prefix == string.Empty)
            {
                Console.WriteLine("The prefix is empty. Aborting character selection.");
                return [];
            }

            //Filter all n-grams based on the prefix
            Dictionary<NGramKey, double> bigrams = FilterNGrams(NGramDatabase.Bigrams, prefix, 1);
            Dictionary<NGramKey, double> trigrams = FilterNGrams(NGramDatabase.Trigrams, prefix, 2);
            Dictionary<NGramKey, double> quadgrams = FilterNGrams(NGramDatabase.Quadgrams, prefix, 3);

            //Return an empty dictionary if any of the n-gram lists are empty
            if (bigrams.Count == 0 || trigrams.Count == 0 || quadgrams.Count == 0)
            {
                Console.WriteLine("One or more n-gram lists are empty. Aborting character selection.");
                return [];
            }

            //Get all unique NextChar values from each n-gram
            HashSet<char> bigramsNextChar = bigrams.Count != 0 ? [.. bigrams.Keys.Select(key => key.NextChar)] : [];
            HashSet<char> trigramsNextChar = trigrams.Count != 0 ?  [.. trigrams.Keys.Select(key => key.NextChar)] : [];
            HashSet<char> quadgramsNextChar = quadgrams.Count != 0 ? [.. quadgrams.Keys.Select(key => key.NextChar)] : [];

            //Get the common characters that appear in all n-grams
            var commons = bigramsNextChar.Intersect(trigramsNextChar).Intersect(quadgramsNextChar).ToHashSet();

            //Return an empty dictionary if no common characters are found
            if (commons.Count == 0)
            {
                Console.WriteLine("No common next characters found. Aborting character selection.");
                return [];
            }

            //Get all probabilities for the common characters
            Dictionary<Character, double> results = [];
            string bigramPrefix = prefix[..1];
            string trigramPrefix = prefix[..2];
            string quadgramPrefix = prefix[..3];

            //Calculate the weighted probability for each common character
            foreach (char common in commons)
            {
                var bigramProb = GetProbability(bigrams, bigramPrefix, common);
                var trigramProb = GetProbability(trigrams, trigramPrefix, common);
                var quadgramProb = GetProbability(quadgrams, quadgramPrefix, common);

                var weightedProbability = CalculateWeightedProbability(bigramProb, trigramProb, quadgramProb);
                Console.WriteLine($"Calculated weighted probability for '{common}' is {weightedProbability}");

                results.Add(CharacterDatabase.GetCharacter(common),weightedProbability);
            }

            return results;
        }

        public static Dictionary<NGramKey, double> FilterNGrams(Dictionary<NGramKey, double> nGram, string prefix, int n)
        {
            string gramName = n switch
            {
                1 => "bigrams",
                2 => "trigrams",
                3 => "quadgrams",
                _ => throw new ArgumentException("Invalid n-gram length.")
            };
            Console.WriteLine($"Filtering {gramName} with prefix: {prefix}");

            var filtered = prefix.Length >= n ?
                nGram.Where(entry => entry.Key.Prefix.StartsWith(prefix[^n..])).ToDictionary() : [];

            Console.WriteLine($"Filtered {gramName} count: {filtered.Count}");
            foreach (var item in filtered)
            {
                Console.WriteLine($"{item}");
            }

            return filtered;
        }

        private static double GetProbability(Dictionary<NGramKey, double> nGram, string prefix, char character)
        {
            var probability = nGram.TryGetValue(new NGramKey(prefix, character), out double prob) ? prob : 0.0;
            Console.WriteLine($"Probability for {prefix} -> {character}: {probability}");
            return probability;
        }

        public static double CalculateWeightedProbability(double bigramProb, double trigramProb, double quadgramProb)
            => (bigramProb * bigramWeight) + (trigramProb * trigramWeight) + (quadgramProb * quadgramWeight);

        #region For Testing
        public static Dictionary<Character, double> GetCharacterProbabilities(string prefix,
            Dictionary<NGramKey, double> bigram,
            Dictionary<NGramKey, double> trigram,
            Dictionary<NGramKey, double> quadgram)
        {
            if (prefix == string.Empty)
            {
                Console.WriteLine("The prefix is empty. Aborting character selection.");
                return [];
            }

            //Filter all n-grams based on the prefix
            Dictionary<NGramKey, double> bigrams = FilterNGrams(bigram, prefix, 1);
            Dictionary<NGramKey, double> trigrams = FilterNGrams(trigram, prefix, 2);
            Dictionary<NGramKey, double> quadgrams = FilterNGrams(quadgram, prefix, 3);

            //Return an empty dictionary if any of the n-gram lists are empty
            if (bigrams.Count == 0 || trigrams.Count == 0 || quadgrams.Count == 0)
            {
                Console.WriteLine("One or more n-gram lists are empty. Aborting character selection.");
                return [];
            }

            //Get all unique NextChar values from each n-gram
            HashSet<char> bigramsNextChar = bigrams.Count != 0 ? [.. bigrams.Keys.Select(key => key.NextChar)] : [];
            HashSet<char> trigramsNextChar = trigrams.Count != 0 ? [.. trigrams.Keys.Select(key => key.NextChar)] : [];
            HashSet<char> quadgramsNextChar = quadgrams.Count != 0 ? [.. quadgrams.Keys.Select(key => key.NextChar)] : [];

            //Get the common characters that appear in all n-grams
            var commons = bigramsNextChar.Intersect(trigramsNextChar).Intersect(quadgramsNextChar).ToHashSet();

            //Return an empty dictionary if no common characters are found
            if (commons.Count == 0)
            {
                Console.WriteLine("No common next characters found. Aborting character selection.");
                return [];
            }

            //Get all probabilities for the common characters
            Dictionary<Character, double> results = [];
            string bigramPrefix = prefix[..1];
            string trigramPrefix = prefix[..2];
            string quadgramPrefix = prefix[..3];

            //Calculate the weighted probability for each common character
            foreach (char common in commons)
            {
                var bigramProb = GetProbability(bigrams, bigramPrefix, common);
                var trigramProb = GetProbability(trigrams, trigramPrefix, common);
                var quadgramProb = GetProbability(quadgrams, quadgramPrefix, common);

                var weightedProbability = CalculateWeightedProbability(bigramProb, trigramProb, quadgramProb);
                Console.WriteLine($"Calculated weighted probability for '{common}' is {weightedProbability}");

                results.Add(CharacterDatabase.GetCharacter(common), weightedProbability);
            }

            return results;
        }
        #endregion
    }
}
