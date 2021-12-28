using System.ComponentModel;
using System.Text;

namespace Krypt2Library
{
    public class Betor : ICipher
    {
        private BetorAlphabetFactory _alphabetFactory;


        public string Encrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            _alphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Encryption);
            PrependAdditionalAlphabetCharacters(output);

            for (int i = 0; i < 8; i++)
            {
                backgroundWorker.ReportProgress(i + 1);
                message = EncryptMessage(message, passphrase, i);
            }

            _alphabetFactory.Reset();

            output.Append(message);

            return output.ToString();
        }

        private string EncryptMessage(string message, string passphrase, int passIndex)
        {
            var output = new StringBuilder();

            for (int i = 0; i < message.Length; i++)
            {
                output.Append(EncryptCharacter(message[i], passIndex));
            }

            return output.ToString();
        }

        public string Decrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            _alphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Decryption);
            var startIndex = _alphabetFactory.MessageStartIndex;

            for (int i = 0; i < 8; i++)
            {
                backgroundWorker.ReportProgress(i + 1);
                message = DecryptMessage(message, passphrase, i);
            }

            _alphabetFactory.Reset();

            output.Append(message);

            return output.ToString();
        }

        private string DecryptMessage(string message, string passphrase, int passIndex)
        {
            var output = new StringBuilder();

            for (int i = 0; i < message.Length; i++)
            {
                output.Append(DecryptCharacter(message[i], passIndex));
            }

            return output.ToString();
        }

        private char DecryptCharacter(char c, int passIndex)
        {
            var index = _alphabetFactory.GetAlphabetForNextCharacter(passIndex).IndexOf(c);
            var cipherAlphabet = _alphabetFactory.alphabet;

            return cipherAlphabet[index];
        }

        private void PrependAdditionalAlphabetCharacters(StringBuilder output)
        {
            var added = _alphabetFactory.added;
            output.Append($"{added}");
        }

        private char EncryptCharacter(char c, int passIndex)
        {
            var index = _alphabetFactory.alphabet.IndexOf(c);
            var cipherAlphabet = _alphabetFactory.GetAlphabetForNextCharacter(passIndex);

            return cipherAlphabet[index];
        }
    }
}