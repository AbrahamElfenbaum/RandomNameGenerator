namespace Library.Models
{
    /// <summary>
    /// Represents a key for an n-gram, consisting of a prefix and the next character.
    /// This is used to track frequency distributions of character sequences.
    /// </summary>
    /// <param name="prefix">The sequence of characters that precede the next character.</param>
    /// <param name="nextChar">The character that follows the prefix in the n-gram.</param>
    public readonly struct NGramKey(string prefix, char nextChar)
    {
        /// <summary>
        /// Gets the prefix of the n-gram, which represents the sequence of characters
        /// leading up to the next character.
        /// </summary>
        public string Prefix { get; } = prefix;

        /// <summary>
        /// Gets the next character in the n-gram sequence, which follows the prefix.
        /// </summary>
        public char NextChar { get; } = nextChar;

        /// <summary>
        /// Determines whether two <see cref="NGramKey"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="NGramKey"/> to compare.</param>
        /// <param name="right">The second <see cref="NGramKey"/> to compare.</param>
        /// <returns><c>true</c> if both instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(NGramKey left, NGramKey right) => left.Equals(right);

        /// <summary>
        /// Determines whether two <see cref="NGramKey"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="NGramKey"/> to compare.</param>
        /// <param name="right">The second <see cref="NGramKey"/> to compare.</param>
        /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(NGramKey left, NGramKey right) => !left.Equals(right);

        /// <summary>
        /// Determines whether the specified object is equal to the current 
        /// <see cref="NGramKey"/> instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the object is an <see cref="NGramKey"/> with 
        /// the same prefix and next character; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj) =>
        obj is NGramKey other && Prefix == other.Prefix && NextChar == other.NextChar;

        /// <summary>
        /// Gets the hash code for the current <see cref="NGramKey"/> instance.
        /// </summary>
        /// <returns>A hash code that represents the current instance.</returns>
        public override int GetHashCode() => HashCode.Combine(Prefix, NextChar);

        /// <summary>
        /// Returns a string representation of the <see cref="NGramKey"/> instance.
        /// </summary>
        /// <returns>A string formatted as "Prefix -> NextChar".</returns>
        public override string ToString() => $"{Prefix} -> {NextChar}";
    }
}
