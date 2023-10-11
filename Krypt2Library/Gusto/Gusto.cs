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
            Alphabet alphabet = GustoAlphabetManager.InitializeAlphabet(cryptType, message);

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

            return string.Concat(messageAsIndexArray.MessageAsListOfTextElements);
        }

        private void ShiftOnePass(MessageAsIndexArray message, Alphabet alphabet, int passCount)
        {
            Random random = _randoms[passCount];
            
            for (int i = 0; i < message.IndexArray.Length; i++)
            {
                int shiftAmount = random.Next(alphabet.AllCharacters.Count);

                // In case of decryption, we need to reverse the shift that happened during encryption.
                if (alphabet.CryptType == CryptType.Decryption) shiftAmount *= -1;

                message.IndexArray[i] += shiftAmount;
            }
        }


        private static void PrependAddedCharactersIfEncrypting(CryptType cryptType, StringBuilder output, List<string> addedAsList)
        {
            if (cryptType == CryptType.Encryption)
            {
                output.Append(string.Join("", addedAsList));
            }
        }

        private static MessageAsIndexArray ConvertMessageToIndexArray(string message, Alphabet alphabet)
        {
            List<string> messageAsList = GustoAlphabetManager.StringToListOfObjects(message);

            if (alphabet.CryptType == CryptType.Decryption)
            {
                messageAsList.RemoveRange(0, alphabet.AddedCharacters.Count);
            }

            return new MessageAsIndexArray(messageAsList, alphabet);
        }
    }
}