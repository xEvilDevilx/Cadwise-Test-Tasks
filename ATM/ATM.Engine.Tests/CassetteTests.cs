using ATM.Common;
using ATM.Common.Enum;
using ATM.Data;
using ATM.Interfaces;

namespace ATM.Engine.Tests
{
    /// <summary>
    /// Presents tests for a Cassette functionality testing
    /// </summary>
    public class CassetteTests
    {
        private ICassette _cassette;

        /// <summary>
        /// Tests a Put() method
        /// </summary>
        [Test]
        public void PutTest()
        {
            // prepare
            int initialCount = 1500;
             _cassette = new Cassette(Denominations.BankNote1000, initialCount);
            int putCount = 999;
            int expectedCount = initialCount + putCount;

            // execute
            _cassette.Put(putCount);
            CashOperationResult incorrectRequestResult = _cassette.Put(-1);
            CashOperationResult limitExceededResult = _cassette.Put(Constants.BankNoteCassetteLimitCount - _cassette.Count + 1);

            // assert
            Assert.IsTrue(expectedCount == _cassette.Count);
            Assert.Throws<ArgumentException>(() => new Cassette(Denominations.BankNote1000, Constants.BankNoteCassetteLimitCount + 1));
            Assert.IsTrue(incorrectRequestResult.Status == CashOperationStatus.IncorrectRequest);
            Assert.IsTrue(limitExceededResult.Status == CashOperationStatus.LimitExceeded);
        }

        /// <summary>
        /// Tests a CashDraw() method
        /// </summary>
        [Test]
        public void CashDrawTest()
        {
            // prepare
            int initialCount = 1500;
            Denominations initialDenomination = Denominations.BankNote1000;
            _cassette = new Cassette(initialDenomination, initialCount);
            int cashDrawCount = 99;
            int expectedCount = initialCount - cashDrawCount;

            // execute
            var (result, bankNotes) = _cassette.CashDraw(cashDrawCount);
            var (incorrectRequestResult, incorrectRequestBankNotes) = _cassette.CashDraw(-1);
            var (notEnoughMoneyResult, notEnoughMoneyBankNotes) = _cassette.CashDraw(_cassette.Count + 1);

            // assert
            Assert.IsTrue(result.Status == CashOperationStatus.Success);
            Assert.IsTrue(bankNotes.Count == cashDrawCount);
            Assert.IsTrue(_cassette.Count == expectedCount);
            Assert.IsTrue(bankNotes.Denomination == initialDenomination);
            Assert.IsTrue(incorrectRequestResult.Status == CashOperationStatus.IncorrectRequest);
            Assert.IsTrue(notEnoughMoneyResult.Status == CashOperationStatus.NotEnoughMoney);
        }
    }
}