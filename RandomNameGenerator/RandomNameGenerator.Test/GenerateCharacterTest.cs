using Library.Logic;
using Library.Models;

namespace RandomNameGenerator.Test
{
    public class GenerateCharacterTest
    {
        // Sample n-grams for testing
        readonly Dictionary<NGramKey, double> bigrams = new()
        {
            { new NGramKey("l", 'i'), 1.0 },
            { new NGramKey("i", 'a'), 1.0 },
            { new NGramKey("a", 'm'), 1.0 },
            { new NGramKey("s", 'o'), 1.0 },
            { new NGramKey("o", 'p'), 1.0 },
            { new NGramKey("p", 'h'), 1.0 },
            { new NGramKey("h", 'i'), 1.0 },
            { new NGramKey("n", 'o'), 1.0 },
            { new NGramKey("o", 'a'), 1.0 },
            { new NGramKey("a", 'h'), 1.0 }
        };

        readonly Dictionary<NGramKey, double> trigrams = new()
        {
            { new NGramKey("li", 'a'), 1.0 },
            { new NGramKey("ia", 'm'), 1.0 },
            { new NGramKey("so", 'p'), 1.0 },
            { new NGramKey("op", 'h'), 1.0 },
            { new NGramKey("ph", 'i'), 1.0 },
            { new NGramKey("hi", 'a'), 1.0 },
            { new NGramKey("no", 'a'), 1.0 },
            { new NGramKey("oa", 'h'), 1.0 }
        };

        readonly Dictionary<NGramKey, double> quadgrams = new()
        {
            { new NGramKey("lia", 'm'), 1.0 },
            { new NGramKey("sop", 'h'), 1.0 },
            { new NGramKey("oph", 'i'), 1.0 },
            { new NGramKey("phi", 'a'), 1.0 }
        };


        [Fact]
        public void GetCharacterProbabilities_ReturnsEmpty()
        {
            var prob = GenerateCharacter.GetCharacterProbabilities("mia", bigrams, trigrams, quadgrams);
            Assert.Empty(prob);
        }

        [Fact]
        public void GetCharacterProbabilities_ReturnsNonEmpty()
        {
            var prob = GenerateCharacter.GetCharacterProbabilities("lia", bigrams, trigrams, quadgrams);

            Assert.NotEmpty(prob);   // Ensure the result is not empty
            Assert.Contains(CharacterDatabase.GetCharacter('m'), prob.Keys); // Ensure 'm' is present
        }
    }
}
