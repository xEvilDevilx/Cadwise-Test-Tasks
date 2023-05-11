using System.Text.RegularExpressions;

namespace ATM.Common.Utils
{
    /// <summary>
    /// Presents a helper for text validation
    /// </summary>
    public class TextValidationUtils
    {
        /// <summary>
        /// Check for text contains numbers only
        /// </summary>
        /// <param name="text">Text for check</param>
        /// <returns>True if text contains numbers only</returns>
        public static bool IsNumberEntered(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(text);
        }
    }
}