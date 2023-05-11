using ATM.Common.Enum;
using ATM.Data;
using ATM.Interfaces;

namespace ATM.Engine.Tests
{
    /// <summary>
    /// Presents tests for a CashStore functionality testing
    /// </summary>
    public class CashStoreTests
    {
        private List<ICassette> _cassettes;
        private ICashStore _cashStore;
        private int initialCount = 1500;

        [SetUp]
        public void Setup()
        {
            _cassettes = new List<ICassette>()
            {
                new Cassette(Denominations.BankNote100, initialCount),
                new Cassette(Denominations.BankNote500, initialCount),
                new Cassette(Denominations.BankNote1000, initialCount)
            };
            _cashStore = new CashStore(_cassettes);
        }

        /// <summary>
        /// Tests a Put() method
        /// </summary>
        [Test]
        public void PutTest()
        {
            // prepare
            BankNotes[] bankNotes = new BankNotes[2]
            {
                new BankNotes(Denominations.BankNote100, 15),
                new BankNotes(Denominations.BankNote1000, 37)
            };
            int expectedCount100 = _cashStore.Cassettes[0].Count + 15;
            int expectedCount1000 = _cashStore.Cassettes[2].Count + 37;

            // execute
            _cashStore.Put(bankNotes);
            CashOperationResult denominationNotAllowedResult = _cashStore.Put(new BankNotes[1] { new BankNotes(Denominations.BankNote50, 15), });
            CashOperationResult limitExceededResult = _cashStore.Put(new BankNotes[1] { new BankNotes(Denominations.BankNote100, _cashStore.Cassettes[0].Count + 1), });

            // assert
            Assert.IsTrue(expectedCount100 == _cashStore.Cassettes[0].Count);
            Assert.IsTrue(expectedCount1000 == _cashStore.Cassettes[2].Count);
            Assert.IsTrue(denominationNotAllowedResult.Status == CashOperationStatus.DenominationNotAllowed);
            Assert.IsTrue(limitExceededResult.Status == CashOperationStatus.LimitExceeded);
        }

        /// <summary>
        /// Tests a CashDraw() method
        /// </summary>
        [Test]
        public void CashDrawTest()
        {
            // prepare
            int expectedCount1000 = 1500;
            int expectedCount500 = 675;
            int expectedCount100 = 3;

            // execute
            var (result, bankNotes) = _cashStore.CashDraw(Denominations.BankNote1000, 1837800);
            var (incorrectRequestResult, incorrectRequestBankNotes) = _cashStore.CashDraw(Denominations.BankNote100, 333);
            var (denominationNotAllowedResult, denominationNotAllowedBankNotes) = _cashStore.CashDraw(Denominations.BankNote50, 100);
            var (notEnoughMoneyResult, notEnoughMoneyBankNotes) = _cashStore.CashDraw(Denominations.BankNote1000, 2500000);

            // assert
            Assert.IsTrue(bankNotes[0].Count == expectedCount1000 && bankNotes[1].Count == expectedCount500 && bankNotes[2].Count == expectedCount100);
            Assert.IsTrue(incorrectRequestResult.Status == CashOperationStatus.IncorrectRequest);
            Assert.IsTrue(denominationNotAllowedResult.Status == CashOperationStatus.DenominationNotAllowed);
            Assert.IsTrue(notEnoughMoneyResult.Status == CashOperationStatus.NotEnoughMoney);
        }
    }
}