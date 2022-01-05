using System.Globalization;
using System.Text;

namespace Krypt2Library
{
    public class GustoAlphabetManager
    {
        private readonly string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";

        internal (List<object> alphabet, List<object> added) InitializeAlphabet(CryptType activity, string message)
        {
            List<object> added = new List<object>();
            List<object> alphabetAsList = new List<object>();

            switch (activity)
            {
                case CryptType.Encryption:
                    ExtendAlphabetForEncyption(message, out added, out alphabetAsList);
                    return (alphabetAsList, added);
                case CryptType.Decryption:
                    ExtendAlphabetForEncyption(message, out added, out alphabetAsList);
                    return (alphabetAsList, added);
                default:
                    ExtendAlphabetForEncyption(message, out added, out alphabetAsList);
                    return (alphabetAsList, added);
            }
        }

        private void ExtendAlphabetForEncyption(string message, out List<object> added, out List<object> alphabetAsList)
        {
            added = new List<object>();
            alphabetAsList = StringToListOfObjects(_standardAlphabet);
            var messageAsList = StringToListOfObjects(message);

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

            added.Sort();
            foreach (var item in added)
            {
                alphabetAsList.Add(item);
            }
        }

        internal static List<object> StringToListOfObjects(string input)
        {
            var output = new List<object>();

            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(input);

            while (enumerator.MoveNext())
            {
                output.Add(enumerator.Current);
                var t = enumerator.Current;
            }

            return output;
        }
    }
}