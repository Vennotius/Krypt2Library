using System.ComponentModel;
using System.Text;

namespace Krypt2Library
{
    public class Gusto : ICipher
    {
        private List<Random> _randoms = new();

        public string Encrypt(string passphrase, string message, BackgroundWorker? backgroundWorker)
        {
            _randoms = RandomsFactory.GetRandomsForPassphrase(passphrase, CryptType.Encryption);

            return Shift(message, CryptType.Encryption);
        }
        public string Decrypt(string passphrase, string message, BackgroundWorker? backgroundWorker)
        {
            _randoms = RandomsFactory.GetRandomsForPassphrase(passphrase, CryptType.Decryption);

            return Shift(message, CryptType.Decryption);
        }


        private string Shift(string message, CryptType cryptType)
        {
            var output = new StringBuilder();
            var alphabet = GustoAlphabetManager.InitializeAlphabet(cryptType, message);

            PrependAddedCharactersIfEncryption(cryptType, output, alphabet.AddedCharacters);

            output.Append(ShiftMessage(message, alphabet));

            return output.ToString();
        }
        private string ShiftMessage(string message, Alphabet alphabet)
        {
            List<object> messageAsList = ConvertMessageToList(message, alphabet.CryptType, alphabet.AddedCharacters.Count);

            for (int passCount = 0; passCount < _randoms.Count; passCount++)
            {
                ShiftOnePass(messageAsList, alphabet, passCount);
            }

            StringBuilder output = ConvertListToStringBuilder(messageAsList);

            return output.ToString();
        }
        private void ShiftOnePass(List<object> messageAsList, Alphabet alphabet, int passCount)
        {
            for (int i = 0; i < messageAsList.Count; i++)
            {
                int shiftAmount = _randoms[passCount].Next(alphabet.AllCharacters.Count);
                if (alphabet.CryptType == CryptType.Decryption) shiftAmount *= -1;
                messageAsList[i] = ShiftTextElement(messageAsList[i], alphabet, shiftAmount);
            }
        }
        private static object ShiftTextElement(object textElement, Alphabet alphabet, int shiftAmount)
        {
            int startIndex = alphabet.AllCharacters.IndexOf(textElement);
            int shiftedIndex = WrapperForShift(startIndex, shiftAmount, alphabet.AllCharacters.Count);
            return alphabet.AllCharacters[shiftedIndex];
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


        private static void PrependAddedCharactersIfEncryption(CryptType cryptType, StringBuilder output, List<object> addedAsList)
        {
            if (cryptType == CryptType.Encryption)
            {
                foreach (var item in addedAsList)
                {
                    output.Append(item);
                }
            }
        }
        private static List<object> ConvertMessageToList(string message, CryptType cryptType, int addedCharactersCount)
        {
            var messageAsList = GustoAlphabetManager.StringToListOfObjects(message);
            if (cryptType == CryptType.Decryption)
            {
                for (int i = 0; i < addedCharactersCount; i++)
                {
                    messageAsList.RemoveAt(0);
                }
            }

            return messageAsList;
        }
        private static StringBuilder ConvertListToStringBuilder(List<object> messageAsList)
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