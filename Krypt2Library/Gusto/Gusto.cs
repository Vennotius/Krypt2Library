using System.ComponentModel;
using System.Text;

namespace Krypt2Library
{
    public class Gusto : ICipher
    {
        private List<Random> _randoms = new();
        private List<RandomContainer> _randomContainers = new();

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
            PopulateRandomContainers(message.Length, alphabet.AllCharacters.Count);

            PrependAddedCharactersIfEncryption(cryptType, output, alphabet.AddedCharacters);

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

            StringBuilder output = ConvertListToStringBuilder(messageAsIndexArray.MessageAsList);

            return output.ToString();
        }
        private void ShiftOnePass(MessageAsIndexArray message, Alphabet alphabet, int passCount)
        {
            for (int i = 0; i < message.MessageArray.Length; i++)
            {
                int shiftAmount = _randomContainers[passCount].ShiftAmounts[i];
                //int shiftAmount = _randoms[passCount].Next(alphabet.AllCharacters.Count);
                if (alphabet.CryptType == CryptType.Decryption) shiftAmount *= -1;
                message.MessageArray[i] += shiftAmount;
            }
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
        private static MessageAsIndexArray ConvertMessageToIndexArray(string message, Alphabet alphabet)
        {
            List<object> messageAsList = ConvertMessageToListOfTextElements(message, alphabet.CryptType, alphabet.AddedCharacters.Count);
            
            var output = new MessageAsIndexArray(messageAsList, alphabet);
            
            return output;
        }
        private static List<object> ConvertMessageToListOfTextElements(string message, CryptType cryptType, int addedCharactersCount)
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
        private void PopulateRandomContainers(int messageLength, int alphabetCount)
        {
            Parallel.ForEach(_randoms, random =>
            {
                _randomContainers.Add(new RandomContainer(random, messageLength, alphabetCount));
            });
        }
    }
}