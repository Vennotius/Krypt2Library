using System.ComponentModel;

namespace Krypt2Library
{
    public class Betor : ICipher
    {
        private BetorAlphabetFactory _alphabetFactory;


        public string Encrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            _alphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Encryption);

            throw new NotImplementedException();
        }
        

        public string Decrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            throw new NotImplementedException();
        }
    }
}