using TextHandler.Interfaces;

namespace TextHandler.Processors.Tests
{
    /// <summary>
    /// Presents tests for a TextProcessor testing
    /// </summary>
    public class TextProcessorTests
    {
        private ITextProcessor _textProcessor;
        private string _inputText;

        [SetUp]
        public void Setup()
        {
            _textProcessor = new TextProcessor();
            _inputText = "! & ? , The .NET Runtime contains just the components needed to run a console app";
        }

        /// <summary>
        /// Tests a RemoveShortWords() method
        /// </summary>
        [Test]
        public void RemoveShortWordsTest()
        {
            // prepare
            string expectedText = "! & ? ,  . Runtime contains just  components needed    console ";
            int minWordLength = 4;

            // execute
            string resultText = _textProcessor.RemoveShortWords(_inputText, minWordLength);

            // assert
            Assert.IsTrue(expectedText.Equals(resultText));
        }

        /// <summary>
        /// Tests a RemovePunctuations() method
        /// </summary>
        [Test]
        public void RemovePunctuationsTest()
        {
            // prepare
            string expectedText = "    The NET Runtime contains just the components needed to run a console app";

            // execute
            string resultText = _textProcessor.RemovePunctuations(_inputText);

            // assert
            Assert.IsTrue(expectedText.Equals(resultText));
        }
    }
}