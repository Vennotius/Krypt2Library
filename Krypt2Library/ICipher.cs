namespace Krypt2Library
{
    public interface ICipher
    {
        string Encrypt(string passphrase, string message);
        string Decrypt(string passphrase, string message);
    }
}