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
                return GenerateRandomCharacter(true, true, true);
            }

            //Generate a random double between 0 and 1
            var randomDouble = Util.RandomDouble();

            //Collect n-gram data based on the prefix length
            var probabilities = GetCharacterProbabilities(prefix);

            if (probabilities.Count == 0)
            {
                Console.WriteLine("No data found for the given prefix. Returning a completely random character.");
                return GenerateRandomCharacter(true, true, true);//If no data is found, return a completely random character
            }

#if false
            //TODO: Determine possible characters that can follow the prefix
            var charProbabilities = CalculateCharacterProbabilities(relevantNGrams);

            //TODO: Ensure only characters present in all relevant n-grams are included
            var validCharacters = FilterCommonCharacters(charProbabilities);

            if (validCharacters.Count == 0)
            {
                Console.WriteLine("No valid characters found for the given prefix. Returning a completely random character.");
                return GenerateRandomCharacter(true, true, true);//If no valid characters are found, return a completely random character
            }

            //Sort characters by probability (ascending)
            var sortedCharacters = validCharacters.OrderBy(kv => kv.Value).ToList(); 
#endif

            //Select a character based on the random double (weighted probability selection)
            double cumulativeProbability = 0;
            foreach (var kvp in probabilities)
            {
                cumulativeProbability += kvp.Value;
                if (randomDouble <= cumulativeProbability)
                    return kvp.Key;
            }

            //Return a random character if no character is selected
            return GenerateRandomCharacter(true, false, false);
        }

        public static Character GenerateRandomCharacter(bool consonant, bool vowel, bool specialCharacter)
        {
            var validCharacters = Characters
                .Where(character => !(consonant && character.isConsonant) ||
                                    !(vowel && character.isVowel) ||
                                    !(specialCharacter && character.isSpecialCharacter))
                .ToArray();

            return validCharacters.Length > 0 ? validCharacters[Util.RandomInt(validCharacters.Length)] : default;
        }

#if false
        static List<NGramData> GetRelevantNGrams(string prefix)
        {
            var bigram = NGramDatabase.Bigrams
                .Where(entry => entry.Key.Prefix.StartsWith(prefix[..1]));
            var trigram = NGramDatabase.Trigrams
                .Where(entry => entry.Key.Prefix.StartsWith(prefix[..2])); ;
            var quadgram = NGramDatabase.Quadgrams
                .Where(entry => entry.Key.Prefix.StartsWith(prefix[..3])); ;

            throw new NotImplementedException();
        }

        static Dictionary<Character, double> CalculateCharacterProbabilities(List<NGramData> relevantNGrams)
        {
            throw new NotImplementedException();
        }

        static Dictionary<Character, double> FilterCommonCharacters(Dictionary<Character, double> charProbabilities)
        {
            throw new NotImplementedException();
        } 
#endif

        public static Dictionary<Character, double> GetCharacterProbabilities(string prefix)
        {
            //Get all n-grams that start with the prefix
            var bigrams = GetNGram(NGramDatabase.Bigrams, prefix, 1);
            var trigrams = GetNGram(NGramDatabase.Trigrams, prefix, 2);
            var quadgrams = GetNGram(NGramDatabase.Quadgrams, prefix, 3);

            //Get all unique NextChar values from each n-gram
            var bigramsNextChar = bigrams.Keys.Select(key => key.NextChar).ToHashSet();
            var trigramsNextChar = trigrams.Keys.Select(key => key.NextChar).ToHashSet();
            var quadgramsNextChar = quadgrams.Keys.Select(key => key.NextChar).ToHashSet();

            //Get the common characters that appear in all n-grams
            var commons = bigramsNextChar.Intersect(trigramsNextChar).Intersect(quadgramsNextChar).ToHashSet();

            Dictionary<Character, double> result = [];

            //Get all probabilities for the common characters
            foreach (var common in commons)
            {
                double bigramProb = bigrams.TryGetValue(new NGramKey(prefix[..1], common), out double bProb) ? bProb : 0.0;
                double trigramProb = trigrams.TryGetValue(new NGramKey(prefix[..2], common), out double tProb) ? tProb : 0.0;
                double quadgramProb = quadgrams.TryGetValue(new NGramKey(prefix[..3], common), out double qProb) ? qProb : 0.0;



                result.Add(CharacterDatabase.GetCharacter(common), 
                           CalculateWeightedProbability(bigramProb, trigramProb, quadgramProb));
            }

            return result;
        }

        public static Dictionary<NGramKey, double> GetNGram(Dictionary<NGramKey, double> nGram, string prefix, int n)
            => nGram.Where(entry => entry.Key.Prefix.StartsWith(prefix[..n])).ToDictionary();

        public static double CalculateWeightedProbability(double bigramProb, double trigramProb, double quadgramProb)
            => (bigramProb * bigramWeight) + (trigramProb * trigramWeight) + (quadgramProb * quadgramWeight);
    }
}
