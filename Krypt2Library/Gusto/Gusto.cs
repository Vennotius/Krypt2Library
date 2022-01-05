using System.ComponentModel;

namespace Krypt2Library
{
    public class Gusto : ICipher
    {
        private readonly GustoAlphabetManager _alphabetManager;

        public Gusto(GustoAlphabetManager alphabetManager)
        {
            _alphabetManager = alphabetManager;
        }

        public string Encrypt(string passphrase, string message, BackgroundWorker? backgroundWorker)
        {
            _alphabetManager.InitializeAlphabet(CryptType.Encryption, message);

            return "";
        }

        public string Decrypt(string passphrase, string message, BackgroundWorker? backgroundWorker)
        {
            throw new NotImplementedException();
        }
    }
}