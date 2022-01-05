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

            return Shift(message, backgroundWorker, CryptType.Encryption);
        }
        public string Decrypt(string passphrase, string message, BackgroundWorker? backgroundWorker)
        {
            _randoms = RandomsFactory.GetRandomsForPassphrase(passphrase, CryptType.Decryption);

            return Shift(message, backgroundWorker, CryptType.Decryption);
        }


        private string Shift(string message, BackgroundWorker? backgroundWorker, CryptType cryptType)
        {
            var output = new StringBuilder();

            (var alphabetAsList, var addedAsList) = GustoAlphabetManager.InitializeAlphabet(cryptType, message);

            PrependAddedCharactersForEncryption(cryptType, output, addedAsList);

            output.Append(ShiftMessage(message, alphabetAsList, backgroundWorker, cryptType, addedAsList.Count));

            return output.ToString();
        }
        private string ShiftMessage(string message, List<object> alphabetAsList, BackgroundWorker? backgroundWorker, CryptType cryptType, int addedCharactersCount)
        {
            List<object> messageAsList = ConvertMessageToList(message, cryptType, addedCharactersCount);

            for (int passCount = 0; passCount < _randoms.Count; passCount++)
            {
                ShiftOnePass(messageAsList, alphabetAsList, backgroundWorker, passCount, cryptType);
            }

            StringBuilder output = ConvertListToStringBuilder(messageAsList);

            return output.ToString();
        }
        private void ShiftOnePass(List<object> messageAsList, List<object> alphabetAsList, BackgroundWorker? backgroundWorker, int passCount, CryptType cryptType)
        {
            for (int i = 0; i < messageAsList.Count; i++)
            {
                int shiftAmount = _randoms[passCount].Next(alphabetAsList.Count);
                if (cryptType == CryptType.Decryption) shiftAmount *= -1;
                messageAsList[i] = ShiftTextElement(messageAsList[i], alphabetAsList, shiftAmount);
            }
        }
        private static object ShiftTextElement(object textElement, List<object> alphabetAsList, int shiftAmount)
        {
            int startIndex = alphabetAsList.IndexOf(textElement);
            int shiftedIndex = ShiftWrapper(startIndex, shiftAmount, alphabetAsList.Count);
            return alphabetAsList[shiftedIndex];
        }
        private static int ShiftWrapper(int inputIndex, int shiftAmount, int length)
        {
            if (shiftAmount == 0) return inputIndex;

            var output = inputIndex + shiftAmount;

            if (output > 0)
            {
                while (output > length - 1)
                {
                    output -= length;
                }
            }

            if (output < 0)
            {
                while (output < 0)
                {
                    output += length;
                }
            }

            return output;
        }


        private static void PrependAddedCharactersForEncryption(CryptType cryptType, StringBuilder output, List<object> addedAsList)
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