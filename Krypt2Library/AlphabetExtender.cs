using System.Text;

namespace Krypt2Library
{
    public static class AlphabetExtender
    {
        public static (string outputExtendedAlphabet, string addedCharacters, int messageStartIndex) ExtendAlphabetIfNeeded(string alphabet, string message, CryptType cryptType)
        {
            switch (cryptType)
            {
                case CryptType.Encryption:
                    return GenerateUpdatedAlphabetForEncryption(AlphabetAsDictionary(alphabet), message);
                case CryptType.Decryption:
                    return GenerateUpdatedAlphabetForDecryption(AlphabetAsDictionary(alphabet), message);
                default:
                    throw new ArgumentException("CryptType not valid");
            }
        }

        private static Dictionary<char, int> AlphabetAsDictionary(string alphabet)
        {
            var output = new Dictionary<char, int>();
            
            foreach (char c in alphabet)
            {
                output.Add(c, 0);
            }

            return output;
        }

        private static (string outputExtendedAlphabet, string addedCharacters, int messageStartIndex) 
            GenerateUpdatedAlphabetForEncryption(Dictionary<char, int> alphabet, string message)
        {
            var outputExtendedAlphabet = new StringBuilder();
            var addedCharacters = new StringBuilder();

            foreach (var item in alphabet.Keys)
            {
                outputExtendedAlphabet.Append(item);
            }

            var added = new List<char>();

            foreach (char c in message)
            {
                if (alphabet.ContainsKey(c) == false && added.Contains(c) == false)
                {
                    outputExtendedAlphabet.Append(c);
                    added.Add(c);
                }
            }
            // Known vulnerablity: Added is not shuffled.
            foreach (var c in added)
            {
                addedCharacters.Append(c);
            }

            return (outputExtendedAlphabet.ToString(), addedCharacters.ToString(), 0);
        }

        private static (string outputExtendedAlphabet, string addedCharacters, int messageStartIndex) 
            GenerateUpdatedAlphabetForDecryption(Dictionary<char, int> alphabet, string message)
        {
            var outputExtendedAlphabet = new StringBuilder();
            var addedCharacters = new StringBuilder();

            foreach (var item in alphabet.Keys)
            {
                outputExtendedAlphabet.Append(item);
            }

            var added = new Dictionary<char, int>();

            int index = 0;
            foreach (char c in message)
            {
                if (outputExtendedAlphabet.ToString().Contains(c) == true) break;
                if (alphabet.ContainsKey(c) == false && added.ContainsKey(c) == false)
                {
                    outputExtendedAlphabet.Append(c);
                    added.Add(c, 1);
                    addedCharacters.Append(c);
                }

                index++;
            }

            return (outputExtendedAlphabet.ToString(), addedCharacters.ToString(), index);
        }
    }
}