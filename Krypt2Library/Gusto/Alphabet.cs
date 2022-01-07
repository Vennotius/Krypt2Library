using System.Globalization;

namespace Krypt2Library
{
    internal class Alphabet
    {
        public List<object> AllCharacters { get; set; }
        public List<object> AddedCharacters { get; set; }
        public CryptType CryptType { get; init; }

        public Alphabet(string alphabet, CryptType cryptType)
        {
            AllCharacters = StringToListOfObjects(alphabet);
            AddedCharacters = new List<object>();
            CryptType = cryptType;
        }

        private static List<object> StringToListOfObjects(string input)
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