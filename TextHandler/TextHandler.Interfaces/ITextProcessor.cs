namespace TextHandler.Interfaces
{
    /// <summary>
    /// Presents an interface for a text processing
    /// </summary>
    public interface ITextProcessor
    {
        /// <summary>
        /// Removes all words from text which has a length lower than a parameter value
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="minWordLength">A minimum length of each word in the text</param>
        /// <returns>Text result after removing a short words with length lower than a parameter value</returns>
        string RemoveShortWords(string text, int minWordLength);

        /// <summary>
        /// Removes all punctuations in the text
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Text result after removing all punctuations</returns>
        string RemovePunctuations(string text);
    }
}