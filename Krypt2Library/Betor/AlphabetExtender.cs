using System.Text;

namespace Krypt2Library
{
    public static class AlphabetExtender
    {
        /// <summary>
        /// If the message contains characters that are not in the standard alphabet, they should be added.
        /// </summary>
        /// <param name="alphabet"></param>
        /// <param name="message"></param>
        /// <param name="cryptType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static (string extendedAlphabet, string addedCharacters, int messageStartIndex)
            ExtendAlphabetIfNeeded(string alphabet, string message, CryptType cryptType)
        {
            switch (cryptType)
            {
                case CryptType.Encryption:
                    return GenerateExtendedAlphabetForEncryption(AlphabetAsDictionary(alphabet), message);
                case CryptType.Decryption:
                    return GenerateExtendedAlphabetForDecryption(AlphabetAsDictionary(alphabet), message);
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
            GenerateExtendedAlphabetForEncryption(Dictionary<char, int> alphabet, string message)
        {
            var extendedAlphabet = PopulateExtendedWithStandardAlphabet(alphabet);

            var addedCharacters = AddMissingCharactersForEncryption(alphabet, message, extendedAlphabet);

            var index = 0; // Because it only applies to decryption where our message starts directly after the declaration of additional characters;

            return (extendedAlphabet.ToString(), addedCharacters.ToString(), index);
        }
        private static StringBuilder AddMissingCharactersForEncryption(Dictionary<char, int> alphabet, string message, StringBuilder extendedAlphabet)
        {
            var addedCharacters = new StringBuilder();
            List<char> added = ExtractCharactersThatAreNotInStandardAlphabet(alphabet, message);

            foreach (char c in added)
            {
                extendedAlphabet.Append(c);
                addedCharacters.Append(c);
            }

            return addedCharacters;
        }
        private static List<char> ExtractCharactersThatAreNotInStandardAlphabet(Dictionary<char, int> alphabet, string message)
        {
            var added = new List<char>();

            foreach (char c in message)
            {
                if (alphabet.ContainsKey(c) == false && added.Contains(c) == false)
                {
                    added.Add(c);
                }
            }

            // For security reasons, in order that nothing might be inferred from
            // the order in which added characters appear at the beginning of the cipherText.
            added.Sort();

            return added;
        }


        private static (string outputExtendedAlphabet, string addedCharacters, int messageStartIndex)
            GenerateExtendedAlphabetForDecryption(Dictionary<char, int> alphabet, string message)
        {
            var extendedAlphabet = PopulateExtendedWithStandardAlphabet(alphabet);

            var addedCharacters = new StringBuilder();

            int index = AddMissingCharactersForDecryptionAndReturnStartingIndex(alphabet, message, addedCharacters, extendedAlphabet);

            return (extendedAlphabet.ToString(), addedCharacters.ToString(), index);
        }
        private static int AddMissingCharactersForDecryptionAndReturnStartingIndex(Dictionary<char, int> alphabet, string message, StringBuilder addedCharacters, StringBuilder extendedAlphabet)
        {
            var _alreadyAdded = new Dictionary<char, bool>();

            int output = 0;
            foreach (char c in message)
            {
                // This works because additional characters in cipherText are prepended to the message.
                // Therefore, once we encounter a known character, we know that out job here is done.
                if (extendedAlphabet.ToString().Contains(c) == true) break;

                if (alphabet.ContainsKey(c) == false && _alreadyAdded.ContainsKey(c) == false)
                {
                    _alreadyAdded.Add(c, true);
                    extendedAlphabet.Append(c);
                    addedCharacters.Append(c);
                    output++;
                }
            }

            return output;
        }


        private static StringBuilder PopulateExtendedWithStandardAlphabet(Dictionary<char, int> alphabet)
        {
            var outputExtendedAlphabet = new StringBuilder();

            foreach (var item in alphabet.Keys)
            {
                outputExtendedAlphabet.Append(item);
            }

            return outputExtendedAlphabet;
        }

    }
}