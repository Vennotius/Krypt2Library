namespace Krypt2Library
{
    public interface IRandom
    {
        int Next();
        int Next(int maxValue);
    }
}