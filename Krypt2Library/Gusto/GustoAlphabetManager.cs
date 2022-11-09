using System.Globalization;

namespace Krypt2Library
{
    internal static class GustoAlphabetManager
    {
        private const string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";

        internal static Alphabet InitializeAlphabet(CryptType cryptType, string message)
        {
            return cryptType switch
            {
                CryptType.Encryption => ExtendAlphabetForEncyption(message),
                CryptType.Decryption => ExtendAlphabetForDecyption(message),
                _ => throw new Exception("Invalid CryptType."),
            };
        }

        private static Alphabet ExtendAlphabetForEncyption(string message)
        {
            Alphabet alphabet = new(_standardAlphabet, CryptType.Encryption);

            ExtractAdditionalCharactersFromMessage(alphabet, message);

            AppendAddedToAlphabet(alphabet);

            return alphabet;
        }
        private static void ExtractAdditionalCharactersFromMessage(Alphabet alphabet, string message)
        {
            HashSet<string> alphabetAsHashSet = alphabet.AllCharacters.ToHashSet();
            List<string> messageAsList = StringToListOfObjects(message);

            foreach (string textElement in messageAsList)
            {
                if (alphabet.AddedCharacters.Contains(textElement) ||
                    alphabetAsHashSet.Contains(textElement)) continue;

                alphabet.AddedCharacters.Add(textElement);
            }
        }
        private static void AppendAddedToAlphabet(Alphabet alphabet)
        {
            alphabet.AddedCharacters.Sort();  // For security reasons.

            alphabet.AllCharacters.AddRange(alphabet.AddedCharacters);
        }

        private static Alphabet ExtendAlphabetForDecyption(string message)
        {
            Alphabet alphabet = new(_standardAlphabet, CryptType.Decryption);
            List<string> messageAsList = StringToListOfObjects(message);

            ExtractAdditionalCharactersFromCipherText(alphabet, messageAsList);

            CheckForInvalidCipherText(alphabet, messageAsList);

            return alphabet;
        }
        private static void ExtractAdditionalCharactersFromCipherText(Alphabet alphabet, List<string> messageAsList)
        {
            foreach (string textElement in messageAsList)
            {
                // Added characters appear in the very beginning of the cipherText.
                // Therefore if we hit a known character, we are done extracting addtional characters.
                if (alphabet.AllCharacters.Contains(textElement)) break;

                alphabet.AddedCharacters.Add(textElement);
                alphabet.AllCharacters.Add(textElement);
            }
        }
        private static void CheckForInvalidCipherText(Alphabet alphabet, List<string> messageAsList)
        {
            // If, after the initial added characters in the CipherText, unknown characters are found, the CipherText is invalid.
            for (int i = alphabet.AddedCharacters.Count; i < messageAsList.Count; i++)
            {
                if (alphabet.AllCharacters.Contains(messageAsList[i]) ||
                    alphabet.AddedCharacters.Contains(messageAsList[i])) continue;

                throw new InvalidCipherException("Invalid CipherText");
            }
        }


        internal static List<string> StringToListOfObjects(string input)
        {
            List<string> output = new();

            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(input);

            while (enumerator.MoveNext())
            {
                output.Add(enumerator.GetTextElement());
            }

            return output;
        }
    }
}