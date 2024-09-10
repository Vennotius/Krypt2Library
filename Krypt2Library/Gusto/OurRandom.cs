namespace Krypt2Library
{
    /// <summary>
    /// This code is from the .NET repository. 
    /// It is used here in order to guarantee compatibility, and to make it 
    /// possible to reproduce in other languages if one has the source for this.
    /// It works exactly the same as Random.cs, but mostly only includes what is used in the library.
    /// </summary>
    internal class OurRandom : Random
    {
        /// <summary>Reference to the <see cref="Random"/> containing this implementation instance.</summary>
        /// <remarks>Used to ensure that any calls to other virtual members are performed using the Random-derived instance, if one exists.</remarks>
        private readonly int[] _seedArray;
        private int _inext;
        private int _inextp;

        public OurRandom(int Seed)
        {

            // Initialize seed array.
            int[] seedArray = _seedArray = new int[56];

            int subtraction = (Seed == int.MinValue) ? int.MaxValue : Math.Abs(Seed);
            int mj = 161803398 - subtraction; // magic number based on Phi (golden ratio)
            seedArray[55] = mj;
            int mk = 1;

            int ii = 0;
            for (int i = 1; i < 55; i++)
            {
                // The range [1..55] is special (Knuth) and so we're wasting the 0'th position.
                if ((ii += 21) >= 55)
                {
                    ii -= 55;
                }

                seedArray[ii] = mk;
                mk = mj - mk;
                if (mk < 0)
                {
                    mk += int.MaxValue;
                }

                mj = seedArray[ii];
            }

            for (int k = 1; k < 5; k++)
            {
                for (int i = 1; i < 56; i++)
                {
                    int n = i + 30;
                    if (n >= 55)
                    {
                        n -= 55;
                    }

                    seedArray[i] -= seedArray[1 + n];
                    if (seedArray[i] < 0)
                    {
                        seedArray[i] += int.MaxValue;
                    }
                }
            }

            _inextp = 21;
        }

        protected override double Sample() =>
            // Including the division at the end gives us significantly improved random number distribution.
            InternalSample() * (1.0 / int.MaxValue);

        //public override int Next() => InternalSample();

        public override int Next(int maxValue) => (int)(Sample() * maxValue);

        // Not currently used, but is included. (20220101)
        //public override int Next(int minValue, int maxValue)
        //{
        //    long range = (long)maxValue - minValue;
        //    return range <= int.MaxValue ?
        //        (int)(Sample() * range) + minValue :
        //        (int)((long)(GetSampleForLargeRange() * range) + minValue);
        //}

        private int InternalSample()
        {
            int locINext = _inext;
            if (++locINext >= 56)
            {
                locINext = 1;
            }

            int locINextp = _inextp;
            if (++locINextp >= 56)
            {
                locINextp = 1;
            }

            int[] seedArray = _seedArray;
            int retVal = seedArray[locINext] - seedArray[locINextp];

            if (retVal == int.MaxValue)
            {
                retVal--;
            }
            if (retVal < 0)
            {
                retVal += int.MaxValue;
            }

            seedArray[locINext] = retVal;
            _inext = locINext;
            _inextp = locINextp;

            return retVal;
        }
    }

    public class Xoshiro256SS : IRandom
    {
        private ulong[] _state = new ulong[4];

        public Xoshiro256SS(ulong[] seed)
        {
            if (seed.Length != 4)
                throw new ArgumentException("Seed must contain exactly 4 elements.");

            Array.Copy(seed, _state, 4);
        }

        private ulong RotateLeft(ulong x, int k) 
            => (x << k) | (x >> (64 - k));

        private ulong NextBase()
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

        public int Next() 
            => (int)(NextBase() & 0xFFFFFFFF);

        public int Next(int maxValue)
        {
            if (maxValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxValue), "maxValue must be greater than 0.");

            int result;
            do
            {
                result = Next();
            } while (result == int.MinValue);  // Handle int.MinValue case directly

            return Math.Abs(result) % maxValue;
        }
    }
}
