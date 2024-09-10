namespace Krypt2Library
{
    public interface IRandom
    {
        int NextInt32();
        int NextInt32(int maxValue);
    }
}