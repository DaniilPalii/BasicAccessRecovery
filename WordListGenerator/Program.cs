using System;
using System.IO;
using System.Linq;

namespace WordListGenerator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var minWordLength = ReadMinWordLength();
            var maxWordLength = ReadMaxWordLength();
            
            var wordsCreator = new WordsCreator();
            var wordsNumber = CountWords(wordsCreator.AllowedSymbols.Length, minWordLength, maxWordLength);
            Console.WriteLine($"I will create {wordsNumber:N0} words for You");

            var resultFilePath = Path.Combine(
                Environment.CurrentDirectory,
                $"Words-{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.txt");
            Console.WriteLine($"in the file \"{resultFilePath}\"");
            Console.WriteLine("now");
            Console.WriteLine();

            using (var fileStreamWriter = File.CreateText(resultFilePath))
            {
                var wordNumber = 0d;
                foreach (var word in wordsCreator.CreateAllWords(minWordLength, maxWordLength))
                {
                    fileStreamWriter.WriteLine(word);
                    Console.CursorLeft = 0;
                    Console.Write($"Generated {wordNumber++:N0} words");
                }
            }
            
            Console.WriteLine("Done");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static int ReadMaxWordLength()
        {
            Console.WriteLine("Input maximal word length (default is 12):");
            var input = Console.ReadLine();

            return !string.IsNullOrEmpty(input) ? int.Parse(input) : DefaultMaxWordLengthInput;
        }

        private static int ReadMinWordLength()
        {
            Console.WriteLine("Input minimal word length (default is 1):");
            var input = Console.ReadLine();

            return !string.IsNullOrEmpty(input) ? int.Parse(input) : DefaultMinWordLengthInput;
        }

        private static double CountWords(int symbolsNumber, int minWordLength, int maxWordLength)
        {
            return Enumerable.Range(minWordLength, maxWordLength - minWordLength + 1)
                .Select(wordLength => Math.Pow(symbolsNumber, wordLength))
                .Sum();
        }

        private const int DefaultMinWordLengthInput = 1;
        private const int DefaultMaxWordLengthInput = 12;
    }
}