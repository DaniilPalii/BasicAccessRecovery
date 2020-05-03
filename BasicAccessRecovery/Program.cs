using System;
using System.Net.Http;
using System.Threading.Tasks;
using WordListGenerator;

namespace BasicAccessRecovery
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var uri = new Uri(ConsoleHelper.ReadLine("Uri to recovery access"));
            var minWordLength = ConsoleHelper.ReadInt("Input minimal word length (default is 1)") ?? 1;
            var maxWordLength = ConsoleHelper.ReadInt("Input maximal word length (default is 12)") ?? 12;

            var httpClient = new HttpClient();
            var loginsPasswordsCreator = new LoginsPasswordsCreator();
            
            Console.WriteLine("Trying to recovery access...");

            foreach (
                var (login, password)
                in loginsPasswordsCreator.CreateAllLoginsPasswords(minWordLength, maxWordLength))
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