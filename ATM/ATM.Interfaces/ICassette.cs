using ATM.Common.Enum;
using ATM.Data;

namespace ATM.Interfaces
{
    /// <summary>
    /// Presents an interface for an ATM Cassette
    /// </summary>
    public interface ICassette
    {
        /// <summary>
        /// Denomination of the cassette
        /// </summary>
        Denominations Denomination { get; }

        /// <summary>
        /// Count of bank notes in the cassette
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Puts bank notes to the cassette
        /// </summary>
        /// <param name="count">Count of bank notes for put into the cassette</param>
        /// <returns>Cash operation result of put bank notes</returns>
        CashOperationResult Put(int count);

        /// <summary>
        /// Makes a cash draw operation
        /// </summary>
        /// <param name="count">Bank notes count</param>
        /// <returns>Bank notes with denomination and counts, and a cash operation result instance</returns>
        (CashOperationResult, BankNotes) CashDraw(int count);
    }
}