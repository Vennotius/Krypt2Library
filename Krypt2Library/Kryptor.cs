using System.ComponentModel;

namespace Krypt2Library
{
    public class Kryptor
    {
        public ICipher Cipher { get; set; }
        public BackgroundWorker? BackgroundWorker { get; }

        public Kryptor(ICipher cipher, BackgroundWorker backgroundWorker)
        {
            Cipher = cipher;
            BackgroundWorker = backgroundWorker;
        }

        public string Encrypt(string passphrase, string message)
        {
            if (string.IsNullOrEmpty(passphrase)) throw new ArgumentException($"'{nameof(passphrase)}' cannot be null or empty.", nameof(passphrase));
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.", nameof(message));

            return Cipher.Encrypt(passphrase, message, BackgroundWorker);
        }

        public string Decrypt(string passphrase, string message)
        {
            if (string.IsNullOrEmpty(passphrase)) throw new ArgumentException($"'{nameof(passphrase)}' cannot be null or empty.", nameof(passphrase));
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.", nameof(message));

            return Cipher.Decrypt(passphrase, message, BackgroundWorker);
        }
    }
}