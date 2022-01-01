﻿using System.ComponentModel;
using System.Text;

namespace Krypt2Library
{
    public class Betor : ICipher
    {
        private readonly Func<char, int, BetorAlphabetFactory, char> encryptCharacter;
        private readonly Func<char, int, BetorAlphabetFactory, char> decryptCharacter;

        private BetorAlphabetFactory AlphabetFactory { get; set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Betor(CharacterSwapMethod swapMethod)
        {
            switch (swapMethod)
            {
                case CharacterSwapMethod.Shuffle:
                    this.encryptCharacter = EncryptCharacterUsingShuffledAlphabet;
                    this.decryptCharacter = DecryptCharacterUsingShuffledAlphabet;
                    break;
                case CharacterSwapMethod.Shift:
                    this.encryptCharacter = EncryptCharacterUsingShift;
                    this.decryptCharacter = DecryptCharacterUsingShift;
                    break;
                default:
                    this.encryptCharacter = EncryptCharacterUsingShift;
                    this.decryptCharacter = DecryptCharacterUsingShift;
                    break;
            }
        }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string Encrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            AlphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Encryption);

            PrependAdditionalAlphabetCharacters(output);

            EncryptMessage(message, backgroundWorker, output);

            return output.ToString();
        }
        private void EncryptMessage(string message, BackgroundWorker backgroundWorker, StringBuilder output)
        {
            for (int passIndex = 0; passIndex < 8; passIndex++)
            {
                message = EncryptOnePass(message, passIndex, backgroundWorker);
            }

            output.Append(message);

            AlphabetFactory.Reset();
        }
        private string EncryptOnePass(string message, int passIndex, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            var currentCharacterIndex = message.Length * passIndex;
            
            var totalCharactersToProcess = (double)message.Length * 8;
            var onePercentOfTotal = totalCharactersToProcess / 100;
            var currentPercent = (currentCharacterIndex / onePercentOfTotal);

            for (int i = 0; i < message.Length; i++)
            {
                output.Append(encryptCharacter(message[i], passIndex, AlphabetFactory));
                
                currentCharacterIndex++;
                
                currentPercent = ReportProgress(backgroundWorker, currentCharacterIndex, totalCharactersToProcess, onePercentOfTotal, currentPercent);
            }

            return output.ToString();
        }
        private static char EncryptCharacterUsingShuffledAlphabet(char c, int passIndex, BetorAlphabetFactory alphabetFactory)
        {
            var index = alphabetFactory.Alphabet.IndexOf(c);
            var cipherAlphabet = alphabetFactory.GetAlphabetForNextCharacter(passIndex);

            return cipherAlphabet[index];
        }

        private static char EncryptCharacterUsingShift(char c, int passIndex, BetorAlphabetFactory alphabetFactory)
        {
            var inputIndex = alphabetFactory.Alphabet.IndexOf(c);
            var outputIndex = alphabetFactory.GetShiftAmountForNextCharacter(inputIndex, passIndex, CryptType.Encryption);
            
            return alphabetFactory.Alphabet[outputIndex];
        }

        public string Decrypt(string passphrase, string message, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            AlphabetFactory = new BetorAlphabetFactory(passphrase, message, CryptType.Decryption);
            var startIndex = AlphabetFactory.MessageStartIndex;

            try
            {
                DecryptMessage(message, backgroundWorker, output, startIndex);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidCipherException("Invalid Cipher Text.");
            }

            return output.ToString();
        }
        private void DecryptMessage(string message, BackgroundWorker backgroundWorker, StringBuilder output, int startIndex)
        {
            for (int i = 0; i < 7; i++)
            {
                message = AlphabetFactory.Added + DecryptOnePass(startIndex, message, i, backgroundWorker);
            }

            message = DecryptOnePass(startIndex, message, 7, backgroundWorker); // Don't prepend "added" after the last pass

            output.Append(message);

            AlphabetFactory.Reset();
        }
        private string DecryptOnePass(int startIndex, string message, int passIndex, BackgroundWorker backgroundWorker)
        {
            var output = new StringBuilder();

            var currentCharacterIndex = message.Length * passIndex;

            var totalCharactersToProcess = (double)message.Length * 8;
            var onePercentOfTotal = totalCharactersToProcess / 100;
            var currentPercent = (currentCharacterIndex / onePercentOfTotal);

            for (int i = startIndex; i < message.Length; i++)
            {
                output.Append(decryptCharacter(message[i], passIndex, AlphabetFactory));
                currentCharacterIndex++;

                currentPercent = ReportProgress(backgroundWorker, currentCharacterIndex, totalCharactersToProcess, onePercentOfTotal, currentPercent);
            }

            return output.ToString();
        }
        private static char DecryptCharacterUsingShuffledAlphabet(char c, int passIndex, BetorAlphabetFactory alphabetFactory)
        {
            var index = alphabetFactory.GetAlphabetForNextCharacter(passIndex).IndexOf(c);
            var cipherAlphabet = alphabetFactory.Alphabet;

            return cipherAlphabet[index];
        }

        private static char DecryptCharacterUsingShift(char c, int passIndex, BetorAlphabetFactory alphabetFactory)
        {
            var inputIndex = alphabetFactory.Alphabet.IndexOf(c);
            var outputIndex = alphabetFactory.GetShiftAmountForNextCharacter(inputIndex, passIndex, CryptType.Decryption);

            return alphabetFactory.Alphabet[outputIndex];
        }

        private void PrependAdditionalAlphabetCharacters(StringBuilder output)
        {
            var added = AlphabetFactory.Added;
            output.Append($"{added}");
        }

        private static double ReportProgress(BackgroundWorker backgroundWorker, int currentCharacterIndex, double totalCharactersToProcess, double onePercentOfTotal, double currentPercent)
        {
            if (backgroundWorker == null) return currentPercent;
            
            if (currentCharacterIndex == totalCharactersToProcess)
            {
                backgroundWorker.ReportProgress(100);
            }
            else if (currentCharacterIndex / onePercentOfTotal > currentPercent + 1)
            {
                backgroundWorker.ReportProgress(Convert.ToInt32(currentPercent));
                currentPercent = (currentCharacterIndex / onePercentOfTotal);
            }

            return currentPercent;
        }
    }
}