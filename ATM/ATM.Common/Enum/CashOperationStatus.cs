namespace ATM.Common.Enum
{
    /// <summary>
    /// Preents a Cash Operation Status types
    /// </summary>
    public enum CashOperationStatus
    {
        Success,
        NotEnoughMoney,
        IncorrectRequest,
        LimitExceeded,
        DenominationNotAllowed
    }
}