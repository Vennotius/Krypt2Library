using System.ComponentModel;
using System.Text;

namespace Krypt2Library
{

    public class KeiserSot : ICipher
    {
        private AlphabetFactory _alphabetFactory;

        private int InitializeAlphabetFactory(string passphrase, string message, CryptType cryptType)
        {
            if (string.IsNullOrWhiteSpace(passphrase)) passphrase = "";
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));

            _alphabetFactory = new AlphabetFactory(passphrase, message, cryptType);
            return _alphabetFactory.MessageStartIndex;
        }

        public string Decrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            bool reportProgress = ValidateBackgroundWorker(backgroundWorker);

            InitializeAlphabetFactory(passphrase, message, CryptType.Decryption);
            DecryptMessage(message, backgroundWorker, output, reportProgress);

            _alphabetFactory.Reset();

            return output.ToString();
        }

        private void DecryptMessage(string message, BackgroundWorker backgroundWorker, StringBuilder output, bool reportProgress)
        {
            var startIndex = _alphabetFactory.MessageStartIndex;

            for (int i = startIndex; i < message.Length; i++)
            {
                output.Append(DecryptCharacter(message[i]));

                if (reportProgress == true)
                {
                    ReportProgress(message, backgroundWorker, i);
                }
            }
        }

        private char DecryptCharacter(char c)
        {
            var index = _alphabetFactory.GetAlphabetForNextCharacter().IndexOf(c);
            var cipherAlphabet = _alphabetFactory.alphabet;

            return cipherAlphabet[index];
        }

        public string Encrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            bool reportProgress = ValidateBackgroundWorker(backgroundWorker);

            InitializeAlphabetFactory(passphrase, message, CryptType.Encryption);
            PrependAdditionalAlphabetCharacters(output);
            
            EncryptMessage(message, backgroundWorker, reportProgress, output);

            _alphabetFactory.Reset();

            return output.ToString();
        }

        private void EncryptMessage(string message, BackgroundWorker backgroundWorker, bool reportProgress, StringBuilder output)
        {
            for (int i = 0; i < message.Length; i++)
            {
                output.Append(EncryptCharacter(message[i]));

                if (reportProgress == true)
                {
                    ReportProgress(message, backgroundWorker, i);
                }
            }
        }

        private static void ReportProgress(string message, BackgroundWorker backgroundWorker, int i)
        {
            if (i % 256 == 0 || i == message.Length - 1)
            {
                double progress = ((double)(i + 1) * 100) / message.Length;
                backgroundWorker.ReportProgress(Convert.ToInt32(progress));
            }
        }

        private static bool ValidateBackgroundWorker(BackgroundWorker backgroundWorker)
        {
            bool reportProgress = true;
            if (backgroundWorker == null || backgroundWorker.WorkerReportsProgress == false)
            {
                reportProgress = false;
            }

            return reportProgress;
        }

        private void PrependAdditionalAlphabetCharacters(StringBuilder output)
        {
            var added = _alphabetFactory.added;
            output.Append($"{added}");
        }

        private char EncryptCharacter(char c)
        {
            var index = _alphabetFactory.alphabet.IndexOf(c);
            var cipherAlphabet = _alphabetFactory.GetAlphabetForNextCharacter();

            return cipherAlphabet[index];
        }
    }
}