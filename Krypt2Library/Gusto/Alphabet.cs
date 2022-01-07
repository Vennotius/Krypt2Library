namespace Krypt2Library
{
    internal class Alphabet
    {
        public List<object> AllCharacters { get; set; }
        public List<object> AddedCharacters { get; set; }
        public CryptType CryptType { get; set; }

        public Alphabet(List<object> alphabetCharacters, List<object> addedCharacters, CryptType cryptType)
        {
            AllCharacters = alphabetCharacters ?? throw new ArgumentNullException(nameof(alphabetCharacters));
            AddedCharacters = addedCharacters ?? throw new ArgumentNullException(nameof(addedCharacters));
            CryptType = cryptType;
        }
    }
}