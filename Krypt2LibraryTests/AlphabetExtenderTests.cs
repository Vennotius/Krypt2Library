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
    public class AlphabetExtenderTests
    {
        private readonly string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";
        
        [TestMethod()]
        public void ExtendAlphabetIfNeededForEncryptionTestShouldNotAddAnything()
        {
            var alphabet = _standardAlphabet;
            var message = "Testing. This should not add anything.";
            
            var output = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Encryption);

            Assert.AreEqual(alphabet, output.outputExtendedAlphabet);
            Assert.AreEqual("", output.addedCharacters);
            Assert.AreEqual(0, output.messageStartIndex);
        }

        [TestMethod()]
        public void ExtendAlphabetIfNeededForEncryptionTestShouldAddOneCharacter()
        {
            var alphabet = _standardAlphabet;
            var message = "Testing. This should add \n";

            var output = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Encryption);

            Assert.AreEqual(alphabet + "\n", output.outputExtendedAlphabet);
            Assert.AreEqual("\n", output.addedCharacters);
            Assert.AreEqual(0, output.messageStartIndex);
        }
        
        [TestMethod()]
        public void ExtendAlphabetIfNeededForEncryptionTestShouldAddTwoCharacters()
        {
            var alphabet = _standardAlphabet;
            var message = "Testing. This should add 'ê' as well as 'ë'";

            var output = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Encryption);

            Assert.AreEqual(alphabet + "êë", output.outputExtendedAlphabet);
            Assert.AreEqual("êë", output.addedCharacters);
            Assert.AreEqual(0, output.messageStartIndex);
        }


        [TestMethod()]
        public void ExtendAlphabetIfNeededForDecryptionTestShouldNotAddAnything()
        {
            var alphabet = _standardAlphabet;
            var message = " ku rg ,ksel s3 trwl3 w sw3t 3'";

            var output = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Decryption);

            Assert.AreEqual(alphabet, output.outputExtendedAlphabet);
            Assert.AreEqual("", output.addedCharacters);
            Assert.AreEqual(0, output.messageStartIndex);
        }

        [TestMethod()]
        public void ExtendAlphabetIfNeededForDecryptionTestShouldAddOneCharacter()
        {
            var alphabet = _standardAlphabet;
            var message = "\nabcde";

            var output = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Decryption);

            Assert.AreEqual(alphabet + "\n", output.outputExtendedAlphabet);
            Assert.AreEqual("\n", output.addedCharacters);
            Assert.AreEqual(1, output.messageStartIndex);
        }

        [TestMethod()]
        public void ExtendAlphabetIfNeededForDecryptionTestShouldAddTwoCharacters()
        {
            var alphabet = _standardAlphabet;
            var message = "ë\nabcde";

            var output = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Decryption);

            Assert.AreEqual(alphabet + "ë\n", output.outputExtendedAlphabet);
            Assert.AreEqual("ë\n", output.addedCharacters);
            Assert.AreEqual(2, output.messageStartIndex);
        }
    }
}