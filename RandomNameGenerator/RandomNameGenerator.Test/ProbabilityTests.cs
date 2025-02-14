using Library.Logic;
using Library.Models;

namespace RandomNameGenerator.Test
{
    public class ProbabilityTests
    {
        readonly List<string> emptyInput = [];
        readonly List<string> singleInput = ["abababa"];
        readonly List<string> multipleInputs = ["abababa", "cdcdcdc", "efefefe"];

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
            var result = Probability.GetRawCounts_FirstLetter(singleInput);
            Assert.Equal(1, result['A']);
        }

        [Fact]
        public void GetRawCount_FirstLetter_MultipleInputs()
        {
            var result = Probability.GetRawCounts_FirstLetter(multipleInputs);
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
            var result = Probability.GetRawCounts_Bigram(singleInput);

            Assert.Equal(3, result[new NGramKey("a", 'b')]);
            Assert.Equal(3, result[new NGramKey("b", 'a')]);
        }

        [Fact]
        public void GetRawCounts_Bigram_MultipleInputs()
        {
            var result = Probability.GetRawCounts_Bigram(multipleInputs);

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
            var result = Probability.GetRawCounts_Trigram(singleInput);

            Assert.Equal(3, result[new NGramKey("ab", 'a')]);
            Assert.Equal(2, result[new NGramKey("ba", 'b')]);
        }

        [Fact]
        public void GetRawCounts_Trigram_MultipleInputs()
        {
            var result = Probability.GetRawCounts_Trigram(multipleInputs);

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
            var result = Probability.GetRawCounts_Quadgram(singleInput);

            Assert.Equal(2, result[new NGramKey("aba", 'b')]);
            Assert.Equal(2, result[new NGramKey("bab", 'a')]);
        }

        [Fact]
        public void GetRawCounts_Quadgram_MultipleInputs()
        {
            var result = Probability.GetRawCounts_Quadgram(multipleInputs);

            Assert.Equal(2, result[new NGramKey("aba", 'b')]);
            Assert.Equal(2, result[new NGramKey("bab", 'a')]);
            Assert.Equal(2, result[new NGramKey("cdc", 'd')]);
            Assert.Equal(2, result[new NGramKey("dcd", 'c')]);
            Assert.Equal(2, result[new NGramKey("efe", 'f')]);
            Assert.Equal(2, result[new NGramKey("fef", 'e')]);
        }
        #endregion
    }
}