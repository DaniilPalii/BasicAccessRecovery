using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WordListGenerator;

namespace BasicAccessRecovery
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var uri = new Uri(ConsoleHelper.ReadString("Uri to recovery access"));
            
            var wordsCreator = new WordsCreator();
            var allowedSymbolsInput = ConsoleHelper.ReadString(
                "Allowed credentials symbols "
                    + "(don't use separators) "
                    + $"(leave empty to use default \"{wordsCreator.AllowedSymbols}\")");
            if (allowedSymbolsInput != null)
                wordsCreator.AllowedSymbols = allowedSymbolsInput.ToArray();
            
            var minWordLength = ConsoleHelper.ReadInt("Input minimal word length (default is 1)") ?? 1;
            var maxWordLength = ConsoleHelper.ReadInt("Input maximal word length (default is 12)") ?? 12;
            
            var loginInput = ConsoleHelper.ReadString("Login (leave empty to try recover)");

            Console.WriteLine("Trying to recovery access...");

            var httpClient = new HttpClient();
            var credentials = loginInput != null
                ? wordsCreator.CreateAllWords(minWordLength, maxWordLength).Select(p => (loginInput, p))
                : wordsCreator.CreateAllWords(minWordLength, maxWordLength)
                    .CrossJoin(wordsCreator.CreateAllWords(minWordLength, maxWordLength), (l, p) => (l, p));

            foreach (var (login, password) in credentials)
            {
                var uriWithCredentials = new UriBuilder(uri) { UserName = login, Password = password }.Uri;
                var gettingTask = httpClient.GetAsync(uriWithCredentials);
                Console.WriteLine(uriWithCredentials);
                var responseMessage = await gettingTask;

                if (responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success!");
                    Console.WriteLine($"Login: {login}");
                    Console.WriteLine($"Password: {password}");

                    Console.WriteLine("Press any key...");
                    Console.ReadKey();
                    Console.WriteLine("Do You really want to exit?");
                    Console.ReadKey();

                    return;
                }
            }
            
            Console.WriteLine("Failed");
        }
    }
}