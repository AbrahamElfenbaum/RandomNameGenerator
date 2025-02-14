using System.Security.Cryptography;

namespace Library.Utilities
{
    class Util
    {
        /// <summary>
        /// Generates a cryptographically secure random integer between 0 (inclusive) 
        /// and a specified maximum value (exclusive).
        /// </summary>
        /// <param name="maxExclusive">
        /// The exclusive upper bound for the random number. Must be greater than 0.
        /// </param>
        /// <returns>
        /// A random integer greater than or equal to 0 and less than <paramref name="maxExclusive"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="maxExclusive"/> is less than or equal to 0.
        /// </exception>
        public static int RandomInt(int maxExclusive)
        {
            if (maxExclusive <= 0)
                throw new ArgumentException("Max Exclusive must be greater than 0");

            byte[] numberByte = new byte[4];

            //Generates an array of 4 random bytes
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(numberByte);

            //Convert the byte array to a non-negative int
            int numberInt = BitConverter.ToInt32(numberByte, 0) & maxExclusive;

            // Returns an integer between 0 (inclusive) and maxExclusive (exclusive)
            return numberInt % maxExclusive;
        }

        /// <summary>
        /// /// Generates a cryptographically secure random double between 0 and 1(inclusive) 
        /// </summary>
        /// <returns>A random double greater than or equal to 0 and  less than or equal to 1</returns>
        public static double RandomDouble()
        {
            byte[] numberByte = new byte[8];

            //Generates an array of 8 random bytes
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(numberByte);

            //Convert the byte array to a non-negative long
            ulong numberLong = BitConverter.ToUInt64(numberByte, 0);

            //Scale the number to a value between 0 and 1 (inclusive)
            return numberLong / ulong.MaxValue;
        }

        /// <summary>
        /// Checks if the given character is a valid letter or special character.
        /// </summary>
        /// <param name="c">The character being checked</param>
        /// <returns>If the input character is a valid character</returns>
        public static bool IsValidCharacter(char c)
        {
            return char.IsLetter(c) || c == '\'' || c == '.' || c == '-' || c == ' ';
        }
    }
}
