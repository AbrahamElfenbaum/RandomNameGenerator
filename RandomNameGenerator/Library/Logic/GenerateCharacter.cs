using Library.Models;
using Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    class GenerateCharacter
    {
        static readonly Character[] Characters = new Character[54];

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

            //TODO: Collect n-gram data based on the prefix length
            var relevantNGrams = GetRelevantNGrams(prefix);

            if (relevantNGrams.Count == 0)
            {
                Console.WriteLine("No data found for the given prefix. Returning a completely random character.");
                return GenerateRandomCharacter(true, true, true);//If no data is found, return a completely random character
            }

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

            //Select a character based on the random double (weighted probability selection)
            double cumulativeProbability = 0;
            foreach (var kvp in sortedCharacters)
            {
                cumulativeProbability += kvp.Value;
                if (randomDouble <= cumulativeProbability)
                    return kvp.Key;
            }

            //Return a random character if no character is selected
            return GenerateRandomCharacter(true, false, false);
        }

        static Character GenerateRandomCharacter(bool consonant, bool vowel, bool specialCharacter)
        {
            var validCharacters = Characters
                .Where(character => !(consonant && character.isConsonant) ||
                                    !(vowel && character.isVowel) ||
                                    !(specialCharacter && character.isSpecialCharacter))
                .ToArray();

            return validCharacters.Length > 0 ? validCharacters[Util.RandomInt(validCharacters.Length)] : default;
        }

        static List<NGramData> GetRelevantNGrams(string prefix)
        {
            var bigram = DataBase.Bigrams
                .Where(entry => entry.Key.Prefix.StartsWith(prefix[..1]));
            var trigram = DataBase.Trigrams
                .Where(entry => entry.Key.Prefix.StartsWith(prefix[..2])); ;
            var quadgram = DataBase.Quadgrams
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
    }
}
