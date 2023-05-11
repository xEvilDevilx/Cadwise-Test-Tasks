using ATM.Common.Enum;

namespace ATM.Data
{
    /// <summary>
    /// Presents a bank notes entity
    /// </summary>
    public class BankNotes
    {
        /// <summary>
        /// Bank notes denomination
        /// </summary>
        public Denominations Denomination { get; }

        /// <summary>
        /// Bank notes count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Constructs the BankNotes entity instance
        /// </summary>
        /// <param name="denomination">Bank notes denomination</param>
        /// <param name="count">Bank notes count</param>
        public BankNotes(Denominations denomination, int count)
        {
            Denomination = denomination;
            Count = count;
        }
    }
}