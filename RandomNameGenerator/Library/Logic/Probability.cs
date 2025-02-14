using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    public class Probability
    {
        #region Base
        /// <summary>
        /// Retrieves raw counts of items based on a specified function or action. 
        /// It either runs a function to calculate counts or applies an action to 
        /// accumulate counts for each item in the provided collection.
        /// </summary>
        /// <typeparam name="T">The type of result to return (usually a 
        /// collection like a dictionary or list).</typeparam>
        /// <param name="names">An enumerable collection of strings for which 
        /// the raw counts will be calculated.</param>
        /// <param name="rawCountsFunc">A function that generates the raw counts 
        /// based on the provided collection.</param>
        /// <param name="rawCountsItem">An optional action to be applied to each 
        /// item in the collection to update the raw counts.</param>
        /// <returns>A collection of counts based on the logic provided by either 
        /// <paramref name="rawCountsFunc"/> or <paramref name="rawCountsItem"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="names"/> 
        /// collection is null.</exception>

        static T? GetRawCounts_Base<T>
            (IEnumerable<string> names,
            Func<IEnumerable<string>, T> rawCountsFunc,
            Action<T, string>? rawCountsItem = null) where T : new()
        {
            //Is the input null
            if (names == null)
                throw new ArgumentNullException(nameof(names), "Input enumerable cannot be null.");

            //Is the input empty
            if (!names.Any())
            {
                Console.WriteLine("Input enumerable is empty. Returning an empty result.");
                return new T();
            }

            T counts = new();
            if (rawCountsItem != null)
            {
                foreach (var name in names)
                    rawCountsItem(counts, name);
                return counts;
            }

            //Run the logic to get the raw counts of the input
            return rawCountsFunc(names);
        }

        /// <summary>
        /// Retrieves raw counts for n-grams from a collection of strings. An 
        /// n-gram is a sequence of n characters from a string.
        /// This method uses the base logic of the `GetRawCounts_Base` method to 
        /// calculate n-gram counts for each string in the collection.
        /// </summary>
        /// <param name="names">An enumerable collection of strings for which 
        /// the n-gram counts will be calculated.</param>
        /// <param name="nGramCount">The length of the n-gram (e.g., 2 for bigrams, 
        /// 3 for trigrams).</param>
        /// <param name="nGramName">A name describing the n-gram type (e.g., "bigram" 
        /// or "trigram").</param>
        /// <returns>A dictionary with NGramKey as the key (prefix and 
        /// next character) and the count as the value.</returns>

        static Dictionary<NGramKey, int> 
            GetRawCounts_NGram_Base(IEnumerable<string> names, int nGramCount, string nGramName)
        {
            return GetRawCounts_Base
                (names,
                input => new Dictionary<NGramKey, int>(),
                (counts, name) =>
                {
                    if (name.Length >= nGramCount)
                    {
                        var mod = nGramCount - 1;
                        var lName = name.ToLower();
                        for (int i = 0; i < lName.Length - mod; i++)
                        {
                            var prefix = lName.Substring(i, mod);
                            var nextChar = lName[i + mod];
                            var key = new NGramKey(prefix, nextChar);
                            if (counts.TryGetValue(key, out int value))
                                counts[key] = ++value;
                            else
                                counts[key] = 1;
                        }
                    }
                    else
                    {
                        Console.WriteLine
                        ($"Skipping name '{name}' as it is too short for {nGramName} analysis.");
                    }
                }) ?? [];
        }
        #endregion

        #region Get Raw Counts
        /// <summary>
        /// Retrieves raw counts of the first letter of each name in a collection. 
        /// It tallies how many times each first letter appears in the provided 
        /// collection of names.
        /// </summary>
        /// <param name="names">An enumerable collection of strings (names) from 
        /// which the first letter counts will be calculated.</param>
        /// <returns>A dictionary where the key is the first letter of each name, 
        /// and the value is the number of occurrences of that letter.</returns>

        public static Dictionary<char, int>
            GetRawCounts_FirstLetter(IEnumerable<string> names)
        {
            return GetRawCounts_Base
                (names,
                input => new Dictionary<char, int>(),
                (counts, name) =>
                {
                    char firstLetter = char.ToUpper(name[0]);
                    if (counts.TryGetValue(firstLetter, out int value))
                        counts[firstLetter] = ++value;
                    else
                        counts[firstLetter] = 1;
                }) ?? [];
        }

        /// <summary>
        /// Retrieves raw counts for bigrams (two-character sequences) from 
        /// a collection of strings.
        /// This method leverages the base `GetRawCounts_NGram_Base` logic to 
        /// calculate bigram counts for each string in the collection.
        /// </summary>
        /// <param name="names">An enumerable collection of strings for which 
        /// the bigram counts will be calculated.</param>
        /// <returns>A dictionary with NGramKey as the key (prefix and next 
        /// character) and the count as the value for each bigram in the collection.</returns>

        public static Dictionary<NGramKey, int>
            GetRawCounts_Bigram(IEnumerable<string> names)
            => GetRawCounts_NGram_Base(names, 2, "bigram");

        /// <summary>
        /// Retrieves raw counts for trigrams (three-character sequences) from 
        /// a collection of strings.
        /// This method leverages the base `GetRawCounts_NGram_Base` logic to 
        /// calculate trigram counts for each string in the collection.
        /// </summary>
        /// <param name="names">An enumerable collection of strings for which 
        /// the bigram counts will be calculated.</param>
        /// <returns>A dictionary with NGramKey as the key (prefix and next 
        /// character) and the count as the value for each trigram in the collection.</returns>
        public static Dictionary<NGramKey, int>
            GetRawCounts_Trigram(IEnumerable<string> names)
            => GetRawCounts_NGram_Base(names, 3, "trigram");

        /// <summary>
        /// Retrieves raw counts for quadgrams (four-character sequences) from 
        /// a collection of strings.
        /// This method leverages the base `GetRawCounts_NGram_Base` logic to 
        /// calculate quadgram counts for each string in the collection.
        /// </summary>
        /// <param name="names">An enumerable collection of strings for which 
        /// the quadgram counts will be calculated.</param>
        /// <returns>A dictionary with NGramKey as the key (prefix and next 
        /// character) and the count as the value for each quadgram in the collection.</returns>
        public static Dictionary<NGramKey, int>
            GetRawCounts_Quadgram(IEnumerable<string> names)
            => GetRawCounts_NGram_Base(names, 4, "quadgram");
        #endregion

#if false
        #region Normalize Counts
        public static Dictionary<char, double>
            NormalizeCounts_FirstLetter(Dictionary<char, int> rawCounts)
        {
            return [];
        }

        public static Dictionary<NGramKey, double>
            NormalizeCounts_Bigram(Dictionary<NGramKey, int> rawCounts)
        {
            return [];
        }

        public static Dictionary<NGramKey, double>
            NormalizeCounts_Trigram(Dictionary<NGramKey, int> rawCounts)
        {
            return [];
        }

        public static Dictionary<NGramKey, double>
            NormalizeCounts_Quadgram(Dictionary<NGramKey, int> rawCounts)
        {
            return [];
        }
        #endregion  
#endif
    }
}
