using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class BetorAlphabetFactoryTests
    {
        [DataRow(2, -1, 4, 1)]
        [DataRow(2, -3, 4, 3)]
        [DataRow(0, -9, 4, 3)]
        [DataRow(0, -8, 4, 0)]
        [DataRow(0, -7, 4, 1)]
        [DataRow(0, -6, 4, 2)]
        [DataRow(0, -5, 4, 3)]
        [DataRow(0, -4, 4, 0)]
        [DataRow(0, -3, 4, 1)]
        [DataRow(0, -2, 4, 2)]
        [DataRow(0, -1, 4, 3)]
        [DataRow(0, 0, 4, 0)]
        [DataRow(0, 1, 4, 1)]
        [DataRow(0, 2, 4, 2)]
        [DataRow(0, 3, 4, 3)]
        [DataRow(0, 4, 4, 0)]
        [DataRow(0, 5, 4, 1)]
        [DataRow(0, 6, 4, 2)]
        [DataRow(0, 7, 4, 3)]
        [DataRow(0, 8, 4, 0)]
        [DataRow(0, 9, 4, 1)]
        [DataRow(2, 1, 4, 3)]
        [DataRow(2, 3, 4, 1)]
        [TestMethod()]
        public void ShiftWrapperTest(int inputIndex, int shiftAmount, int length, int expected)
        {
            var shiftBy = BetorAlphabetFactory.ShiftWrapper(inputIndex, shiftAmount, length);

            Assert.AreEqual(expected, shiftBy);
        }
    }
}