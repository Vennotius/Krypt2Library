using System.Text;

namespace Krypt2Library
{
    public class Gusto : ICipher
    {
        private List<Random> _randoms;

        public string Encrypt(string passphrase, string message)
        {
            _randoms = RandomsFactory.GetRandomsForPassphrase(passphrase, CryptType.Encryption);

            return Shift(message, CryptType.Encryption);
        }
        public string Decrypt(string passphrase, string message)
        {
            _randoms = RandomsFactory.GetRandomsForPassphrase(passphrase, CryptType.Decryption);

            return Shift(message, CryptType.Decryption);
        }


        private string Shift(string message, CryptType cryptType)
        {
            var output = new StringBuilder();
            var alphabet = GustoAlphabetManager.InitializeAlphabet(cryptType, message);

            PrependAddedCharactersIfEncrypting(cryptType, output, alphabet.AddedCharacters);

            output.Append(ShiftMessage(message, alphabet));

            return output.ToString();
        }
        private string ShiftMessage(string message, Alphabet alphabet)
        {
            MessageAsIndexArray messageAsIndexArray = ConvertMessageToIndexArray(message, alphabet);

            for (int passCount = 0; passCount < _randoms.Count; passCount++)
            {
                ShiftOnePass(messageAsIndexArray, alphabet, passCount);
            }

            StringBuilder output = ConvertListOfTextElementsToStringBuilder(messageAsIndexArray.MessageAsListOfTextElements);

            return output.ToString();
        }
        private void ShiftOnePass(MessageAsIndexArray message, Alphabet alphabet, int passCount)
        {
            for (int i = 0; i < message.IndexArray.Length; i++)
            {
                int shiftAmount = _randoms[passCount].Next(alphabet.AllCharacters.Count);
                if (alphabet.CryptType == CryptType.Decryption) shiftAmount *= -1;
                message.IndexArray[i] += shiftAmount;
            }
        }


        private static void PrependAddedCharactersIfEncrypting(CryptType cryptType, StringBuilder output, List<object> addedAsList)
        {
            if (cryptType == CryptType.Encryption)
            {
                foreach (var item in addedAsList)
                {
                    output.Append(item);
                }
            }
        }
        private static MessageAsIndexArray ConvertMessageToIndexArray(string message, Alphabet alphabet)
        {
            List<object> messageAsList = ConvertMessageToListOfTextElements(message, alphabet.CryptType, alphabet.AddedCharacters.Count);

            var output = new MessageAsIndexArray(messageAsList, alphabet);

            return output;
        }
        private static List<object> ConvertMessageToListOfTextElements(string message, CryptType cryptType, int addedCharactersCount)
        {
            var messageAsList = GustoAlphabetManager.StringToListOfObjects(message);

            RemovePrependedCharactersIfDecrypting(cryptType, addedCharactersCount, messageAsList);

            return messageAsList;
        }
        private static void RemovePrependedCharactersIfDecrypting(CryptType cryptType, int addedCharactersCount, List<object> messageAsList)
        {
            if (cryptType == CryptType.Decryption)
            {
                for (int i = 0; i < addedCharactersCount; i++)
                {
                    messageAsList.RemoveAt(0);
                }
            }
        }

        private static StringBuilder ConvertListOfTextElementsToStringBuilder(List<object> messageAsList)
        {
            var output = new StringBuilder();
            foreach (var textElement in messageAsList)
            {
                output.Append(textElement);
            }

            return output;
        }
    }
}