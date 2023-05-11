using System;

using ATM.Common;
using ATM.Common.Enum;
using ATM.Data;
using ATM.Interfaces;

namespace ATM.Engine
{
    /// <summary>
    /// Implements a functionality for an ATM Cassette
    /// </summary>
    public class Cassette : ICassette
    {
        /// <summary>
        /// Denomination of the cassette
        /// </summary>
        public Denominations Denomination { get; private set; }

        /// <summary>
        /// Count of bank notes in the cassette
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Constructs a Cassette instance with initial bank notes count and denomination
        /// </summary>
        /// <param name="denomintaion">Denomination of the cassette</param>
        /// <param name="count">Count of bank notes in the cassette</param>
        public Cassette(Denominations denomintaion, int count)
        {
            if (count > Constants.BankNoteCassetteLimitCount)
            {
                string message = Constants.CassetteLimitExceededMessage.
                    Replace(Constants.DenominationTextConst, ((int)Denomination).ToString()).
                    Replace(Constants.AllowedBankNotesCountTextConst, Constants.BankNoteCassetteLimitCount.ToString());
                throw new ArgumentException(message, nameof(count));
            }

            Denomination = denomintaion;
            Count = count;
        }

        /// <summary>
        /// Puts bank notes to the cassette
        /// </summary>
        /// <param name="count">Count of bank notes for put into the cassette</param>
        /// <returns>Cash operation result of put bank notes</returns>
        public CashOperationResult Put(int count)
        {
            if (count < 0)
            {
                // log a hack trying
                return new CashOperationResult(CashOperationStatus.IncorrectRequest, Constants.EnteredIncorrectCountMessage);
            }

            if (count + Count > Constants.BankNoteCassetteLimitCount)
            {
                // log a cassette limit money exceeded
                int allowedCount = Constants.BankNoteCassetteLimitCount - Count;
                string message = Constants.CassetteLimitExceededMessage.
                    Replace(Constants.DenominationTextConst, ((int)Denomination).ToString()).
                    Replace(Constants.AllowedBankNotesCountTextConst, allowedCount.ToString());
                return new CashOperationResult(CashOperationStatus.LimitExceeded, message);
            }

            Count += count;

            return new CashOperationResult(CashOperationStatus.Success, Constants.OperationSuccessMessage);
        }

        /// <summary>
        /// Makes a cash draw operation
        /// </summary>
        /// <param name="count">Bank notes count</param>
        /// <returns>Bank notes with denomination and count, and a cash operation result instance</returns>
        public (CashOperationResult, BankNotes) CashDraw(int count)
        {
            CashOperationResult result;

            if (count < 0)
            {
                // log a hack trying
                result = new CashOperationResult(CashOperationStatus.IncorrectRequest, Constants.EnteredIncorrectCountMessage);
                return (result, new BankNotes(Denomination, 0));
            }

            if (count > Count)
            {
                // log of not enough money in the cassette
                string message = Constants.CassetteHasNoEnoughCountMessage.
                    Replace(Constants.DenominationTextConst, ((int)Denomination).ToString()).
                    Replace(Constants.AllowedBankNotesCountTextConst, Count.ToString());
                result = new CashOperationResult(CashOperationStatus.NotEnoughMoney, message);
                return (result, new BankNotes(Denomination, 0));
            }

            Count -= count;

            result = new CashOperationResult(CashOperationStatus.Success, Constants.OperationSuccessMessage);
            return (result, new BankNotes(Denomination, count));
        }
    }
}