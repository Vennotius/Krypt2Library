namespace Krypt2Library
{
    public class Xoshiro256SS(ulong seed1, ulong seed2, ulong seed3, ulong seed4) : IRandom
    {
        private readonly ulong[] _state = [seed1, seed2, seed3, seed4];

        private static ulong RotateLeft(ulong x, int k)
            => (x << k) | (x >> (64 - k));

        private ulong NextULong()
        {
            ulong result = RotateLeft(_state[1] * 5, 7) * 9;

            ulong t = _state[1] << 17;

            _state[2] ^= _state[0];
            _state[3] ^= _state[1];
            _state[1] ^= _state[2];
            _state[0] ^= _state[3];

            _state[2] ^= t;
            _state[3] = RotateLeft(_state[3], 45);

            return result;
        }

        public int NextInt32() 
            => (int)(NextULong() & 0xFFFFFFFF);

        public int NextInt32(int maxValue)
        {
            if (maxValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxValue), "maxValue must be greater than 0.");

            int result;
            do
            {
                result = NextInt32();
            } while (result == int.MinValue);  // Handle int.MinValue case directly

            return Math.Abs(result) % maxValue;
        }
    }
}
