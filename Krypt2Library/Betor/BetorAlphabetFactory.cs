using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Krypt2LibraryTests")]
namespace Krypt2Library
{
    public class BetorAlphabetFactory
    {
        public string Alphabet { get; private set; }
        public string Added { get; private set; }
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
            _randoms = RandomsFactory.GetRandomsForPassphrase(_passphrase, _cryptType);

            (Alphabet, Added, MessageStartIndex) = AlphabetExtender.ExtendAlphabetIfNeeded(_standardAlphabet, message, cryptType);

            _alphabetList = Alphabet.ToList();
        }

        internal void Reset()
        {
            _randoms = RandomsFactory.GetRandomsForPassphrase(_passphrase, _cryptType);
        }

        internal List<char> GetAlphabetForNextCharacter(int randomIndex)
        {
            var output = new List<char>(_alphabetList);

            ShuffleAlphabet<char>(output, randomIndex);

            return output;
        }
        private void ShuffleAlphabet<T>(List<T> list, int randomIndex)
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

        internal int GetShiftAmountForNextCharacter(int inputIndex, int randomIndex, CryptType cryptType)
        {
            var shiftAmount = _randoms[randomIndex].Next(Alphabet.Length);

            if (cryptType == CryptType.Decryption) shiftAmount *= -1;

            int finalIndex = ShiftWrapper(inputIndex, shiftAmount, Alphabet.Length);


            return finalIndex;
        }
        internal static int ShiftWrapper(int inputIndex, int shiftAmount, int length)
        {
            if (shiftAmount == 0) return inputIndex;

            var output = inputIndex + shiftAmount;

            if (output > 0)
            {
                while (output > length - 1)
                {
                    output -= length;
                }
            }

            if (output < 0)
            {
                while (output < 0)
                {
                    output += length;
                }
            }

            return output;
        }

    }
}