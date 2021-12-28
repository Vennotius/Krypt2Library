using Microsoft.VisualStudio.TestTools.UnitTesting;
using Krypt2Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class BetorTests
    {
        [TestMethod()]
        public void EncryptTestForConsistentOutput()
        {
            var betor = new Betor();
            var bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            var message = "Output of Encrypt() should not change if the code changes, because that will break backwards compatibility.";
            var passphrase = "HereWeTest";

            var expectedCipherText = "2Qlp2'2*@MK2mGu:NMPrNY1UOIiS,MNuOkT2nc,g tExBPDz?+C1U#D.VyEzU:JF5)IYT#UX)Zik, jN8)@vmnDulZY6jpyQ':Ifdg;JTjX";

            var cipherText = betor.Encrypt(passphrase, message, bgWorker);

            Assert.AreEqual(expectedCipherText, cipherText);
        }

        [TestMethod()]
        public void EncryptDecryptTest()
        {
            var betor = new Betor();
            var bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            var message = "If we encrypt text and then decrypt that cipherText, the decrypted text should exactly match the original text";
            var passphrase = "HereWeAlwaysTest";
            var cipherText = betor.Encrypt(passphrase, message, bgWorker);
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            var decryptedText = betor.Decrypt(passphrase, cipherText, bgWorker);

            Assert.AreEqual(message, decryptedText);
        }
    }
}