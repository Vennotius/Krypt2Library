namespace Krypt2Library
{
    internal class MessageAsIndexArray
    {
        private readonly Alphabet _alphabet;
        private readonly int[] _indexArray;

        internal int[] IndexArray
        {
            get => _indexArray;
            init => _indexArray = value;
        }

        internal IEnumerable<string> MessageAsListOfTextElements
            => MessageToListOfTextElements(_indexArray, _alphabet);

        public MessageAsIndexArray(List<string> messageAsList, Alphabet alphabet)
        {
            _alphabet = alphabet;
            _indexArray = ConvertToIndexArray(messageAsList, _alphabet);
        }

        private static IEnumerable<string> MessageToListOfTextElements(int[] messageArray, Alphabet alphabet)
        {
            for (int i = 0; i < messageArray.Length; i++)
            {
                int index = WrapperForShift(0, messageArray[i], alphabet.AllCharacters.Count);
                yield return alphabet.AllCharacters[index];
            }
        }

        private static int[] ConvertToIndexArray(List<string> messageAsList, Alphabet alphabet)
        {
            int[] output = new int[messageAsList.Count];

            Dictionary<string, int> alphabetIndex = alphabet.GetAlphabetIndexDictionary();

            for (int i = 0; i < messageAsList.Count; i++)
                output[i] = alphabetIndex[messageAsList[i]];

            return output;
        }

        private static int WrapperForShift(int startIndex, int shiftAmount, int length)
            => (startIndex + shiftAmount % length + length) % length;
    }
}