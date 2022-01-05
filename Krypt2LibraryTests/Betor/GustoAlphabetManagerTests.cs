using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class GustoAlphabetManagerTests
    {
        private readonly string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";

        [DataRow("Hallo", "")]
        [DataRow("Reën! Dagsê.", "êë")]
        [DataRow("👩🏽‍🚒 Testing some Grapheme clusters. á  á", "👩🏽‍🚒á")]
        [TestMethod()]
        public void InitializeAlphabetForEncryptionTest(string message, string expectedAdded)
        {
            var azMgr = new GustoAlphabetManager();

            (List<object> alphabet, List<object> added) = azMgr.InitializeAlphabet(CryptType.Encryption, message);

            var expectedNewAlphabet = GustoAlphabetManager.StringToListOfObjects(_standardAlphabet + expectedAdded);
            var expectedNewAdded = GustoAlphabetManager.StringToListOfObjects(expectedAdded);

            Assert.AreEqual(expectedNewAlphabet.Count, alphabet.Count);
            Assert.AreEqual(expectedNewAdded.Count, added.Count);

            for (int i = 0; i < expectedNewAlphabet.Count; i++)
            {
                Assert.AreEqual(expectedNewAlphabet[i], alphabet[i]);
            }

            for (int i = 0; i < expectedNewAdded.Count; i++)
            {
                Assert.AreEqual(expectedNewAdded[i], added[i]);
            }
        }

        [DataRow("Hallo", "")]
        [DataRow("êDagsê", "ê")]
        [DataRow("👩🏽‍🚒á Testing some Grapheme clusters. á  á", "👩🏽‍🚒á")]
        [TestMethod()]
        public void InitializeAlphabetForDecryptionTest(string cipherText, string expectedAdded)
        {
            var azMgr = new GustoAlphabetManager();

            (List<object> alphabet, List<object> added) = azMgr.InitializeAlphabet(CryptType.Decryption, cipherText);

            var expectedNewAlphabet = GustoAlphabetManager.StringToListOfObjects(_standardAlphabet + expectedAdded);
            var expectedNewAdded = GustoAlphabetManager.StringToListOfObjects(expectedAdded);

            Assert.AreEqual(expectedNewAlphabet.Count, alphabet.Count);
            Assert.AreEqual(expectedNewAdded.Count, added.Count);

            for (int i = 0; i < expectedNewAlphabet.Count; i++)
            {
                Assert.AreEqual(expectedNewAlphabet[i], alphabet[i]);
            }

            for (int i = 0; i < expectedNewAdded.Count; i++)
            {
                Assert.AreEqual(expectedNewAdded[i], added[i]);
            }
        }

        [TestMethod()]
        public void StringToListOfObjectsTest()
        {
            var input = "abcde👩🏽‍🚒";

            var expected = new List<object>()
            {
                "a",
                "b",
                "c",
                "d",
                "e",
                "👩🏽‍🚒"
            };

            List<object> test = GustoAlphabetManager.StringToListOfObjects(input);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], test[i]);
            }
        }
    }
}