namespace Krypt2Library
{
    internal class Alphabet
    {
        public List<object> AllCharacters { get; init; }
        public List<object> AddedCharacters { get; init; }
        public CryptType CryptType { get; init; }

        public Alphabet(string alphabet, CryptType cryptType)
        {
            AllCharacters = GustoAlphabetManager.StringToListOfObjects(alphabet);
            AddedCharacters = new List<object>();
            CryptType = cryptType;
        }

        public Dictionary<object, int> GetAlphabetIndexDictionary()
        {
            Dictionary<object, int> output = new();

            for (int charIndex = 0; charIndex < AllCharacters.Count; charIndex++)
            {
                output[AllCharacters[charIndex]] = charIndex;
            }

            return output;
        }
    }
}