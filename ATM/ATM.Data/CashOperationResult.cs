using ATM.Common.Enum;

namespace ATM.Data
{
    /// <summary>
    /// Presents a Cash Operation result data
    /// </summary>
    public class CashOperationResult
    {
        /// <summary>
        /// Status of a Cash Operation
        /// </summary>
        public CashOperationStatus Status { get; private set; }

        /// <summary>
        /// Additional message for explaining a specified status
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Constructs an instance for result of a Cash Operation
        /// </summary>
        /// <param name="status">Status of a Cash Operation</param>
        /// <param name="message">Additional message for explaining a specified status</param>
        public CashOperationResult(CashOperationStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}