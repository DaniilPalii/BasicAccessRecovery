using System.Collections.Generic;
using WordListGenerator;

namespace BasicAccessRecovery
{
    public class LoginsPasswordsCreator
    {
        public WordsCreator WordsCreator { get; set; } = new WordsCreator();

        public IEnumerable<(string, string)> CreateAllLoginsPasswords(int minLength, int maxLength)
        {
            return WordsCreator.CreateAllWords(minLength, maxLength)
                .CrossJoin(WordsCreator.CreateAllWords(minLength, maxLength), (login, password) => (login, password));
        }
    }
}