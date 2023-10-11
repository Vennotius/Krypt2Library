namespace Krypt2Library
{
    internal class Alphabet
    {
        public List<string> AllCharacters { get; init; }
        public List<string> AddedCharacters { get; init; }
        public CryptType CryptType { get; init; }

        public Alphabet(string alphabet, CryptType cryptType)
        {
            AllCharacters = GustoAlphabetManager.StringToListOfObjects(alphabet);
            AddedCharacters = new List<string>();
            CryptType = cryptType;
        }

        public Dictionary<string, int> GetAlphabetIndexDictionary()
        {
            Dictionary<string, int> output = new(AllCharacters.Count);

            for (int charIndex = 0; charIndex < AllCharacters.Count; charIndex++)
            {
                output[AllCharacters[charIndex]] = charIndex;
            }

            return output;
        }
    }
}