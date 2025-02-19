using Library.Logic;
using Library.Models;

namespace RandomNameGenerator.Test
{
    public class ProbabilityTests
    {
        readonly List<string> emptyInput = [];

        #region Raw Counts
        readonly List<string> singleInputRawCount = ["abababa"];
        readonly List<string> multipleInputsRawCount = ["abababa", "cdcdcdc", "efefefe"];

        #region First Letter
        [Fact]
        public void GetRawCount_FirstLetter_EmptyInput()
        {
            var result = Probability.GetRawCounts_FirstLetter(emptyInput);
            Assert.Empty(result);
        }

        [Fact]
        public void GetRawCount_FirstLetter_SingleInput()
        {
            var result = Probability.GetRawCounts_FirstLetter(singleInputRawCount);
            Assert.Equal(1, result['A']);
        }

        [Fact]
        public void GetRawCount_FirstLetter_MultipleInputs()
        {
            var result = Probability.GetRawCounts_FirstLetter(multipleInputsRawCount);
            Assert.Equal(1, result['A']);
            Assert.Equal(1, result['C']);
            Assert.Equal(1, result['E']);
        }
        #endregion

        #region Bigram
        [Fact]
        public void GetRawCounts_Bigram_EmptyInput()
        {
            var result = Probability.GetRawCounts_Bigram(emptyInput);
            Assert.Empty(result);
        }

        [Fact]
        public void GetRawCounts_Bigram_SingleInput()
        {
            var result = Probability.GetRawCounts_Bigram(singleInputRawCount);

            Assert.Equal(3, result[new NGramKey("a", 'b')]);
            Assert.Equal(3, result[new NGramKey("b", 'a')]);
        }

        [Fact]
        public void GetRawCounts_Bigram_MultipleInputs()
        {
            var result = Probability.GetRawCounts_Bigram(multipleInputsRawCount);

            Assert.Equal(3, result[new NGramKey("a", 'b')]);
            Assert.Equal(3, result[new NGramKey("b", 'a')]);
            Assert.Equal(3, result[new NGramKey("c", 'd')]);
            Assert.Equal(3, result[new NGramKey("d", 'c')]);
            Assert.Equal(3, result[new NGramKey("e", 'f')]);
            Assert.Equal(3, result[new NGramKey("f", 'e')]);
        }
        #endregion

        #region Trigram
        [Fact]
        public void GetRawCounts_Trigram_EmptyInput()
        {
            var result = Probability.GetRawCounts_Trigram(emptyInput);
            Assert.Empty(result);
        }

        [Fact]
        public void GetRawCounts_Trigram_SingleInput()
        {
            var result = Probability.GetRawCounts_Trigram(singleInputRawCount);

            Assert.Equal(3, result[new NGramKey("ab", 'a')]);
            Assert.Equal(2, result[new NGramKey("ba", 'b')]);
        }

        [Fact]
        public void GetRawCounts_Trigram_MultipleInputs()
        {
            var result = Probability.GetRawCounts_Trigram(multipleInputsRawCount);

            Assert.Equal(3, result[new NGramKey("ab", 'a')]);
            Assert.Equal(2, result[new NGramKey("ba", 'b')]);
            Assert.Equal(3, result[new NGramKey("cd", 'c')]);
            Assert.Equal(2, result[new NGramKey("dc", 'd')]);
            Assert.Equal(3, result[new NGramKey("ef", 'e')]);
            Assert.Equal(2, result[new NGramKey("fe", 'f')]);
        }
        #endregion

        #region Quadgram
        [Fact]
        public void GetRawCounts_Quadgram_EmptyInput()
        {
            var result = Probability.GetRawCounts_Quadgram(emptyInput);
            Assert.Empty(result);
        }

        [Fact]
        public void GetRawCounts_Quadgram_SingleInput()
        {
            var result = Probability.GetRawCounts_Quadgram(singleInputRawCount);

            Assert.Equal(2, result[new NGramKey("aba", 'b')]);
            Assert.Equal(2, result[new NGramKey("bab", 'a')]);
        }

        [Fact]
        public void GetRawCounts_Quadgram_MultipleInputs()
        {
            var result = Probability.GetRawCounts_Quadgram(multipleInputsRawCount);

            Assert.Equal(2, result[new NGramKey("aba", 'b')]);
            Assert.Equal(2, result[new NGramKey("bab", 'a')]);
            Assert.Equal(2, result[new NGramKey("cdc", 'd')]);
            Assert.Equal(2, result[new NGramKey("dcd", 'c')]);
            Assert.Equal(2, result[new NGramKey("efe", 'f')]);
            Assert.Equal(2, result[new NGramKey("fef", 'e')]);
        }
        #endregion
        #endregion

        #region Normalize
        readonly List<string> singleInputNormalize = ["Adam"];
        readonly List<string> multipleInputsNormalize = ["Adam", "Abe", "Abby"];

        #region First Letter
        [Fact]
        public void NormalizeCounts_FirstLetter_EmptyInput()
        {
            var rawCounts = Probability.GetRawCounts_FirstLetter(emptyInput);
            var result = Probability.NormalizeCounts_FirstLetter(rawCounts);
            Assert.Empty(result);
        }

        [Fact]
        public void NormalizeCounts_FirstLetter_SingleInput()
        {
            var rawCounts = Probability.GetRawCounts_FirstLetter(singleInputNormalize);
            var result = Probability.NormalizeCounts_FirstLetter(rawCounts);
            Assert.Equal(1, result['A']);
        }

        [Fact]
        public void NormalizeCounts_FirstLetter_MultipleInputs()
        {
            var rawCounts = Probability.GetRawCounts_FirstLetter(multipleInputsNormalize);
            var result = Probability.NormalizeCounts_FirstLetter(rawCounts);
            Assert.Equal(1, result['A']);
        }
        #endregion

        #region Bigram
        [Fact]
        public void NormalizeCounts_Bigram_EmptyInput()
        {
            var rawCounts = Probability.GetRawCounts_Bigram(emptyInput);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Empty(result);
        }

        [Fact]
        public void NormalizeCounts_Bigram_SingleInputs()
        {
            var rawCounts = Probability.GetRawCounts_Bigram(singleInputNormalize);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Equal(0.5, result[new NGramKey("a", 'd')], 2);
            Assert.Equal(1, result[new NGramKey("d", 'a')]);
            Assert.Equal(0.5, result[new NGramKey("a", 'm')], 2);
        }

        [Fact]
        public void NormalizeCounts_Bigram_MultipleInputs()
        {
            var rawCounts = Probability.GetRawCounts_Bigram(multipleInputsNormalize);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Equal(0.25, result[new NGramKey("a", 'd')], 2);
            Assert.Equal(1, result[new NGramKey("d", 'a')]);
            Assert.Equal(0.25, result[new NGramKey("a", 'm')], 2);
            Assert.Equal(0.5, result[new NGramKey("a", 'b')], 2);
            Assert.Equal(0.33, result[new NGramKey("b", 'e')], 2);
            Assert.Equal(0.33, result[new NGramKey("b", 'b')], 2);
            Assert.Equal(0.33, result[new NGramKey("b", 'y')], 2);
        }
        #endregion

        #region Trigram
        [Fact]
        public void NormalizeCounts_Trigram_EmptyInput()
        {
            var rawCounts = Probability.GetRawCounts_Trigram(emptyInput);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Empty(result);
        }

        [Fact]
        public void NormalizeCounts_Trigram_SingleInputs()
        {
            var rawCounts = Probability.GetRawCounts_Trigram(singleInputNormalize);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Equal(1, result[new NGramKey("ad", 'a')]);
            Assert.Equal(1, result[new NGramKey("da", 'm')]);
        }

        [Fact]
        public void NormalizeCounts_Trigram_MultipleInputs()
        {
            var rawCounts = Probability.GetRawCounts_Trigram(multipleInputsNormalize);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Equal(1, result[new NGramKey("ad", 'a')]);
            Assert.Equal(1, result[new NGramKey("da", 'm')]);
            Assert.Equal(0.5, result[new NGramKey("ab", 'e')], 2);
            Assert.Equal(0.5, result[new NGramKey("ab", 'b')], 2);
            Assert.Equal(1, result[new NGramKey("bb", 'y')]);
        }
        #endregion

        #region Quadgram
        [Fact]
        public void NormalizeCounts_Quadgram_EmptyInput()
        {
            var rawCounts = Probability.GetRawCounts_Quadgram(emptyInput);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Empty(result);
        }

        [Fact]
        public void NormalizeCounts_Quadgram_SingleInputs()
        {
            var rawCounts = Probability.GetRawCounts_Quadgram(singleInputNormalize);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Equal(1, result[new NGramKey("ada", 'm')]);
        }

        [Fact]
        public void NormalizeCounts_Quadgram_MultipleInputs()
        {
            var rawCounts = Probability.GetRawCounts_Quadgram(multipleInputsNormalize);
            var result = Probability.NormalizeCounts_NGram(rawCounts);
            Assert.Equal(1, result[new NGramKey("ada", 'm')]);
            Assert.Equal(1, result[new NGramKey("abb", 'y')]);
        }
        #endregion   
        #endregion
    }
}