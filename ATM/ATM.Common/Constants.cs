namespace ATM.Common
{
    /// <summary>
    /// Presents all in app constants
    /// </summary>
    public class Constants
    {
        public const int BankNoteCassetteLimitCount = 2500;

        public const string DenominationTextConst = "{Denomination}";
        public const string AllowedBankNotesCountTextConst = "{allowedCount}";

        public const string EnteredIncorrectCountMessage = "Entered an incorrect bank notes count.";
        public const string CassetteLimitExceededMessage = "Denomination {Denomination} limit reached. {allowedCount} bank notes count put allowed.";
        public const string CassetteHasNoEnoughCountMessage = "Cassette with denomination {Denomination} has no enough bank notes count. {allowedCount} for cash draw allowed only.";
        public const string OperationSuccessMessage = "Operation success.";
        public const string DenominationNotAllowdMessage = "Denomination {Denomination} does not allowed.";
        public const string RequestedAmountShouldBeMultipleOfMessage = "Requested amount should be a multiple of {Denomination}";
        public const string ATMHasNoEnoughMoney = "ATM has no enough money for a cash draw operation. Please enter a lower amount.";
    }
}