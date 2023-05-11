using System.Text;

using TextHandler.Interfaces;

namespace TextHandler.Processors.Tests
{
    /// <summary>
    /// Presents tests for a TextStreamProcessor testing
    /// </summary>
    public class TextStreamProcessorTests
    {
        private ITextProcessor _textProcessor;
        private ITextStreamProcessor _textStreamProcessor;
        private string _inputText;

        [SetUp]
        public void Setup()
        {
            _textProcessor = new TextProcessor(); // could use mock instead
            _textStreamProcessor = new TextStreamProcessor(_textProcessor);
            _inputText = "! & ? , The .NET Runtime contains just the components needed to run a console app";
        }

        /// <summary>
        /// Tests a Process() method
        /// </summary>
        [Test]
        public async Task ProcessTest()
        {
            // prepare
            using (var testInputStream = new MemoryStream(Encoding.UTF8.GetBytes(_inputText)))
            using (var testOutputStream = new MemoryStream())
            {
                string expectedText = "      Runtime contains just  components needed    console \r\n";
                int minWordLength = 4;

                // execute
                await _textStreamProcessor.Process(testInputStream, testOutputStream, minWordLength, true);
                testOutputStream.Position = 0;
                using var sr = new StreamReader(testOutputStream, leaveOpen: true);
                string resultText = sr.ReadToEnd();

                // assert
                Assert.IsTrue(expectedText.Equals(resultText));
            }
        }
    }
}