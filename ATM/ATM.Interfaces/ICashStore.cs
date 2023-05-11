using System.Collections.Generic;

using ATM.Common.Enum;
using ATM.Data;

namespace ATM.Interfaces
{
    /// <summary>
    /// Presents interface for a Cash Store
    /// </summary>
    public interface ICashStore
    {
        /// <summary>
        /// Cassettes collection of the Cash Store
        /// </summary>
        List<ICassette> Cassettes { get; }

        /// <summary>
        /// Puts bank notes to the cash store
        /// </summary>
        /// <param name="bankNotes">Bank notes with denominations and counts for put into the cash store</param>
        /// <returns>Cash operation result</returns>
        CashOperationResult Put(BankNotes[] bankNotes);

        /// <summary>
        /// Makes a cash draw operation
        /// </summary>
        /// <param name="denomintaion">Bank note denomination</param>
        /// <param name="amount">Cash amount</param>
        /// <returns>Bank notes with denominations and counts, and cash operation result</returns>
        (CashOperationResult, BankNotes[]) CashDraw(Denominations denomintaion, int amount);
    }
}