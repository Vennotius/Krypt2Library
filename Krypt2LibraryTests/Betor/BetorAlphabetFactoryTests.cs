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
    public class BetorAlphabetFactoryTests
    {
        [TestMethod()]
        public void GetRandomsForPassphraseTest()
        {
            List<Random> randomsList = BetorAlphabetFactory.GetRandomsForPassphrase("test", CryptType.Encryption);

            Assert.AreEqual(8, randomsList.Count);

            foreach (var random in randomsList)
            {
                Assert.AreEqual(1, randomsList.Where(x => x == random).Count());
            }
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

            List<int> seedsList = BetorAlphabetFactory.GetRandomSeedsFromByteArray(array);

            Assert.AreEqual(8, seedsList.Count);

            foreach (var seed in seedsList)
            {
                Assert.AreEqual(1, seedsList.Where(x => x == seed).Count());
            }
        }
        
    }
}