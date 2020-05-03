using System.Linq;
using NUnit.Framework;

namespace WordListGenerator.UnitTests
{
    [TestFixture]
    public class WordsCreatorTests
    {
        [SetUp]
        public void SetUp()
        {
            wordsCreator = new WordsCreator { AllowedSymbols = "abc".ToArray() };
        }
        
        [Test]
        public void ShouldNotGenerateDuplicates()
        {
            var words = wordsCreator.CreateAllWords(minLength: 1, maxLength: 3);

            Assert.That(words, Is.Unique);
        }

        [Test]
        public void ShouldGenerateMinLengthWord()
        {
            var words = wordsCreator.CreateAllWords(minLength: 1, maxLength: 3);

            Assert.That(words, Has.Member("a"));
        }

        [Test]
        public void ShouldSkipNotRequiredLengthWord()
        {
            var words = wordsCreator.CreateAllWords(minLength: 2, maxLength: 3);

            Assert.That(words, Has.No.Member("a"));
        }

        [Test]
        public void ShouldGenerateMaxLengthWord()
        {
            var words = wordsCreator.CreateAllWords(minLength: 1, maxLength: 3);

            Assert.That(words, Has.Member("abc"));
        }

        [Test]
        public void ShouldGenerateAllPossibleValues()
        {
            var words = wordsCreator.CreateAllWords(minLength: 1, maxLength: 3);

            Assert.That(words.Count(), Is.EqualTo(3 + (3 * 3) + (3 * 3 * 3)));
        }
        
        private WordsCreator wordsCreator = new WordsCreator();
    }
}