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
                    ExtendAlphabetForEncyption(message, out addedAsList, out alphabetAsList);
                    output = new Alphabet(alphabetAsList, addedAsList, cryptType);
                    return output;
                case CryptType.Decryption:
                    ExtendAlphabetForDecyption(message, out addedAsList, out alphabetAsList);
                    output = new Alphabet(alphabetAsList, addedAsList, cryptType);
                    return output;
                default:
                    throw new Exception("Invalid CryptType.");
            }
        }
        private static void ExtendAlphabetForEncyption(string message, out List<object> addedAsList, out List<object> alphabetAsList)
        {
            InitializeForVariablesForExtendAlphabet(message, out addedAsList, out alphabetAsList, out List<object> messageAsList);
            
            ExtractAdditionalCharactersFromMessage(addedAsList, alphabetAsList, messageAsList);

            AppendAddedToAlphabet(addedAsList, alphabetAsList);
        }
        private static void ExtractAdditionalCharactersFromMessage(List<object> added, List<object> alphabetAsList, List<object> messageAsList)
        {
            foreach (var textElement in messageAsList)
            {
                if (added.Contains(textElement) == false)
                {
                    if (alphabetAsList.Contains(textElement) == false)
                    {
                        added.Add(textElement);
                    }
                }
            }
        }
        private static void AppendAddedToAlphabet(List<object> added, List<object> alphabetAsList)
        {
            added.Sort();  // For security reasons.
            foreach (var item in added)
            {
                alphabetAsList.Add(item);
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