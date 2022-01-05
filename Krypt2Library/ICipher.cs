using System.ComponentModel;

namespace Krypt2Library
{
    public interface ICipher
    {
        string Encrypt(string passphrase, string message, BackgroundWorker? backgroundWorker);
        string Decrypt(string passphrase, string message, BackgroundWorker? backgroundWorker);
    }
}