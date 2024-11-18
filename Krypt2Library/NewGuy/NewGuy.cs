using System.Security.Cryptography;
using System.Text;

namespace Krypt2Library
{
    public class NewGuy : ICipher
    {
        //#error Ek dink ons moet newlines uit die ciphertext hou. ONs moet 'n ander karacter insit wat altyd vervang word,
        // Dalk moet ons eerder verskillende alfabette hê, saam met 'n flag of ons newlines moet insluit of nie.
        // - gewone latynse karakters groot en klein (werk vir Engels), syfers, basic simbole
        // - gewone latynse karakters groot en klein (werk vir Engels), syfers, uitgebreide simbole
        // - gewone latynse karakters groot en klein (werk vir Engels), syfers, basic simbole
        // Hmm, nee, weet jy wat. Ek het lank gelede al hieroor gedink, en as dit gaan oor security, kan die user ook maar verandwoordelikheid neem en 'n paar onnodige karakters ingooi.
        // Ek wil nie onnodig baie karakters in die ciphertext hê nie, want dalk moet dit geprint of met die hand geskryf word, en dan is die lees inlees daarvan moeiliker.
        // As dit net gecopy en gepaste word, of veral as dit van en na files werk, dan maak dit moontlik niks saak nie.
        // Ek probeer regtig die analog toepassings beskerm, WANT EK GLO DIS BELANGRIK, en dit is in elk geval die hoof toepassing wat ek envision.

        // Verder, as dit oor analog toepassing gaan, kan ons bv:
        // - remove enige whitespace van die ciphertext (kry 'n vervang karakter daarvoor of iets)
        // - output cipher text in blokke van 4 karakters, en daardie weer gegroepeer in maklik leesbare hoeveelhede

        // Verder, weereens @ analog toepassing:
        // - Kan 'n mens nie 'n hand-vriendelike weergawe van die hash en die PRNG maak nie
        // - Dan is dit iets wat 'n mens met die hand kan gebruik as dit moet.

        private static readonly string StandardAlphabetString =
            " !\"#$%&'()*+,-./\\0123456789:;=?_~@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]<>^`abcdefghijklmnopqrstuvwxyz{|}ÆÀÁÂÄÇÈÉÊËÌÍÎÏÒÓÔÖÙÚÛÜÝŸÑŒßæàáâäçèéêëìíîïòóôöùúûüýÿñœ\n";

        private static readonly List<Rune> StandardAlphabet = StandardAlphabetString.EnumerateRunes().Distinct().ToList();

        public string Encrypt(string passphrase, string plaintext)
        {
            var plaintextRunes = plaintext.EnumerateRunes().ToList();

            var runesNotInStandardAlphabet = plaintextRunes
                .Where(r => !StandardAlphabet.Contains(r))
                .Distinct()
                .OrderBy(r => r.Value)
                .ToList();

            var extendedAlphabet = runesNotInStandardAlphabet.Concat(StandardAlphabet).ToList();

            // Map runes to indices for quick lookup
            var runeToIndex = extendedAlphabet
                .Select((rune, index) => new { rune, index })
                .ToDictionary(x => x.rune, x => x.index);

            var random = GetPrngFromPassphrase(passphrase);
            var ciphertextBuilder = new StringBuilder();

            // Encrypt each rune
            foreach (var rune in plaintextRunes)
            {
                if (!runeToIndex.TryGetValue(rune, out int index))
                    throw new ArgumentException($"Rune '{rune}' not in the extended alphabet.");

                int shift = random.NextInt32(extendedAlphabet.Count);
                var shiftedRune = extendedAlphabet[(index + shift) % extendedAlphabet.Count];
                ciphertextBuilder.Append(shiftedRune);
            }

            // Prepend extra runes to ciphertext
            string extraPrefix = string.Concat(runesNotInStandardAlphabet);
            return extraPrefix + ciphertextBuilder.ToString();
        }

        public string Decrypt(string passphrase, string ciphertext)
        {
            var ciphertextRunes = ciphertext.EnumerateRunes().ToList();

            // Extract extra runes from the prefix
            int prefixLength = 0;
            while (prefixLength < ciphertextRunes.Count && !StandardAlphabet.Contains(ciphertextRunes[prefixLength]))
                prefixLength++;

            var runesNotInStandardAlphabet = ciphertextRunes.Take(prefixLength).ToList();

            var extendedAlphabet = runesNotInStandardAlphabet.Concat(StandardAlphabet).ToList();

            // Map runes to indices for quick lookup
            var runeToIndex = extendedAlphabet
                .Select((rune, index) => new { rune, index })
                .ToDictionary(x => x.rune, x => x.index);

            // Initialize PRNG
            var random = GetPrngFromPassphrase(passphrase);

            var plaintextBuilder = new StringBuilder();

            // Decrypt each rune
            foreach (var rune in ciphertextRunes.Skip(prefixLength))
            {
                if (!runeToIndex.TryGetValue(rune, out int index))
                    throw new ArgumentException($"Rune '{rune}' not in the extended alphabet.");

                int shift = random.NextInt32(extendedAlphabet.Count);
                int originalIndex = (index - shift + extendedAlphabet.Count) % extendedAlphabet.Count;
                var originalRune = extendedAlphabet[originalIndex];
                plaintextBuilder.Append(originalRune);
            }

            return plaintextBuilder.ToString();
        }

        private Xoshiro256SS GetPrngFromPassphrase(string passphrase)
        {
            byte[] hashArray = SHA512.HashData(Encoding.UTF8.GetBytes(passphrase));
            var seeds = GetUInt64SeedsFromHash(hashArray);

            return new Xoshiro256SS(seeds[0], seeds[1], seeds[2], seeds[3]);
        }

        private List<ulong> GetUInt64SeedsFromHash(byte[] hashArray)
        {
            if (hashArray.Length < 64)
                throw new ArgumentException("Hash array must be at least 64 bytes long.");

            var seeds = new List<ulong>();

            // Extract 4 ulong seeds from the 64-byte hash
            for (int i = 0; i < 32; i += 8)
            {
                ulong seed = BitConverter.ToUInt64(hashArray, i);
                seeds.Add(seed);
            }

            return seeds;
        }

        private class Xoshiro256SS
        {
            private ulong s0, s1, s2, s3;

            public Xoshiro256SS(ulong seed0, ulong seed1, ulong seed2, ulong seed3)
            {
                s0 = seed0;
                s1 = seed1;
                s2 = seed2;
                s3 = seed3;
            }

            public int NextInt32(int maxExclusive) 
                => (int)(NextUInt64() % (ulong)maxExclusive);

            private ulong NextUInt64()
            {
                ulong result = RotateLeft(s1 * 5, 7) * 9;

                ulong t = s1 << 17;

                s2 ^= s0;
                s3 ^= s1;
                s1 ^= s2;
                s0 ^= s3;

                s2 ^= t;
                s3 = RotateLeft(s3, 45);

                return result;
            }

            private static ulong RotateLeft(ulong x, int k) 
                => (x << k) | (x >> (64 - k));
        }
    }
}
