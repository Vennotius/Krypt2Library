using System.Globalization;

namespace Krypt2Library
{
    internal class Alphabet
    {
        public List<object> AllCharacters { get; init; }
        public List<object> AddedCharacters { get; init; }
        public CryptType CryptType { get; init; }
        public Dictionary<object, int> AlphabetIndexDictionary
        {
            get
            {
                var output = new Dictionary<object, int>();
                for (int characterIndex = 0; characterIndex < AllCharacters.Count; characterIndex++)
                {
                    output[AllCharacters[characterIndex]] = characterIndex;
                }

                return output;
            }
        }

        public Alphabet(string alphabet, CryptType cryptType)
        {
            AllCharacters = GustoAlphabetManager.StringToListOfObjects(alphabet);
            AddedCharacters = new List<object>();
            CryptType = cryptType;
        }
    }
}