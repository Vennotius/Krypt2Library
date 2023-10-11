namespace Krypt2Library
{
    internal class MessageAsIndexArray
    {
        private readonly Alphabet _alphabet;

        internal int[] IndexArray { get; init; }
        internal List<object> MessageAsListOfTextElements => MessageToListOfTextElements(IndexArray, _alphabet);

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
                int index = WrapperForShift(0, item, alphabet.AllCharacters.Count);
                output.Add(alphabet.AllCharacters[index]);
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
            => (startIndex + (shiftAmount % length) + length) % length;
    }
}