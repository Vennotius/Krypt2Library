namespace Krypt2Library
{
    internal ref struct MessageAsIndexArray
    {
        private readonly Alphabet _alphabet;
        private Span<int> _indexArray;

        internal Span<int> IndexArray
        {
            readonly get => _indexArray;
            init => _indexArray = value;
        }

        internal readonly List<object> MessageAsListOfTextElements
            => MessageToListOfTextElements(_indexArray, _alphabet);

        public MessageAsIndexArray(List<string> messageAsList, Alphabet alphabet)
        {
            _alphabet = alphabet;
            _indexArray = ConvertToIndexArray(messageAsList, _alphabet);
        }

        private static List<object> MessageToListOfTextElements(Span<int> messageArray, Alphabet alphabet)
        {
            List<object> output = new();

            for (int i = 0; i < messageArray.Length; i++)
            {
                int index = WrapperForShift(0, messageArray[i], alphabet.AllCharacters.Count);
                output.Add(alphabet.AllCharacters[index]);
            }

            return output;
        }

        private static Span<int> ConvertToIndexArray(List<string> messageAsList, Alphabet alphabet)
        {
            int[] underlyingArray = new int[messageAsList.Count];
            Span<int> output = new Span<int>(underlyingArray);

            Dictionary<string, int> alphabetIndex = alphabet.GetAlphabetIndexDictionary();

            for (int i = 0; i < messageAsList.Count; i++)
            {
                output[i] = alphabetIndex[messageAsList[i]];
            }

            return output;
        }

        public static int WrapperForShift(int startIndex, int shiftAmount, int length)
            => (startIndex + (shiftAmount % length) + length) % length;
    }
}