using System.Globalization;

namespace Krypt2Library
{
    public class GustoAlphabetManager
    {
        private static readonly string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";

        internal static Alphabet InitializeAlphabet(CryptType cryptType, string message)
        {
            Alphabet output;
            List<object> addedAsList;
            List<object> alphabetAsList;

            switch (cryptType)
            {
                case CryptType.Encryption:
                    return ExtendAlphabetForEncyption(message);
                case CryptType.Decryption:
                    ExtendAlphabetForDecyption(message, out addedAsList, out alphabetAsList);
                    output = new Alphabet(alphabetAsList, addedAsList, cryptType);
                    return output;
                default:
                    throw new Exception("Invalid CryptType.");
            }
        }
        private static Alphabet ExtendAlphabetForEncyption(string message)
        {
            var alphabet = new Alphabet(CryptType.Encryption);
            alphabet.AllCharacters = StringToListOfObjects(_standardAlphabet);
            var messageAsList = StringToListOfObjects(message);

            ExtractAdditionalCharactersFromMessage(alphabet, messageAsList);

            AppendAddedToAlphabet(alphabet);

            return alphabet;
        }
        private static void ExtractAdditionalCharactersFromMessage(Alphabet alphabet, List<object> messageAsList)
        {
            foreach (var textElement in messageAsList)
            {
                if (alphabet.AddedCharacters.Contains(textElement) == false)
                {
                    if (alphabet.AllCharacters.Contains(textElement) == false)
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


        private static void ExtendAlphabetForDecyption(string message, out List<object> added, out List<object> alphabetAsList)
        {
            InitializeForVariablesForExtendAlphabet(message, out added, out alphabetAsList, out List<object> messageAsList);
            
            ExtractAdditionalCharactersFromCipherText(added, alphabetAsList, messageAsList);

            CheckForInvalidCipherText(messageAsList, alphabetAsList, added);
        }
        private static void ExtractAdditionalCharactersFromCipherText(List<object> added, List<object> alphabetAsList, List<object> messageAsList)
        {
            foreach (var textElement in messageAsList)
            {
                if (alphabetAsList.Contains(textElement) == true) break;

                added.Add(textElement);
                alphabetAsList.Add(textElement);
            }
        }
        private static void CheckForInvalidCipherText(List<object> messageAsList, List<object> alphabetAsList, List<object> added)
        {
            // If, after the initial added characters in the CipherText, unknown characters are found, the CipherText is invalid.
            for (int i = added.Count; i < messageAsList.Count; i++)
            {
                if (alphabetAsList.Contains(messageAsList[i]) == false && added.Contains(messageAsList[i]) == false)
                {
                    throw new InvalidCipherException("Invalid CipherText");
                }
            }
        }


        private static void InitializeForVariablesForExtendAlphabet(string message, out List<object> added, out List<object> alphabetAsList, out List<object> messageAsList)
        {
            added = new List<object>();
            alphabetAsList = StringToListOfObjects(_standardAlphabet);
            messageAsList = StringToListOfObjects(message);
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