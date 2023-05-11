using System;
using System.IO;
using System.Threading.Tasks;

using TextHandler.Common.Exceptions;
using TextHandler.Interfaces;

namespace TextHandler.Processors
{
    /// <summary>
    /// Implements a text stream processing functionality
    /// </summary>
    public class TextStreamProcessor : ITextStreamProcessor
    {
        private ITextProcessor _textProcessor;

        /// <summary>
        /// Constructs specified text stream processor instance
        /// </summary>
        /// <param name="textProcessor">Specified text processor</param>
        public TextStreamProcessor(ITextProcessor textProcessor) 
        {
            _textProcessor = textProcessor;
        }

        /// <summary>
        /// Makes a text stream processing
        /// </summary>
        /// <param name="inputStream">Stream with input text</param>
        /// <param name="outputStream">Stream for output text</param>
        /// <param name="minWordLength">Minimal word length. All words with lower length will be removed</param>
        /// <param name="isRemovePunctuations">Flag marks to remove punctuations in the text or no</param>
        public async Task Process(Stream inputStream, Stream outputStream, int minWordLength, bool? isRemovePunctuations)
        {
            try
            {
                using (StreamReader sr = new StreamReader(inputStream, leaveOpen: true))
                using (StreamWriter sw = new StreamWriter(outputStream, sr.CurrentEncoding, leaveOpen: true))
                {
                    String textLine;
                    while ((textLine = await sr.ReadLineAsync()) != null)
                    {
                        textLine = _textProcessor.RemoveShortWords(textLine, minWordLength);
                        if (isRemovePunctuations.HasValue && isRemovePunctuations.Value)
                        {
                            textLine = _textProcessor.RemovePunctuations(textLine);
                        }

                        await sw.WriteLineAsync(textLine);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new TextStreamProcessorException(ex.Message);
            }
        }
    }
}