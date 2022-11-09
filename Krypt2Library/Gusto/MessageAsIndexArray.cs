namespace Krypt2Library
{
    internal class MessageAsIndexArray
    {
        internal int[] IndexArray { get; init; }
        internal List<object> MessageAsListOfTextElements
        {
            get => MessageToListOfTextElements(IndexArray, _alphabet);
        }

        private readonly Alphabet _alphabet;

        public MessageAsIndexArray(List<string> messageAsList, Alphabet alphabet)
        {
            _alphabet = alphabet;
            IndexArray = ConvertToIndexArray(messageAsList, _alphabet);
        }

        private static List<object> MessageToListOfTextElements(int[] messageArray, Alphabet alphabet)
        {
            List<object> output = new();

            foreach (int item in messageArray)
            {
                output.Add(alphabet.AllCharacters[WrapperForShift(0, item, alphabet.AllCharacters.Count)]);
            }

            return output;
        }
        private static int[] ConvertToIndexArray(List<string> messageAsList, Alphabet alphabet)
        {
            int[] output = new int[messageAsList.Count];

            Dictionary<string, int> alphabetIndex = alphabet.GetAlphabetIndexDictionary();

            for (int i = 0; i < messageAsList.Count; i++)
            {
                output[i] = alphabetIndex[messageAsList[i]];
            }

            return output;
        }
        public static int WrapperForShift(int startIndex, int shiftAmount, int length)
        {
            int outputIndex = startIndex + (shiftAmount % length);

            if (outputIndex < 0) outputIndex += length;

            return outputIndex;
        }
    }
}