using System.ComponentModel;

namespace Krypt2Library
{
    public class Gusto : ICipher
    {
        public string Encrypt(string passphrase, string message, BackgroundWorker? backgroundWorker)
        {
            GustoAlphabetManager.InitializeAlphabet(CryptType.Encryption, message);

            return "";
        }

        public string Decrypt(string passphrase, string message, BackgroundWorker? backgroundWorker)
        {
            throw new NotImplementedException();
        }
    }
}