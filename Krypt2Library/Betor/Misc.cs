namespace Krypt2Library
{
    internal class Misc
    {
        private static int ShiftIndexWrapper(int length, int shiftBy)
        {
            if (shiftBy == 0) return 0;

            if (shiftBy > 0)
            {
                if (shiftBy < length) return shiftBy;

                while (shiftBy >= length)
                {
                    shiftBy -= length;
                }

                return shiftBy;
            }

            if (shiftBy > -length) return shiftBy + length;

            while (shiftBy <= -length)
            {
                shiftBy += length;
            }

            return shiftBy + length;
        }
    }
}