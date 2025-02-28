
using Library.Logic;
using Library.Models;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Dictionary<NGramKey, double> bigrams = new()
        {
            { new NGramKey("l", 'i'), 1.0 },
            { new NGramKey("i", 'a'), 1.0 },
            { new NGramKey("a", 'm'), 1.0 }
        };

Dictionary<NGramKey, double> trigrams = new()
        {
            { new NGramKey("li", 'a'), 1.0 },  // Common next character 'a'
            { new NGramKey("ia", 'm'), 1.0 }   // Common next character 'm'
        };

Dictionary<NGramKey, double> quadgrams = new()
        {
            { new NGramKey("lia", 'm'), 1.0 }  // Common next character 'm'
        };

//liam

var prob = GenerateCharacter.GetCharacterProbabilities("lia", bigrams, trigrams, quadgrams);
