using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            var (outputExtendedAlphabet, addedCharacters, messageStartIndex)
                = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Encryption);

            Assert.AreEqual(alphabet, outputExtendedAlphabet);
            Assert.AreEqual("", addedCharacters);
            Assert.AreEqual(0, messageStartIndex);
        }

        [TestMethod()]
        public void ExtendAlphabetIfNeededForEncryptionTestShouldAddOneCharacter()
        {
            var alphabet = _standardAlphabet;
            var message = "Testing. This should add \n";

            var (outputExtendedAlphabet, addedCharacters, messageStartIndex)
                = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Encryption);

            Assert.AreEqual(alphabet + "\n", outputExtendedAlphabet);
            Assert.AreEqual("\n", addedCharacters);
            Assert.AreEqual(0, messageStartIndex);
        }

        [TestMethod()]
        public void ExtendAlphabetIfNeededForEncryptionTestShouldAddTwoCharacters()
        {
            var alphabet = _standardAlphabet;
            var message = "Testing. This should add 'ê' as well as 'ë'";

            var (outputExtendedAlphabet, addedCharacters, messageStartIndex)
                = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Encryption);

            Assert.AreEqual(alphabet + "êë", outputExtendedAlphabet);
            Assert.AreEqual("êë", addedCharacters);
            Assert.AreEqual(0, messageStartIndex);
        }


        [TestMethod()]
        public void ExtendAlphabetIfNeededForDecryptionTestShouldNotAddAnything()
        {
            var alphabet = _standardAlphabet;
            var message = " ku rg ,ksel s3 trwl3 w sw3t 3'";

            var (outputExtendedAlphabet, addedCharacters, messageStartIndex)
                = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Decryption);

            Assert.AreEqual(alphabet, outputExtendedAlphabet);
            Assert.AreEqual("", addedCharacters);
            Assert.AreEqual(0, messageStartIndex);
        }

        [TestMethod()]
        public void ExtendAlphabetIfNeededForDecryptionTestShouldAddOneCharacter()
        {
            var alphabet = _standardAlphabet;
            var message = "\nabcde";

            var (outputExtendedAlphabet, addedCharacters, messageStartIndex)
                = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Decryption);

            Assert.AreEqual(alphabet + "\n", outputExtendedAlphabet);
            Assert.AreEqual("\n", addedCharacters);
            Assert.AreEqual(1, messageStartIndex);
        }

        [TestMethod()]
        public void ExtendAlphabetIfNeededForDecryptionTestShouldAddTwoCharacters()
        {
            var alphabet = _standardAlphabet;
            var message = "ë\nabcde";

            var (outputExtendedAlphabet, addedCharacters, messageStartIndex)
                = AlphabetExtender.ExtendAlphabetIfNeeded(alphabet, message, CryptType.Decryption);

            Assert.AreEqual(alphabet + "ë\n", outputExtendedAlphabet);
            Assert.AreEqual("ë\n", addedCharacters);
            Assert.AreEqual(2, messageStartIndex);
        }
    }
}