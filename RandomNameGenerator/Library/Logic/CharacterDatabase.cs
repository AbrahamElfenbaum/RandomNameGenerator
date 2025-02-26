using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic
{
    public static class CharacterDatabase
    {
        // Dictionary for fast lookups by both lowercase and uppercase characters.
        private static readonly Dictionary<char, Character> CharacterDictionary = [];

        static CharacterDatabase()
        {
            // Initialize the dictionary with both lower and upper case mappings.
            CharacterDictionary['a'] = new Character('a', 'A', true, false, false);
            CharacterDictionary['b'] = new Character('b', 'B', false, true, false);
            CharacterDictionary['c'] = new Character('c', 'C', false, true, false);
            CharacterDictionary['d'] = new Character('d', 'D', false, true, false);
            CharacterDictionary['e'] = new Character('e', 'E', true, false, false);
            CharacterDictionary['f'] = new Character('f', 'F', false, true, false);
            CharacterDictionary['g'] = new Character('g', 'G', false, true, false);
            CharacterDictionary['h'] = new Character('h', 'H', false, true, false);
            CharacterDictionary['i'] = new Character('i', 'I', true, false, false);
            CharacterDictionary['j'] = new Character('j', 'J', false, true, false);
            CharacterDictionary['k'] = new Character('k', 'K', false, true, false);
            CharacterDictionary['l'] = new Character('l', 'L', false, true, false);
            CharacterDictionary['m'] = new Character('m', 'M', false, true, false);
            CharacterDictionary['n'] = new Character('n', 'N', false, true, false);
            CharacterDictionary['o'] = new Character('o', 'O', true, false, false);
            CharacterDictionary['p'] = new Character('p', 'P', false, true, false);
            CharacterDictionary['q'] = new Character('q', 'Q', false, true, false);
            CharacterDictionary['r'] = new Character('r', 'R', false, true, false);
            CharacterDictionary['s'] = new Character('s', 'S', false, true, false);
            CharacterDictionary['t'] = new Character('t', 'T', false, true, false);
            CharacterDictionary['u'] = new Character('u', 'U', true, false, false);
            CharacterDictionary['v'] = new Character('v', 'V', false, true, false);
            CharacterDictionary['w'] = new Character('w', 'W', false, true, false);
            CharacterDictionary['x'] = new Character('x', 'X', false, true, false);
            CharacterDictionary['y'] = new Character('y', 'Y', true, false, false);
            CharacterDictionary['z'] = new Character('z', 'Z', false, true, false);
            CharacterDictionary['á'] = new Character('á', 'Á', true, false, false);
            CharacterDictionary['é'] = new Character('é', 'É', true, false, false);
            CharacterDictionary['í'] = new Character('í', 'Í', true, false, false);
            CharacterDictionary['ó'] = new Character('ó', 'Ó', true, false, false);
            CharacterDictionary['ú'] = new Character('ú', 'Ú', true, false, false);
            CharacterDictionary['à'] = new Character('à', 'À', true, false, false);
            CharacterDictionary['è'] = new Character('è', 'È', true, false, false);
            CharacterDictionary['ì'] = new Character('ì', 'Ì', true, false, false);
            CharacterDictionary['ò'] = new Character('ò', 'Ò', true, false, false);
            CharacterDictionary['ù'] = new Character('ù', 'Ù', true, false, false);
            CharacterDictionary['ä'] = new Character('ä', 'Ä', true, false, false);
            CharacterDictionary['ë'] = new Character('ë', 'Ë', true, false, false);
            CharacterDictionary['ï'] = new Character('ï', 'Ï', true, false, false);
            CharacterDictionary['ö'] = new Character('ö', 'Ö', true, false, false);
            CharacterDictionary['ü'] = new Character('ü', 'Ü', true, false, false);
            CharacterDictionary['â'] = new Character('â', 'Â', true, false, false);
            CharacterDictionary['ê'] = new Character('ê', 'Ê', true, false, false);
            CharacterDictionary['î'] = new Character('î', 'Î', true, false, false);
            CharacterDictionary['ô'] = new Character('ô', 'Ô', true, false, false);
            CharacterDictionary['û'] = new Character('û', 'Û', true, false, false);
            CharacterDictionary['ç'] = new Character('ç', 'Ç', false, true, false);
            CharacterDictionary['ñ'] = new Character('ñ', 'Ñ', false, true, false);
            CharacterDictionary['ý'] = new Character('ý', 'Ý', true, false, false);
            CharacterDictionary['ß'] = new Character('ß', 'ẞ', false, true, false);
            CharacterDictionary['\''] = new Character('\'', '\'', false, false, true);
            CharacterDictionary['-'] = new Character('-', '-', false, false, true);
            CharacterDictionary['.'] = new Character('.', '.', false, false, true);
            CharacterDictionary[' '] = new Character(' ', ' ', false, false, true);
        }

        public static Character GetCharacter(char c)
            => CharacterDictionary.TryGetValue(c, out var character) ? character : default;
    }
}
