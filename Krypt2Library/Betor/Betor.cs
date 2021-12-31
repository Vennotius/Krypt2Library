using System.ComponentModel;
using System.Text;

namespace Krypt2Library
{
    public class Betor : ICipher
    {
        private BetorAlphabetFactory AlphabetFactory { get; set; }

        public string Encrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            AlphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Encryption);

            PrependAdditionalAlphabetCharacters(output);

            EncryptMessage(message, backgroundWorker, output);

            return output.ToString();
        }
        private void EncryptMessage(string message, BackgroundWorker backgroundWorker, StringBuilder output)
        {
            for (int passIndex = 0; passIndex < 8; passIndex++)
            {
                message = EncryptOnePass(message, passIndex, backgroundWorker);
            }

            output.Append(message);

            AlphabetFactory.Reset();
        }
        private string EncryptOnePass(string message, int passIndex, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            var currentCharacterIndex = message.Length * passIndex;
            
            var totalCharactersToProcess = (double)message.Length * 8;
            var onePercentOfTotal = totalCharactersToProcess / 100;
            var currentPercent = (currentCharacterIndex / onePercentOfTotal);

            for (int i = 0; i < message.Length; i++)
            {
                output.Append(EncryptCharacter(message[i], passIndex));
                
                currentCharacterIndex++;
                
                currentPercent = ReportProgress(backgroundWorker, currentCharacterIndex, totalCharactersToProcess, onePercentOfTotal, currentPercent);
            }

            return output.ToString();
        }
        private char EncryptCharacter(char c, int passIndex)
        {
            var index = AlphabetFactory.Alphabet.IndexOf(c);
            var cipherAlphabet = AlphabetFactory.GetAlphabetForNextCharacter(passIndex);

            return cipherAlphabet[index];
        }

        public string Decrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            AlphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Decryption);
            var startIndex = AlphabetFactory.MessageStartIndex;
            try
            {
                DecryptMessage(message, backgroundWorker, output, startIndex);
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Invalid Cipher Text");
            }

            return output.ToString();
        }
        private void DecryptMessage(string message, BackgroundWorker backgroundWorker, StringBuilder output, int startIndex)
        {
            for (int i = 0; i < 7; i++)
            {
                message = AlphabetFactory.Added + DecryptOnePass(startIndex, message, i, backgroundWorker);
            }

            message = DecryptOnePass(startIndex, message, 7, backgroundWorker); // Don't prepend "added" after the last pass

            output.Append(message);

            AlphabetFactory.Reset();
        }
        private string DecryptOnePass(int startIndex, string message, int passIndex, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            var currentCharacterIndex = message.Length * passIndex;

            var totalCharactersToProcess = (double)message.Length * 8;
            var onePercentOfTotal = totalCharactersToProcess / 100;
            var currentPercent = (currentCharacterIndex / onePercentOfTotal);

            for (int i = startIndex; i < message.Length; i++)
            {
                output.Append(DecryptCharacter(message[i], passIndex));
                currentCharacterIndex++;

                currentPercent = ReportProgress(backgroundWorker, currentCharacterIndex, totalCharactersToProcess, onePercentOfTotal, currentPercent);
            }

            return output.ToString();
        }
        private char DecryptCharacter(char c, int passIndex)
        {
            var index = AlphabetFactory.GetAlphabetForNextCharacter(passIndex).IndexOf(c);
            var cipherAlphabet = AlphabetFactory.Alphabet;

            return cipherAlphabet[index];
        }

        private void PrependAdditionalAlphabetCharacters(StringBuilder output)
        {
            var added = AlphabetFactory.Added;
            output.Append($"{added}");
        }

        private static double ReportProgress(BackgroundWorker backgroundWorker, int currentCharacterIndex, double totalCharactersToProcess, double onePercentOfTotal, double currentPercent)
        {
            if (backgroundWorker == null) return currentPercent;
            
            if (currentCharacterIndex == totalCharactersToProcess)
            {
                backgroundWorker.ReportProgress(100);
            }
            else if (currentCharacterIndex / onePercentOfTotal > currentPercent + 1)
            {
                backgroundWorker.ReportProgress(Convert.ToInt32(currentPercent));
                currentPercent = (currentCharacterIndex / onePercentOfTotal);
            }

            return currentPercent;
        }
    }
}