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

        public MessageAsIndexArray(List<object> messageAsList, Alphabet alphabet)
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
        private static int[] ConvertToIndexArray(List<object> messageAsList, Alphabet alphabet)
        {
            int[] output = new int[messageAsList.Count];

            Dictionary<object, int> alphabetIndex = alphabet.GetAlphabetIndexDictionary();

            for (int i = 0; i < messageAsList.Count; i++)
            {
                output[i] = alphabetIndex[messageAsList[i]];
            }

            return output;
        }
        private static int WrapperForShift(int inputIndex, int shiftAmount, int length)
        {
            int output = inputIndex + shiftAmount;

            while (output > length - 1) output -= length;

            while (output < 0) output += length;

            return output;
        }
    }
}