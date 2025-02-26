using Library.Logic;
using Library.Models;

namespace RandomNameGenerator.Test
{
    public class GenerateCharacterTest
    {
        // Sample n-grams for testing
        readonly Dictionary<NGramKey, double> bigrams = new()
        {
            { new NGramKey("he", 'l'), 0.5 },
            { new NGramKey("he", 'o'), 0.3 },
            { new NGramKey("he", 'y'), 0.2 }
        };

        readonly Dictionary<NGramKey, double> trigrams = new()
        {
            { new NGramKey("hel", 'l'), 0.4 },
            { new NGramKey("hel", 'o'), 0.6 }
        };

        readonly Dictionary<NGramKey, double> quadgrams = new()
        {
            { new NGramKey("hell", 'o'), 0.7 },
            { new NGramKey("hell", 'y'), 0.3 }
        };

        [Fact]
        public void GetCharacter()
        {
            var result = CharacterDatabase.GetCharacter('h');
            Assert.Equal(result, new Character('h', 'H', false, true, false));
        }

        [Fact]
        public void GetCharacterProbabilities()
        {
            var result = GenerateCharacter.GetCharacterProbabilities("he");
            Assert.Equal(3, result.Count);
        }
    }
}
