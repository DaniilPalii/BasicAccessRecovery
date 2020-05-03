using System;
using System.Linq;
using NUnit.Framework;

namespace WordListGenerator.UnitTests
{
    [TestFixture]
    public class WordsCreatorTests
    {
        [Test]
        public void ShouldNotGenerateDuplicates()
        {
            var allowedSymbols = "abc".ToArray();

            var words = WordsCreator.CreateAllWords(allowedSymbols, minLength: 1, maxLength: 3);

            Assert.That(words, Is.Unique);
        }

        [Test]
        public void ShouldGenerateMinLengthWord()
        {
            var allowedSymbols = "abc".ToArray();

            var words = WordsCreator.CreateAllWords(allowedSymbols, minLength: 1, maxLength: 3);

            Assert.That(words, Has.Member("a"));
        }

        [Test]
        public void ShouldSkipNotRequiredLengthWord()
        {
            var allowedSymbols = "abc".ToArray();

            var words = WordsCreator.CreateAllWords(allowedSymbols, minLength: 2, maxLength: 3);

            Assert.That(words, Has.No.Member("a"));
        }

        [Test]
        public void ShouldGenerateMaxLengthWord()
        {
            var allowedSymbols = "abc".ToArray();

            var words = WordsCreator.CreateAllWords(allowedSymbols, minLength: 1, maxLength: 3);

            Assert.That(words, Has.Member("abc"));
        }

        [Test]
        public void ShouldGenerateAllPossibleValues()
        {
            var allowedSymbols = "abc".ToArray();

            var words = WordsCreator.CreateAllWords(allowedSymbols, minLength: 1, maxLength: 3);

            Assert.That(words.Count(), Is.EqualTo(3 + (3 * 3) + (3 * 3 * 3)));
        }
    }
}