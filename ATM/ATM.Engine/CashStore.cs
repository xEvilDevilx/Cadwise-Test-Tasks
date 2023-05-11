using System.Collections.Generic;
using System.Linq;

using ATM.Common;
using ATM.Common.Enum;
using ATM.Data;
using ATM.Interfaces;

namespace ATM.Engine
{
    /// <summary>
    /// Implements functionality for a Cash Store
    /// </summary>
    public class CashStore : ICashStore
    {
        private readonly List<ICassette> _cassettes;

        /// <summary>
        /// Cassettes collection of the Cash Store
        /// </summary>
        public List<ICassette> Cassettes { get { return _cassettes; } }

        /// <summary>
        /// Constructs a CashStore instance with specified bank notes in cassettes 
        /// </summary>
        /// <param name="cassettes"></param>
        public CashStore(List<ICassette> cassettes)
        {
            _cassettes = cassettes;
        }

        /// <summary>
        /// Puts bank notes to the cash store
        /// </summary>
        /// <param name="bankNotes">Bank notes with denominations and counts for put into the cash store</param>
        /// <returns>Cash operation result</returns>
        public CashOperationResult Put(BankNotes[] bankNotes)
        {
            // check ATM ability to get all bank notes
            foreach (var bankNote in bankNotes)
            {
                ICassette? cassette = _cassettes.Where(x => x.Denomination == bankNote.Denomination).FirstOrDefault();
                if (cassette == null)
                {
                    // log unknown bank note denomination put request
                    string message = Constants.DenominationNotAllowdMessage
                        .Replace(Constants.DenominationTextConst, ((int)bankNote.Denomination).ToString());
                    return new CashOperationResult(CashOperationStatus.DenominationNotAllowed, message);
                }

                if (bankNote.Count + cassette.Count > Constants.BankNoteCassetteLimitCount)
                {
                    // log a cassette limit money exceeded
                    int allowedCount = Constants.BankNoteCassetteLimitCount - cassette.Count;
                    string message = Constants.CassetteLimitExceededMessage
                        .Replace(Constants.DenominationTextConst, ((int)bankNote.Denomination).ToString())
                        .Replace(Constants.AllowedBankNotesCountTextConst, allowedCount.ToString());
                    return new CashOperationResult(CashOperationStatus.LimitExceeded, message);
                }
            }

            // put bank notes to cassettes
            foreach (var bankNote in bankNotes)
            {
                ICassette? cassette = _cassettes.Where(x => x.Denomination == bankNote.Denomination).FirstOrDefault();
                cassette.Put(bankNote.Count);
            }

            return new CashOperationResult(CashOperationStatus.Success, Constants.OperationSuccessMessage);
        }

        /// <summary>
        /// Makes a cash draw operation
        /// </summary>
        /// <param name="denomination">Requested bank notes denomination</param>
        /// <param name="cashAmount">Cash amount</param>
        /// <returns>Bank notes with denominations and counts, and cash operation result</returns>
        public (CashOperationResult, BankNotes[]) CashDraw(Denominations denomination, int cashAmount)
        {
            CashOperationResult result;

            // check amount multiple min denomination
            if (cashAmount % (int)Denominations.BankNote10 != 0)
            {
                string message = Constants.RequestedAmountShouldBeMultipleOfMessage
                    .Replace(Constants.DenominationTextConst, ((int)Denominations.BankNote10).ToString());
                result = new CashOperationResult(CashOperationStatus.IncorrectRequest, message);
                return (result, new BankNotes[0]);
            }

            // check requested denomination exists
            if (!_cassettes.Exists(x => x.Denomination == denomination))
            {
                // log unknown bank note denomination put request
                string message = Constants.DenominationNotAllowdMessage
                    .Replace(Constants.DenominationTextConst, ((int)denomination).ToString());
                result = new CashOperationResult(CashOperationStatus.DenominationNotAllowed, message);
                return (result, new BankNotes[0]);
            }

            // try to collect requested money
            List<BankNotes> cashDrawBankNotes = new List<BankNotes>();
            int currentCashAmount = cashAmount;
            ICassette[] cassettes = _cassettes.Where(x => (int)x.Denomination <= (int)denomination).OrderByDescending(x => (int)x.Denomination).ToArray();
            foreach (var cassette in cassettes)
            {
                if (currentCashAmount < 1)
                {
                    break;
                }

                if (cassette.Count < 1)
                {
                    continue;
                }

                int denominationBankNotesCount = currentCashAmount / (int)cassette.Denomination;
                if (denominationBankNotesCount < 1)
                {
                    continue;
                }

                if (denominationBankNotesCount > cassette.Count)
                {
                    denominationBankNotesCount = cassette.Count;
                }

                BankNotes bankNotes = new BankNotes(cassette.Denomination, denominationBankNotesCount);
                cashDrawBankNotes.Add(bankNotes);
                currentCashAmount -= (denominationBankNotesCount * (int)cassette.Denomination);
            }

            // check ability to cash draw requested amount
            if (currentCashAmount > 0)
            {
                result = new CashOperationResult(CashOperationStatus.NotEnoughMoney, Constants.ATMHasNoEnoughMoney);
                return (result, new BankNotes[0]);
            }

            // foreach each cassette and collect requested amount to return
            foreach (BankNotes bankNotes in cashDrawBankNotes)
            {
                ICassette cassette = _cassettes.Where(x => x.Denomination == bankNotes.Denomination).First();
                cassette.CashDraw(bankNotes.Count);
            }
            result = new CashOperationResult(CashOperationStatus.Success, Constants.OperationSuccessMessage);
            return (result, cashDrawBankNotes.ToArray());
        }
    }
}