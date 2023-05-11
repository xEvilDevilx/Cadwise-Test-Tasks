using System.Linq;
using System.Text.RegularExpressions;

using TextHandler.Interfaces;

namespace TextHandler.Processors
{
    /// <summary>
    /// Implements a text processing functionality
    /// </summary>
    public class TextProcessor : ITextProcessor
    {
        /// <summary>
        /// Removes all words from text which has a length lower than a parameter value
        /// </summary>
        /// <param name="text">Input text</param>
        /// <param name="minWordLength">A minimum length of each word in the text</param>
        /// <returns>Text result after removing a short words with length lower than a parameter value</returns>
        public string RemoveShortWords(string text, int minWordLength)
        {
            if (string.IsNullOrEmpty(text))
            { 
                return string.Empty; 
            }

            minWordLength--;
            string regexPattern = @"\b\w{1,minWordLength}\b".Replace("minWordLength", minWordLength.ToString());
            string result = Regex.Replace(text, regexPattern, string.Empty);
            return result;
        }

        /// <summary>
        /// Removes all punctuations in the text
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Text result after removing all punctuations</returns>
        public string RemovePunctuations(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            // I wanted to implement it via Regex, but in this case I should to use punctuation patterns
            // for different encodings, it could take more time for implementing issue, therefore I made a decision
            // for current case to use a punctuation check from char type.
            // Regex case looks like:
            // string result = Regex.Replace(text, Constants.Punctuations, string.Empty);
            // return result;
            return new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        }
    }
}