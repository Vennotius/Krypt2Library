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
            
            EncryptMessage(message, backgroundWorker, output);

            return output.ToString();
        }

        private void EncryptMessage(string message, BackgroundWorker backgroundWorker, StringBuilder output)
        {
            for (int i = 0; i < 8; i++)
            {
                if (backgroundWorker != null) backgroundWorker.ReportProgress(i + 1);

                message = EncryptOnePass(message, i);
            }

            output.Append(message);

            _alphabetFactory.Reset();
        }
        private string EncryptOnePass(string message, int passIndex)
        {
            var output = new StringBuilder();

            for (int i = 0; i < message.Length; i++)
            {
                output.Append(EncryptCharacter(message[i], passIndex));
            }

            return output.ToString();
        }
        private char EncryptCharacter(char c, int passIndex)
        {
            var index = _alphabetFactory.Alphabet.IndexOf(c);
            var cipherAlphabet = _alphabetFactory.GetAlphabetForNextCharacter(passIndex);

            return cipherAlphabet[index];
        }


        public string Decrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            _alphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Decryption);
            var startIndex = _alphabetFactory.MessageStartIndex;
            DecryptMessage(passphrase, message, backgroundWorker, output, startIndex);

            return output.ToString();
        }
        
        private void DecryptMessage(string passphrase, string message, BackgroundWorker backgroundWorker, StringBuilder output, int startIndex)
        {
            for (int i = 0; i < 7; i++)
            {
                if (backgroundWorker != null) backgroundWorker.ReportProgress(i + 1);

                message = _alphabetFactory.Added + DecryptOnePass(startIndex, message, i); 
            }
            
            message = DecryptOnePass(startIndex, message, 7); // Don't prepend "added" after the last pass

            output.Append(message);

            _alphabetFactory.Reset();
        }
        private string DecryptOnePass(int startIndex, string message, int passIndex)
        {
            var output = new StringBuilder();

            for (int i = startIndex; i < message.Length; i++)
            {
                output.Append(DecryptCharacter(message[i], passIndex));
            }

            return output.ToString();
        }
        private char DecryptCharacter(char c, int passIndex)
        {
            var index = _alphabetFactory.GetAlphabetForNextCharacter(passIndex).IndexOf(c);
            var cipherAlphabet = _alphabetFactory.Alphabet;

            return cipherAlphabet[index];
        }

        
        private void PrependAdditionalAlphabetCharacters(StringBuilder output)
        {
            var added = _alphabetFactory.Added;
            output.Append($"{added}");
        }

    }
}