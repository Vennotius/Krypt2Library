using Microsoft.VisualStudio.TestTools.UnitTesting;
using Krypt2Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class MessageAsIndexArrayTests
    {
        [DataRow(0, 10, 8, 2)]
        [DataRow(0, 80, 8, 0)]
        [DataRow(0, 8, 8, 0)]
        [DataRow(0, 1, 8, 1)]
        [DataRow(0, -1, 8, 7)]
        [DataRow(0, -10, 8, 6)]
        [DataRow(4, -1, 8, 3)]
        [DataRow(4, -5, 8, 7)]
        [DataRow(4, -24, 8, 4)]
        [DataRow(4, -25, 8, 3)]
        [TestMethod()]
        public void WrapperForShiftTest(int inputIndex, int shiftAmount, int length, int expected)
        {
            int result = MessageAsIndexArray.WrapperForShift(inputIndex, shiftAmount, length);

            Assert.AreEqual(expected, result);
        }
    }
}