namespace Krypt2Library
{
    internal class RandomContainer
    {
        public Random Random { get; init; }
        public int[] ShiftAmounts { get; private set; }

        public RandomContainer(Random random, int messageLength, int alphabetLength)
        {
            Random = random;
            PopulateShiftAmounts(random, messageLength, alphabetLength);
        }

        private void PopulateShiftAmounts(Random random, int messageLength, int alphabetLength)
        {
            ShiftAmounts = new int[messageLength];
            for (int i = 0; i < messageLength; i++)
            {
                ShiftAmounts[i] = random.Next(alphabetLength);
            }
        }
    }
}