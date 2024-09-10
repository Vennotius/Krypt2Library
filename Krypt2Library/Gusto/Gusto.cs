using System.Text;

namespace Krypt2Library
{
    public class Gusto : ICipher
    {
        private List<IRandom> _randoms;

        public string Encrypt(string passphrase, string message)
        {
            // Normalize line endings
            string normalizedMessage = message.Replace(Environment.NewLine, "\n");
            _randoms = RandomsFactory.GetRandomsFromPassphrase(passphrase, CryptType.Encryption);

            return Shift(normalizedMessage, CryptType.Encryption);
        }

        public string Decrypt(string passphrase, string message)
        {
            _randoms = RandomsFactory.GetRandomsFromPassphrase(passphrase, CryptType.Decryption);

            // Convert back to the appropriate line ending for the platform
            return Shift(message, CryptType.Decryption).Replace("\n", Environment.NewLine);
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
            int allCharactersCount = alphabet.AllCharacters.Count;

            for (int ri = 0; ri < _randoms.Count; ri++)
            {
                IRandom random = _randoms[ri];

                for (int ci = ri; ci < messageAsIndexArray.IndexArray.Length; ci += _randoms.Count)
                {
                    int shiftAmount = random.NextInt32(allCharactersCount);

                    // In case of decryption, we need to reverse the shift that happened during encryption.
                    if (alphabet.CryptType == CryptType.Decryption)
                        shiftAmount *= -1;

                    messageAsIndexArray.IndexArray[ci] += shiftAmount;
                }
            }

            return string.Concat(messageAsIndexArray.MessageAsListOfTextElements);
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