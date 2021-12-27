using System.Collections.Generic;

namespace Krypt2Library
{
    public class AlphabetFactory
    {
        private Random _random;
        private readonly string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";
        public string alphabet;
        public string added;
        private readonly List<char> _alphabetList;
        private readonly string _passphrase;

        public int MessageStartIndex { get; private set; }

        public AlphabetFactory(string passphrase, string message, CryptType cryptType)
        {
            _passphrase = passphrase;
            _random = new Random(GetSoortvanHash(_passphrase));

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

        public void Reset()
        {
            _random = new Random(GetSoortvanHash(_passphrase));
        }

        private static int GetSoortvanHash(string passphrase)
        {
            int output = 1;

            for (int i = 0; i < passphrase.Length; i++)
            {
                if (passphrase[i] != 0) output += passphrase[i];
                if (output % 3 == 0) output *= passphrase[i];
                if (output % 5 == 0) output /= 3;
                if (i % 2 == 0) output += 3;
                if (i % 7 == 0) output += 4;
            }

            return output;
        }

        public List<char> GetAlphabetForNextCharacter()
        {
            var output = new List<char>(_alphabetList);

            ShuffleAlphabet<char>(output);

            return output;
        }

        public void ShuffleAlphabet<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = _random.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }

        public void Shuffle<T>(T[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int n = array.GetLength(0);
                while (n > 1)
                {
                    n--;
                    int k = _random.Next(n + 1);
                    T value = array[i, k];
                    array[i, k] = array[i, n];
                    array[i, n] = value;
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