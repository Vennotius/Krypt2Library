using System.Security.Cryptography;
using System.Text;

namespace Krypt2Library
{
    internal static class RandomsFactory
    {
        internal static List<Random> GetRandomsForPassphrase(string passphrase, CryptType cryptType)
        {
            List<Random> output = new();

            // Use Hash Array to extract seeds for created Randoms
            {
                byte[] hashArray = SHA256.HashData(Encoding.UTF8.GetBytes(passphrase));
                List<int> seeds = GetInt32SeedsFromByteArray(hashArray);

                foreach (int seed in seeds)
                {
                    output.Add(new OurRandom(seed));
                }
            }

            if (cryptType == CryptType.Decryption) output.Reverse();

            return output;
        }

        internal static List<int> GetInt32SeedsFromByteArray(byte[] hashArray)
        {
            List<int> output = new();

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
    }
}