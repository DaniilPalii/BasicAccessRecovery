using System;
using System.Collections.Generic;
using System.Linq;

namespace WordListGenerator
{
    public class WordsCreator
    {
        public char[] AllowedSymbols { get; set; } =
            "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890_-!".ToArray();

        public IEnumerable<string> CreateAllWords(int minLength, int maxLength)
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

            for (var length = minLength; length <= maxLength; length++)
                foreach (var word in CreateAllWords(length))
                    yield return word;
        }

        public IEnumerable<string> CreateAllWords(int length)
        {
            if (length <= 0)
                throw new ArgumentException($"Length must be bigger than 0 but was {length}", nameof(length));

            var words = AllowedSymbols.Select(s => s.ToString());

            if (length > 1)
                for (var i = 2; i <= length; i++)
                    words = words.CrossJoin(AllowedSymbols, (w, s) => w + s);

            return words;
        }
    }
}