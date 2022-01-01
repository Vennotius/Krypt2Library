using System.Security.Cryptography;
using System.Text;

namespace Krypt2Library
{
    internal static class RandomsFactory
    {
        internal static List<Random> GetRandomsForPassphrase(string passphrase, CryptType cryptType)
        {
            var output = new List<Random>();

            byte[] hashArray = GetHashByteArray(passphrase);

            List<int> randomSeeds = GetRandomSeedsFromByteArray(hashArray);

            foreach (var seed in randomSeeds)
            {
                output.Add(new OurRandom(seed));
            }

            if (cryptType == CryptType.Decryption) output.Reverse();

            return output;
        }

        internal static List<int> GetRandomSeedsFromByteArray(byte[] hashArray)
        {
            var output = new List<int>();

            for (int i = 0; i < 32; i += 4)
            {
                int seed =
                    hashArray[i] +
                    (hashArray[i + 1] * 256) +
                    (hashArray[i + 2] * 256 * 256) +
                    (hashArray[i + 3] * 256 * 256 * 256);

                output.Add(seed);
            }

            return output;
        }

        private static byte[] GetHashByteArray(string passphrase)
        {
            byte[] hash;

            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            }

            return hash;
        }
    }
}