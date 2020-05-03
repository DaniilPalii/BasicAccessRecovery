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

            var resultFilePath = Path.Combine(
                Environment.CurrentDirectory,
                $"Words-{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.txt");
            Console.Write($"Creating file \"{resultFilePath}\"...");
            using var fileStreamWriter = File.CreateText(resultFilePath);

            foreach (var word in WordsCreator.CreateAllWords(AllowedSymbols, minWordLength, maxWordLength))
                fileStreamWriter.WriteLine(word);
            
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

        private static readonly char[] AllowedSymbols
            = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890_-!".ToArray();

        private const int DefaultMinWordLengthInput = 1;
        private const int DefaultMaxWordLengthInput = 12;
    }
}