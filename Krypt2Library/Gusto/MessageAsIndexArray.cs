namespace Krypt2Library
{
    internal class MessageAsIndexArray
    {
        public int[] IndexArray { get; private set; }
        public List<object> MessageAsListOfTextElements
        {
            get
            {
                return MessageToListOfTextElements(IndexArray, _alphabet);
            }
        }

        private readonly Alphabet _alphabet;

        public MessageAsIndexArray(List<object> messageAsList, Alphabet alphabet)
        {
            _alphabet = alphabet;
            IndexArray = ConvertToIndexArray(messageAsList, _alphabet);
        }

        private static List<object> MessageToListOfTextElements(int[] messageArray, Alphabet alphabet)
        {
            var output = new List<object>();

            foreach (var item in messageArray)
            {
                output.Add(alphabet.AllCharacters[WrapperForShift(0, item, alphabet.AllCharacters.Count)]);
            }

            return output;
        }
        private static int[] ConvertToIndexArray(List<object> messageAsList, Alphabet alphabet)
        {
            var output = new int[messageAsList.Count];

            var alphabetIndex = alphabet.AlphabetIndexDictionary;

            for (int i = 0; i < messageAsList.Count; i++)
            {
                output[i] = alphabetIndex[messageAsList[i]];
            }

            return output;
        }
        private static int WrapperForShift(int inputIndex, int shiftAmount, int length)
        {
            var output = inputIndex + shiftAmount;

            while (output > length - 1)
            {
                output -= length;
            }

            while (output < 0)
            {
                output += length;
            }

            return output;
        }
    }
}