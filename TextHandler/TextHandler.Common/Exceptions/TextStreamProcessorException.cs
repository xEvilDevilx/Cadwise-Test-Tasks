using System;

namespace TextHandler.Common.Exceptions
{
    /// <summary>
    /// Presents an exception for a text stream processor fail situations
    /// </summary>
    public class TextStreamProcessorException : Exception
    {
        /// <summary>
        /// Constructs a Text Stream Processor exception instance with specified message
        /// </summary>
        /// <param name="message">Exception message</param>
        public TextStreamProcessorException(string message) : base(message)
        {
        }
    }
}