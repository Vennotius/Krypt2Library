using System.Security.Cryptography;
using System.Text;

namespace Krypt2Library
{
    internal static class RandomsFactory
    {
        internal static List<IRandom> GetRandomsFromPassphrase(string passphrase, CryptType cryptType)
        {
            List<IRandom> output = new();

            // Use Hash Array to extract seeds for created Randoms
            {
                byte[] hashArray = SHA512.HashData(Encoding.UTF8.GetBytes(passphrase)); // 64 bytes
                var seeds = GetInt32SeedsFromByteArray(hashArray).ToArray(); // 4 seeds

                output.Add(new Xoshiro256SS(seeds));
            }

            return output;
        }

        internal static List<ulong> GetInt32SeedsFromByteArray(byte[] hashArray)
        {
            if (hashArray.Length < 32)
            {
                throw new ArgumentException("Byte array must be at least 32 bytes long.");
            }

            List<ulong> output = new();

            // Loop to extract 4 ulong seeds from 32-byte array
            for (int i = 0; i < 32; i += 8)
            {
                ulong seed = (ulong)hashArray[i] |
                             ((ulong)hashArray[i + 1] << 8) |
                             ((ulong)hashArray[i + 2] << 16) |
                             ((ulong)hashArray[i + 3] << 24) |
                             ((ulong)hashArray[i + 4] << 32) |
                             ((ulong)hashArray[i + 5] << 40) |
                             ((ulong)hashArray[i + 6] << 48) |
                             ((ulong)hashArray[i + 7] << 56);

                output.Add(seed);
            }

            return output;
        }

    }
}