using System;
using System.Collections.Generic;
using System.Linq;

namespace WordListGenerator
{
    public static class WordsCreator
    {
        public static IEnumerable<string> CreateAllWords(IReadOnlyCollection<char> allowedSymbols, int minLength, int maxLength)
        {
            if (minLength <= 0)
                throw new ArgumentException(
                    $"Minimal length should be greater than 0 but was {minLength}",
                    nameof(minLength));
            
            if (maxLength < minLength)
                throw new ArgumentException(
                    "Maximal length should be greater than minimal"
                    + $"but maximal length was {maxLength} when minimal length was {minLength}",
                    nameof(maxLength));

            var wordsByLengths = CreateWordsByLengths(allowedSymbols, maxLength);
            var words = Enumerable.Empty<string>();

            for (var length = minLength; length <= maxLength; length++)
                words = words.Concat(wordsByLengths[length]);

            return words;
        }

        private static Dictionary<int, string[]> CreateWordsByLengths(
            IReadOnlyCollection<char> allowedSymbols,
            int maxLength)
        {
            var wordsByLengths = new Dictionary<int, string[]>
            {
                [1] = allowedSymbols.Select(s => s.ToString()).ToArray()
            };

            if (maxLength != 1)
                for (var length = 2; length <= maxLength; length++)
                    wordsByLengths[length]
                        = wordsByLengths[length - 1].CrossJoin(allowedSymbols, (w, s) => w + s).ToArray();

            return wordsByLengths;
        }

        public static IEnumerable<string> CreateAllWords(IReadOnlyCollection<char> allowedSymbols, int length)
        {
            if (length < 1)
                throw new ArgumentException($"Length must be bigger than 0 but was {length}", nameof(length));

            var wordsByLengths = CreateWordsByLengths(allowedSymbols, length);

            return wordsByLengths[length];
        }
    }
}