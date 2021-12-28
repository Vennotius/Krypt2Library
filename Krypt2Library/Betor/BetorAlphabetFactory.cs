using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("Krypt2LibraryTests")]
namespace Krypt2Library
{
    public class BetorAlphabetFactory
    {
        private List<Random> _randoms;
        private readonly string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";
        public string alphabet;
        public string added;
        private readonly List<char> _alphabetList;
        private readonly string _passphrase;

        public int MessageStartIndex { get; private set; }

        public BetorAlphabetFactory(string passphrase, string message, CryptType cryptType)
        {
            _passphrase = passphrase;
            _randoms = GetRandomsForPassphrase(_passphrase);

            switch (cryptType)
            {
                case CryptType.Encryption:
                    (alphabet, added, MessageStartIndex) = AlphabetExtender.ExtendAlphabetIfNeeded(_standardAlphabet, message, cryptType);
                    break;
                case CryptType.Decryption:
                    (alphabet, added, MessageStartIndex) = AlphabetExtender.ExtendAlphabetIfNeeded(_standardAlphabet, message, cryptType);
                    break;
            }
            _alphabetList = alphabet.ToList();
        }

        internal static List<Random> GetRandomsForPassphrase(string passphrase)
        {
            var output = new List<Random>();

            byte[] hashArray = GetHashByteArray(passphrase);
            
            List<int> randomSeeds = GetRandomSeedsFromByteArray(hashArray);

            foreach (var seed in randomSeeds)
            {
                output.Add(new Random(seed));
            }

            return output;
        }

        internal static List<int> GetRandomSeedsFromByteArray(byte[] hashArray)
        {
            var output = new List<int>();

            for (int i = 0; i < 32; i += 4)
            {
                int seed = hashArray[i] + (hashArray[i+1] * 256) + (hashArray[i+2] * 256 * 256) + (hashArray[i+3] * 256 * 256 * 256);
                output.Add(seed);
            }

            return output;
        }

        private static byte[] GetHashByteArray(string passphrase)
        {
            byte[] hash;

            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            }

            return hash;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public List<char> GetAlphabetForNextCharacter(int randomIndex)
        {
            var output = new List<char>(_alphabetList);

            ShuffleAlphabet<char>(output, randomIndex);

            return output;
        }

        public void ShuffleAlphabet<T>(List<T> list, int randomIndex)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = _randoms[randomIndex].Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }

        private static int ShiftIndexWrapper(int length, int shiftBy)
        {
            if (shiftBy == 0) return 0;

            if (shiftBy > 0)
            {
                if (shiftBy < length) return shiftBy;

                while (shiftBy >= length)
                {
                    shiftBy -= length;
                }

                return shiftBy;
            }

            if (shiftBy > -length) return shiftBy + length;

            while (shiftBy <= -length)
            {
                shiftBy += length;
            }

            return shiftBy + length;
        }

        public static int[,] GenerateAlphabetMap(string characters)
        {
            var output = new int[characters.Length, characters.Length];

            for (int i = 0; i < characters.Length; i++)
            {
                for (int j = 0; j < characters.Length; j++)
                {
                    output[i, j] = j;
                }

                ShiftArrayRowRight(output, i, i);
            }

            return output;
        }

        public static void ShiftArrayRowRight(int[,] array, int row, int shiftBy)
        {
            var length = array.GetLength(1);

            var temp = new int[length];
            for (int i = 0; i < length; i++)
            {
                temp[i] = array[row, i];
            }

            for (int i = shiftBy; i < length; i++)
            {
                array[row, i] = temp[i - shiftBy];
            }

            int index = 0;
            for (int i = length - shiftBy; i < length; i++)
            {
                array[row, index] = temp[i];
                index++;
            }
        }
    }
}