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
    public class AlphabetFactoryTests
    {
        //[TestMethod()]
        //public void PassphraseHasherTest()
        //{
        //    var testStrings = GetTestStrings();
        //    var hashes = new Dictionary<int, string>();

        //    foreach (var testString in testStrings)
        //    {
        //        var hash = AlphabetFactory.GetOneEigthOfSHA256Hash(testString);
        //        Assert.IsFalse(hashes.ContainsKey(hash));
        //        hashes.Add(hash, testString);
        //    }
        //}

        //[TestMethod()]
        //public void AttemptSomeKindOfConversionToInt32Test()
        //{
        //    // Checks for collisions. Takes a relatively long time. Checked and found no collisions in 268,435,456 possibilities. (128 * 128 * 128 * 128)

        //    var hashes = new Dictionary<int, bool>();

        //    for (byte a = 0; a < 128; a++)
        //    {
        //        for (byte b = 0; b < 128; b++)
        //        {
        //            for (byte c = 0; c < 128; c++)
        //            {
        //                for (byte d = 0; d < 128; d++)
        //                {
        //                    var array = new byte[4] { d, c, b, a };
        //                    var hash = AlphabetFactory.AttemptSomeKindOfConversionToInt32(0, array);
        //                    Assert.IsFalse(hashes.ContainsKey(hash));
        //                    hashes.Add(hash, true);
        //                }
        //            }
        //        }
        //    }
        //}

        private List<string> GetTestStrings()
        {
            var output = new List<string>();

            var alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";

            foreach (char c1 in alphabet)
            {
                foreach (char c2 in alphabet)
                {
                    foreach (char c3 in alphabet)
                    {
                        output.Add($"{c1}{c2}{c3}");
                    }
                }
            }

            return output;
        }
    }
}