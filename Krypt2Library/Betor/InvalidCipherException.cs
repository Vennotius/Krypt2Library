namespace Krypt2Library
{
    public class InvalidCipherException : Exception
    {
        public InvalidCipherException(string? message) : base(message)
        {
        }
    }
}