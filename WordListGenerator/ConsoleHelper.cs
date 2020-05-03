using System;

namespace WordListGenerator
{
    public static class ConsoleHelper
    {
        public static string ReadLine(string message)
        {
            Console.Write($"{message}: ");

            return Console.ReadLine();
        }

        public static int? ReadInt(string message)
        {
            var input = ReadLine(message);

            return !string.IsNullOrEmpty(input) ? int.Parse(input) : (int?)null;
        }
    }
}