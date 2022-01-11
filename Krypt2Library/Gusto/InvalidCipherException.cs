namespace Krypt2Library
{
    internal class InvalidCipherException : Exception
    {
        public InvalidCipherException(string? message) : base(message)
        {
        }
    }
}