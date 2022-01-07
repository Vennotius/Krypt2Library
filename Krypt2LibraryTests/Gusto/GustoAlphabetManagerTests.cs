using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            var alphabet = GustoAlphabetManager.InitializeAlphabet(CryptType.Encryption, message);

            var expectedNewAlphabet = GustoAlphabetManager.StringToListOfObjects(_standardAlphabet + expectedAdded);
            var expectedNewAdded = GustoAlphabetManager.StringToListOfObjects(expectedAdded);

            Assert.AreEqual(expectedNewAlphabet.Count, alphabet.AllCharacters.Count);
            Assert.AreEqual(expectedNewAdded.Count, alphabet.AddedCharacters.Count);

            for (int i = 0; i < expectedNewAlphabet.Count; i++)
            {
                Assert.AreEqual(expectedNewAlphabet[i], alphabet.AllCharacters[i]);
            }

            for (int i = 0; i < expectedNewAdded.Count; i++)
            {
                Assert.AreEqual(expectedNewAdded[i], alphabet.AddedCharacters[i]);
            }
        }

        [DataRow("Hallo", "")]
        [DataRow("êêDagsê", "ê")]
        [DataRow("👩🏽‍🚒á Testing some Grapheme clusters. á  á", "👩🏽‍🚒á")]
        [TestMethod()]
        public void InitializeAlphabetForDecryptionTest(string cipherText, string expectedAdded)
        {
            var alphabet = GustoAlphabetManager.InitializeAlphabet(CryptType.Decryption, cipherText);

            var expectedNewAlphabet = GustoAlphabetManager.StringToListOfObjects(_standardAlphabet + expectedAdded);
            var expectedNewAdded = GustoAlphabetManager.StringToListOfObjects(expectedAdded);

            Assert.AreEqual(expectedNewAlphabet.Count, alphabet.AllCharacters.Count);
            Assert.AreEqual(expectedNewAdded.Count, alphabet.AddedCharacters.Count);

            for (int i = 0; i < expectedNewAlphabet.Count; i++)
            {
                Assert.AreEqual(expectedNewAlphabet[i], alphabet.AllCharacters[i]);
            }

            for (int i = 0; i < expectedNewAdded.Count; i++)
            {
                Assert.AreEqual(expectedNewAdded[i], alphabet.AddedCharacters[i]);
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

        [TestMethod()]
        public void InvalidCipherTextTest()
        {
            var cipherText = "ghgh~♫+";
            
            Assert.ThrowsException<InvalidCipherException>(() => GustoAlphabetManager.InitializeAlphabet(CryptType.Decryption, cipherText));
        }
    }
}