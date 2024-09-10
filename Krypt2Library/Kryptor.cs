using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Krypt2LibraryTests")]
namespace Krypt2Library
{
    public class Kryptor<T> where T : ICipher, new()
    {
        public ICipher Cipher { get; init; }

        public Kryptor()
        {
            Cipher = new T();
        }

        public string Encrypt(string passphrase, string message)
        {
            if (string.IsNullOrEmpty(passphrase)) throw new ArgumentException($"'{nameof(passphrase)}' cannot be null or empty.", nameof(passphrase));
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.", nameof(message));

            return Cipher.Encrypt(passphrase, message);
        }

        public string Decrypt(string passphrase, string message)
        {
            if (string.IsNullOrEmpty(passphrase)) throw new ArgumentException($"'{nameof(passphrase)}' cannot be null or empty.", nameof(passphrase));
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException($"'{nameof(message)}' cannot be null or empty.", nameof(message));

            return Cipher.Decrypt(passphrase, message);
        }
    }
}