using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypt2Library
{
    /// <summary>
    /// This code is from the .NET repository. 
    /// It is used here in order to guarantee compatibility, and to make it 
    /// possible to reproduce in other languages if one has the source for this.
    /// It works exactly the same as Random.cs, but mostly only includes what is used in the library.
    /// </summary>
    public class OurRandom: Random
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

        public override int Next() => InternalSample();
        
        public override int Next(int maxValue) => (int)(Sample() * maxValue);

        // Not currently used, but is included. (20220101)
        public override int Next(int minValue, int maxValue)
        {
            long range = (long)maxValue - minValue;
            return range <= int.MaxValue ?
                (int)(Sample() * range) + minValue :
                (int)((long)(GetSampleForLargeRange() * range) + minValue);
        }


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

        private double GetSampleForLargeRange()
        {
            // The distribution of the double returned by Sample is not good enough for a large range.
            // If we use Sample for a range [int.MinValue..int.MaxValue), we will end up getting even numbers only.
            int result = InternalSample();

            // We can't use addition here: the distribution will be bad if we do that.
            if (InternalSample() % 2 == 0) // decide the sign based on second sample
            {
                result = -result;
            }

            double d = result;
            d += int.MaxValue - 1; // get a number in range [0..2*int.MaxValue-1)
            d /= 2u * int.MaxValue - 1;
            return d;
        }
    }
}
