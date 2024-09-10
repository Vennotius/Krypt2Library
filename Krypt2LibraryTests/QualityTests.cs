using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Krypt2Library.Tests
{
    [TestClass()]
    public class QualityTests
    {
        private const string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";

        [DataRow("Short string")]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [DataRow("123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123123")]
        [DataRow("If we encrypt text and then decrypt that cipherText, the decrypted text should exactly match the original text")]
        [DataRow("En effet, je n’ai pas honte de l’Evangile [de Christ]: c’est la puissance de Dieu pour le salut de tout homme qui croit, du Juif d’abord, mais aussi du non-Juif. En effet, c’est l’Evangile qui révèle la justice de Dieu par la foi et pour la foi, comme cela est écrit: Le juste vivra par la foi. En effet, je n’ai pas honte de l’Evangile [de Christ]: c’est la puissance de Dieu pour le salut de tout homme qui croit, du Juif d’abord, mais aussi du non-Juif. En effet, c’est l’Evangile qui révèle la justice de Dieu par la foi et pour la foi, comme cela est écrit: Le juste vivra par la foi. En effet, je n’ai pas honte de l’Evangile [de Christ]: c’est la puissance de Dieu pour le salut de tout homme qui croit, du Juif d’abord, mais aussi du non-Juif. En effet, c’est l’Evangile qui révèle la justice de Dieu par la foi et pour la foi, comme cela est écrit: Le juste vivra par la foi. En effet, je n’ai pas honte de l’Evangile [de Christ]: c’est la puissance de Dieu pour le salut de tout homme qui croit, du Juif d’abord, mais aussi du non-Juif. En effet, c’est l’Evangile qui révèle la justice de Dieu par la foi et pour la foi, comme cela est écrit: Le juste vivra par la foi.")]
        [DataRow("Welgeluksalig is die man wat nie wandel in die raad van die goddelose en nie staan op die weg van die sondaars en nie sit in die kring van die spotters nie; maar sy behae is in die wet van die HERE, en hy oordink sy wet dag en nag. En hy sal wees soos ’n boom wat geplant is by waterstrome, wat sy vrugte gee op sy tyd en waarvan die blare nie verwelk nie; en alles wat hy doen, voer hy voorspoedig uit. So is die goddelose mense nie, maar soos kaf wat die wind verstrooi. Daarom sal die goddelose nie bestaan in die oordeel en die sondaars in die vergadering van die regverdiges nie. Want die HERE ken die weg van die regverdiges, maar die weg van die goddelose sal vergaan.")]
        [TestMethod()]
        public void CompressionTest(string message)
        {
            // I cannot remember the name for it, but this test looks at relative compression rates.
            // A pseudorandom string does not compress well in comparison to normal English text for example.
            // Therefore, if our cipher text compresses worse than plain text, it is at least something.
            // But if it compresses worse than a pseudorandom string, this means that there is some information to be gleaned form the cipher text.
            // This tests is our cipherText compresses at least as badly as a pseudorandom string does. (with a 1% tolerance.)

            var kryptor = new Kryptor<Gusto>();

            var encrypted = kryptor.Encrypt("compressionTest", message);
            var randomstring = GenerateRandomString(message.Length, _standardAlphabet);

            byte[] enc = Zip(encrypted);
            byte[] rnd = Zip(randomstring);

            Assert.IsTrue(enc.Length >= rnd.Length * 0.99);
        }

        private static string GenerateRandomString(int length, string standardAlphabet)
        {
            var sb = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(standardAlphabet[random.Next(standardAlphabet.Length - 1)]);
            }

            return sb.ToString();
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using var gs = new GZipStream(mso, CompressionMode.Compress);

            CopyTo(msi, gs);

            return mso.ToArray();
        }
    }
}