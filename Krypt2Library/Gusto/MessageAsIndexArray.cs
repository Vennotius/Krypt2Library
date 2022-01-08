namespace Krypt2Library
{
    internal class MessageAsIndexArray
    {
        public int[] MessageArray { get; set; }
        public Alphabet Alphabet { get; set; }
        public List<object> MessageAsList
        {
            get
            {
                return MessageToList(MessageArray, Alphabet);
            }
        }
        
        public MessageAsIndexArray(List<object> messageAsList, Alphabet alphabet)
        {
            Alphabet = alphabet;
            MessageArray = ConvertToArray(messageAsList, Alphabet);
        }

        private List<object> MessageToList(int[] messageArray, Alphabet alphabet)
        {
            var output = new List<object>();

            foreach (var item in messageArray)
            {
                output.Add(alphabet.AllCharacters[WrapperForShift(0, item, alphabet.AllCharacters.Count)]);
            }

            return output;
        }
        private static int[] ConvertToArray(List<object> messageAsList, Alphabet alphabet)
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