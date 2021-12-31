using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class BetorTests
    {
        [DataRow("Output of Encrypt(♫♪) should not change if the code changes, because that will break backwards compatibility.",
            "♫♪%Su3:d1*oe#2bZuDKJyy *Z;8@♫aj♪Yr@\"?WGi:,8DW!uWer$Qe*2X1DFBiS:Qi.♫O8%;I5 .T 'cr$e$3%d)UR*u2O\" 8;7J3R3K:a$+%emw")]
        [DataRow("Therefore God has highly exalted him and bestowed on him the name that is above every name, so that at the name of Jesus every knee should bow, in heaven and on earth and under the earth, and every tongue confess that Jesus Christ is Lord, to the glory of God the Father.",
            "JFR.juIpyMywZmmt#ypg(YkDA:!OFMdAMH%8?j?n \"aDyOb4Y+9q qfGc*(pg@JeIdP6sN5eg!t;ZZdfG:vrN5pnha$QL%0pJ:I$tHt4QoM(YG@.%-08yq*A0(q3O,rNy17@(NcavWUzCD5l;e8#b;Jz6(&4bD%+7,C#1qWzjFSO5*hq@e#**'*7mSG;0@hv.*6-dxRa8!lvDfCb%E!jOKty-yrr.GyVs;NwviiO-6%0g,-1KrmL0pHM@HZgwz(Mue!;l#RoZxg8gB'")]
        [TestMethod()]
        public void EncryptTestForConsistentOutput(string message, string expectedCipherText)
        {
            var betor = new Betor();
            var passphrase = "HereWeTest";

            var cipherText = betor.Encrypt(passphrase, message, null);

            Assert.AreEqual(expectedCipherText, cipherText);
        }

        [DataRow("If we encrypt text and then decrypt that cipherText, the decrypted text should exactly match the original text")]
        [DataRow("En effet, je n’ai pas honte de l’Evangile [de Christ]: c’est la puissance de Dieu pour le salut de tout homme qui croit, du Juif d’abord, mais aussi du non-Juif. En effet, c’est l’Evangile qui révèle la justice de Dieu par la foi et pour la foi, comme cela est écrit: Le juste vivra par la foi.")]
        [DataRow("Welgeluksalig is die man wat nie wandel in die raad van die goddelose en nie staan op die weg van die sondaars en nie sit in die kring van die spotters nie; maar sy behae is in die wet van die HERE, en hy oordink sy wet dag en nag. En hy sal wees soos ’n boom wat geplant is by waterstrome, wat sy vrugte gee op sy tyd en waarvan die blare nie verwelk nie; en alles wat hy doen, voer hy voorspoedig uit. So is die goddelose mense nie, maar soos kaf wat die wind verstrooi. Daarom sal die goddelose nie bestaan in die oordeel en die sondaars in die vergadering van die regverdiges nie. Want die HERE ken die weg van die regverdiges, maar die weg van die goddelose sal vergaan.")]
        [DataRow("Κύριος Ἰησοῦς")]
        [DataRow("   ")]
        [DataRow("")]
        [DataRow("♪")]
        [TestMethod()]
        public void EncryptDecryptTest(string message)
        {
            var betor = new Betor();

            var passphrase = "HereWeAlwaysTestBecauseWeDistrustOurGuesses";

            var cipherText = betor.Encrypt(passphrase, message, null);

            var decryptedText = betor.Decrypt(passphrase, cipherText, null);

            Assert.AreEqual(message, decryptedText);
        }

        [TestMethod()]
        public void InvalidCipherTextTest()
        {
            // ToDo: Let it test for throwing of exception...
            // Also: Reconsider how IndexOutOfrange is handled. Instead of handling that exception, perhaps check for index of [-1] and handle it ourselves instead of handling an exception.
            
            var betor = new Betor();

            var passphrase = "InvalidCipherTextShouldNotTHrowAnException";

            var cipherText = "ghgh~+";

            Assert.ThrowsException<InvalidCipherException>(() => betor.Decrypt(passphrase, cipherText, null));

        }
    }
}