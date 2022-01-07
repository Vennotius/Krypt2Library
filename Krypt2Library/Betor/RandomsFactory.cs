using System.Security.Cryptography;
using System.Text;

namespace Krypt2Library
{
    internal static class RandomsFactory
    {
        internal static List<Random> GetRandomsForPassphrase(string passphrase, CryptType cryptType)
        {
            var output = new List<Random>();

            foreach (var seed in GetRandomSeedsFromPassphrase(passphrase))
            {
                output.Add(new OurRandom(seed));
            }

            if (cryptType == CryptType.Decryption) output.Reverse();

            return output;
        }

        private static List<int> GetRandomSeedsFromPassphrase(string passphrase)
        {
            byte[] hashArray = GetHashByteArray(passphrase);

            List<int> randomSeeds = GetInt32SeedsFromByteArray(hashArray);

            return randomSeeds;
        }

        internal static List<int> GetInt32SeedsFromByteArray(byte[] hashArray)
        {
            var output = new List<int>();

            for (int i = 0; i < 32; i += 4)
            {
                int seed = hashArray[i] +
                          (hashArray[i + 1] << 8) +
                          (hashArray[i + 2] << 16) +
                          (hashArray[i + 3] << 24);

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