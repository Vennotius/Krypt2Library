using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class RandomsFactoryTests
    {
        [TestMethod()]
        public void GetRandomsForPassphraseTest()
        {
            List<Random> randomsList = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Encryption);

            Assert.AreEqual(8, randomsList.Count);

            foreach (var random in randomsList)
            {
                Assert.AreEqual(1, randomsList.Where(x => x == random).Count());
            }
        }

        [TestMethod()]
        public void GetRandomsForPassphraseRepeatabilityTest()
        {
            List<Random> randomsList1 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Encryption);
            List<Random> randomsList2 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Encryption);
            CompareRandomsLists(randomsList1, randomsList2);

            List<Random> randomsList3 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Decryption);
            List<Random> randomsList4 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Decryption);
            CompareRandomsLists(randomsList3, randomsList4);

            List<Random> randomsList5 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Encryption);
            List<Random> randomsList6 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Decryption);
            randomsList6.Reverse();
            CompareRandomsLists(randomsList5, randomsList6);

            List<Random> randomsList7 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Decryption);
            List<Random> randomsList8 = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Encryption);
            randomsList8.Reverse();
            CompareRandomsLists(randomsList7, randomsList8);
        }

        private static void CompareRandomsLists(List<Random> randomsList1, List<Random> randomsList2)
        {
            for (int i = 0; i < randomsList1.Count; i++)
            {
                for (int j = 20; j < 30; j++)
                {
                    Assert.AreEqual(randomsList1[i].Next(j), randomsList2[i].Next(j));
                }
            }
        }

        [DataRow(0, 1, 2)]
        [DataRow(3, 4, 5)]
        [DataRow(6, 7, 0)]
        [DataRow(1, 3, 6)]
        [TestMethod()]
        public void RandomsAreIndeedDifferentTest(int a, int b, int c)
        {
            List<Random> randomsList = RandomsFactory.GetRandomsFromPassphrase("test", CryptType.Encryption);
            CompareThree(randomsList, a, b, c);
        }

        private static void CompareThree(List<Random> randomsList, int a, int b, int c)
        {
            var one = new List<int>();
            var two = new List<int>();
            var three = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                one.Add(randomsList[a].Next(50));
                two.Add(randomsList[b].Next(50));
                three.Add(randomsList[c].Next(50));
            }
            Assert.IsFalse(one.SequenceEqual(two));
            Assert.IsFalse(one.SequenceEqual(three));
            Assert.IsFalse(two.SequenceEqual(three));
        }

        [TestMethod()]
        public void GetRandomSeedsFromByteArrayTest()
        {
            byte[] array = new byte[]
            {
                0, 8, 16, 24, 32, 40, 48, 56, 64,
                72, 80, 88, 96, 104, 112, 120, 128,
                1, 9, 17, 25, 33, 41, 49, 57, 65,
                73, 81, 89, 97, 105, 113, 121, 129
            };

            List<int> seedsList = RandomsFactory.GetInt32SeedsFromByteArray(array);

            Assert.AreEqual(8, seedsList.Count);

            foreach (var seed in seedsList)
            {
                Assert.AreEqual(1, seedsList.Where(x => x == seed).Count());
            }
        }

    }
}