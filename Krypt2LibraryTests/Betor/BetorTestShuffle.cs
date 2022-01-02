using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class BetorTestShuffle
    {
        // Temporarily commented out to save time on tests. Shuffled Alphabet is on ice for the moment.
        //[DataRow("Therefore God has highly exalted him and bestowed on him the name that is above every name, so that at the name of Jesus every knee should bow, in heaven and on earth and under the earth, and every tongue confess that Jesus Christ is Lord, to the glory of God the Father.",
        //    "JFR.juIpyMywZmmt#ypg(YkDA:!OFMdAMH%8?j?n \"aDyOb4Y+9q qfGc*(pg@JeIdP6sN5eg!t;ZZdfG:vrN5pnha$QL%0pJ:I$tHt4QoM(YG@.%-08yq*A0(q3O,rNy17@(NcavWUzCD5l;e8#b;Jz6(&4bD%+7,C#1qWzjFSO5*hq@e#**'*7mSG;0@hv.*6-dxRa8!lvDfCb%E!jOKty-yrr.GyVs;NwviiO-6%0g,-1KrmL0pHM@HZgwz(Mue!;l#RoZxg8gB'")]
        [DataRow("Output of Encrypt(♫♪) should not change",
            "♪♫%Su3:d1*oe#2bZuDKJ!$ *Z;8@♪aj♫Yr@\"?WGi:")]
        [TestMethod()]
        public void EncryptTestForConsistentOutputUsingShuffledAlphabet(string message, string expectedCipherText)
        {
            var betor = new Betor(CharacterSwapMethod.Shuffle);
            var passphrase = "HereWeTest";

            var cipherText = betor.Encrypt(passphrase, message, null);

            Assert.AreEqual(expectedCipherText, cipherText);
        }

        // Temporarily commented out to save time on tests. Shuffled Alphabet is on ice for the moment.
        //[DataRow("[1] PAULUS, ’n gevangene van Christus Jesus, en die broeder Timótheüs, aan Filémon, die geliefde en ons mede-arbeider, [2] en aan Áppia, die geliefde suster, en aan Archíppus, ons medestryder, en aan die gemeente wat in jou huis is: [3] Genade vir julle en vrede van God onse Vader en die Here Jesus Christus!")]
        //[DataRow("En effet, je n’ai pas honte de l’Evangile [de Christ]: c’est la puissance de Dieu pour le salut de tout homme qui croit, du Juif d’abord, mais aussi du non-Juif. En effet, c’est l’Evangile qui révèle la justice de Dieu par la foi et pour la foi, comme cela est écrit: Le juste vivra par la foi.")]
        //[DataRow("Κύριος Ἰησοῦς")]
        //[DataRow("   ")]
        //[DataRow("")]
        //[DataRow("♪")]
        //[DataRow("Decrypted text should exactly match the original text")]
        [DataRow("Testing123")]
        [TestMethod()]
        public void EncryptDecryptUsingShuffledAlphabetTest(string message)
        {
            var betor = new Betor(CharacterSwapMethod.Shuffle);

            var passphrase = "HereWeAlwaysTestBecauseWeDistrustOurGuesses";

            var cipherText = betor.Encrypt(passphrase, message, null);

            var decryptedText = betor.Decrypt(passphrase, cipherText, null);

            Assert.AreEqual(message, decryptedText);
        }
    }
}