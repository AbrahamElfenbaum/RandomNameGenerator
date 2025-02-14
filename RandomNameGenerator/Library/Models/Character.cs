namespace Library.Models
{
    /// <summary>
    /// Represents a character with its lower and upper case forms and categorization 
    /// as a vowel, consonant or special character.
    /// </summary>
    /// <param name="Lower">The lowercase representation of the character.</param>
    /// <param name="Upper">The uppercase representation of the character.</param>
    /// <param name="isVowel">Indicates whether the character is a vowel.</param>
    /// <param name="isConsonant">Indicates whether the character is a consonant.</param>
    /// <param name="isSpecialCharacter">Indicates whether the character is a special character.</param>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Character"/> struct with the specified values.
    /// </remarks>
    /// <param name="Lower">The lowercase character.</param>
    /// <param name="Upper">The uppercase character.</param>
    /// <param name="isVowel">True if the character is a vowel; otherwise, false.</param>
    /// <param name="isConsonant">True if the character is a consonant; otherwise, false.</param>
    /// <param name="isSpecialCharacter">True if the character is a special character; otherwise, false.</param>
    public struct Character(char Lower, char Upper, bool isVowel, bool isConsonant, bool isSpecialCharacter)
    {
        /// <summary>
        /// The lowercase form of the character.
        /// </summary>
        public char Lower = Lower;

        /// <summary>
        /// The uppercase form of the character.
        /// </summary>
        public char Upper = Upper;

        /// <summary>
        /// Indicates whether the character is classified as a vowel.
        /// </summary>
        public bool isVowel = isVowel;

        /// <summary>
        /// Indicates whether the character is classified as a consonant.
        /// </summary>
        public bool isConsonant = isConsonant;

        /// <summary>
        /// Indicates whether the character is classified as a special character.
        /// </summary>
        public bool isSpecialCharacter = isSpecialCharacter;
    }
}
