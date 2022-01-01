using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("Krypt2LibraryTests")]
namespace Krypt2Library
{
    public class BetorAlphabetFactory
    {
        public string Alphabet { get; set; }
        public string Added { get; set; }
        public int MessageStartIndex { get; private set; }

        private List<Random> _randoms;
        private readonly string _standardAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .,?!\"':;()@#$%&*+-";
        private readonly List<char> _alphabetList;
        private readonly string _passphrase;
        private readonly CryptType _cryptType;


        public BetorAlphabetFactory(string passphrase, string message, CryptType cryptType)
        {
            _passphrase = passphrase;
            _cryptType = cryptType;
            _randoms = GetRandomsForPassphrase(_passphrase, _cryptType);

            (Alphabet, Added, MessageStartIndex) = AlphabetExtender.ExtendAlphabetIfNeeded(_standardAlphabet, message, cryptType);

            _alphabetList = Alphabet.ToList();
        }

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

        public void Reset()
        {
            _randoms = GetRandomsForPassphrase(_passphrase, _cryptType);
        }

        public List<char> GetAlphabetForNextCharacter(int randomIndex)
        {
            var output = new List<char>(_alphabetList);

            ShuffleAlphabet<char>(output, randomIndex);

            return output;
        }

        public void ShuffleAlphabet<T>(List<T> list, int randomIndex)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = _randoms[randomIndex].Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }
    }
}