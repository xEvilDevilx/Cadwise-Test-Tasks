using System.IO;
using System.Threading.Tasks;

namespace TextHandler.Interfaces
{
    /// <summary>
    /// Presents an interface for a text stream processing
    /// </summary>
    public interface ITextStreamProcessor
    {
        /// <summary>
        /// Makes a text stream processing
        /// </summary>
        /// <param name="inputStream">Stream with input text</param>
        /// <param name="outputStream">Stream for output text</param>
        /// <param name="minWordLength">Minimal word length. All words with lower length will be removed</param>
        /// <param name="isRemovePunctuations">Flag marks to remove punctuations in the text or no</param>
        Task Process(Stream inputStream, Stream outputStream, int minWordLength, bool? isRemovePunctuations);
    }
}