namespace Krypt2Library
{
    internal class Message
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

        private List<object> MessageToList(int[] messageArray, Alphabet alphabet)
        {
            var output = new List<object>();

            foreach (var item in messageArray)
            {
                output.Add(alphabet.AllCharacters[item]);
            }

            return output;
        }

        public Message(List<object> messageAsList, Alphabet alphabet)
        {
            Alphabet = alphabet;
            MessageArray = ConvertToArray(messageAsList, Alphabet);
        }

        private static int[] ConvertToArray(List<object> messageAsList, Alphabet alphabet)
        {
            var output = new int[messageAsList.Count];

            for (int i = 0; i < messageAsList.Count; i++)
            {
                output[i] = alphabet.AllCharacters.IndexOf(messageAsList[i]);
            }

            return output;
        }
    }
}