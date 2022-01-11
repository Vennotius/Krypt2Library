using System.Globalization;

namespace Krypt2Library
{
    public static class GustoAlphabetManager
    {
        private const string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";

        internal static Alphabet InitializeAlphabet(CryptType cryptType, string message)
        {
            switch (cryptType)
            {
                case CryptType.Encryption:
                    return ExtendAlphabetForEncyption(message);
                case CryptType.Decryption:
                    return ExtendAlphabetForDecyption(message);
                default:
                    throw new Exception("Invalid CryptType.");
            }
        }

        private static Alphabet ExtendAlphabetForEncyption(string message)
        {
            var alphabet = new Alphabet(_standardAlphabet, CryptType.Encryption);

            ExtractAdditionalCharactersFromMessage(alphabet, message);

            AppendAddedToAlphabet(alphabet);

            return alphabet;
        }
        private static void ExtractAdditionalCharactersFromMessage(Alphabet alphabet, string message)
        {
            var alphabetAsHashSet = alphabet.AllCharacters.ToHashSet();
            var messageAsList = StringToListOfObjects(message);

            foreach (var textElement in messageAsList)
            {
                if (alphabet.AddedCharacters.Contains(textElement) == false)
                {
                    if (alphabetAsHashSet.Contains(textElement) == false)
                    {
                        alphabet.AddedCharacters.Add(textElement);
                    }
                }
            }
        }
        private static void AppendAddedToAlphabet(Alphabet alphabet)
        {
            alphabet.AddedCharacters.Sort();  // For security reasons.
            foreach (var item in alphabet.AddedCharacters)
            {
                alphabet.AllCharacters.Add(item);
            }
        }

        private static Alphabet ExtendAlphabetForDecyption(string message)
        {
            var alphabet = new Alphabet(_standardAlphabet, CryptType.Decryption);
            var messageAsList = StringToListOfObjects(message);

            ExtractAdditionalCharactersFromCipherText(alphabet, messageAsList);

            CheckForInvalidCipherText(alphabet, messageAsList);

            return alphabet;
        }
        private static void ExtractAdditionalCharactersFromCipherText(Alphabet alphabet, List<object> messageAsList)
        {
            foreach (var textElement in messageAsList)
            {
                // Added characters appear in the very beginning of the cipherText.
                // Therefore if we hit a known character, we are done extracting addtional characters.
                if (alphabet.AllCharacters.Contains(textElement) == true) break;

                alphabet.AddedCharacters.Add(textElement);
                alphabet.AllCharacters.Add(textElement);
            }
        }
        private static void CheckForInvalidCipherText(Alphabet alphabet, List<object> messageAsList)
        {
            // If, after the initial added characters in the CipherText, unknown characters are found, the CipherText is invalid.
            for (int i = alphabet.AddedCharacters.Count; i < messageAsList.Count; i++)
            {
                if (alphabet.AllCharacters.Contains(messageAsList[i]) == false && alphabet.AddedCharacters.Contains(messageAsList[i]) == false)
                {
                    throw new InvalidCipherException("Invalid CipherText");
                }
            }
        }


        internal static List<object> StringToListOfObjects(string input)
        {
            var output = new List<object>();

            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(input);

            while (enumerator.MoveNext())
            {
                output.Add(enumerator.Current);
            }

            return output;
        }
    }
}