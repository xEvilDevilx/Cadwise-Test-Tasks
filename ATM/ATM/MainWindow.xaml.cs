using System.Collections.Generic;
using System.Windows;

using ATM.Common;
using ATM.Common.Enum;
using ATM.Data;
using ATM.Engine;
using ATM.Interfaces;

namespace ATM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ICassette> _cassettes;
        private ICashStore _cashStore;

        public MainWindow()
        {
            InitializeComponent();

            _cassettes = new List<ICassette>()
            {
                new Cassette(Denominations.BankNote10, Constants.BankNoteCassetteLimitCount),
                new Cassette(Denominations.BankNote50, Constants.BankNoteCassetteLimitCount),
                new Cassette(Denominations.BankNote100, Constants.BankNoteCassetteLimitCount),
                new Cassette(Denominations.BankNote500, Constants.BankNoteCassetteLimitCount),
                new Cassette(Denominations.BankNote1000, Constants.BankNoteCassetteLimitCount),
                new Cassette(Denominations.BankNote5000, Constants.BankNoteCassetteLimitCount),
            };
            _cashStore = new CashStore(_cassettes);
            UpdateCassettesUI();
        }

        private void BtnPutClick(object sender, RoutedEventArgs rea)
        {
            CashWindow window = new CashWindow();
            if (window.ShowDialog() == true)
            {
                BankNotes[] bankNotes = window.GetBankNotesWindowData();
                CashOperationResult result = _cashStore.Put(bankNotes);
                if (result.Status == CashOperationStatus.Success)
                {
                    UpdateCassettesUI();
                    ShowNotificationMessage(result.Message);
                }
                else
                {
                    ShowNotificationMessage(result.Message);
                }
            }
        }

        private void BtnCashDrawClick(object sender, RoutedEventArgs rea)
        {
            RequestDenominationWindow window = new RequestDenominationWindow();
            if (window.ShowDialog() == true)
            {
                var (result, bankNotes) = _cashStore.CashDraw(window.SelectedDenomination, window.EnteredCashAmount);
                switch (result.Status)
                {
                    case CashOperationStatus.Success:
                        ShowCashWithdrawnBankNotes(bankNotes);
                        UpdateCassettesUI();
                        break;
                    case CashOperationStatus.NotEnoughMoney:
                    case CashOperationStatus.IncorrectRequest:
                    case CashOperationStatus.LimitExceeded:
                    case CashOperationStatus.DenominationNotAllowed:
                        ShowNotificationMessage(result.Message);
                        break;
                }
            }
        }

        private void ShowCashWithdrawnBankNotes(BankNotes[] bankNotes)
        {
            CashWindow window = new CashWindow(bankNotes);
            window.ShowDialog();
        }

        private void ShowNotificationMessage(string message)
        {
            NotificationWindow window = new NotificationWindow(message);
            window.ShowDialog();
        }

        private void UpdateCassettesUI()
        {
            foreach (ICassette cassette in _cashStore.Cassettes)
            {
                switch (cassette.Denomination)
                {
                    case Denominations.BankNote10:
                        _denomination10Value.Text = cassette.Count.ToString();
                        SetFullVisibility(cassette.Count, _denomination10Full);
                        break;
                    case Denominations.BankNote50:
                        _denomination50Value.Text = cassette.Count.ToString();
                        SetFullVisibility(cassette.Count, _denomination50Full);
                        break;
                    case Denominations.BankNote100:
                        _denomination100Value.Text = cassette.Count.ToString();
                        SetFullVisibility(cassette.Count, _denomination100Full);
                        break;
                    case Denominations.BankNote500:
                        _denomination500Value.Text = cassette.Count.ToString();
                        SetFullVisibility(cassette.Count, _denomination500Full);
                        break;
                    case Denominations.BankNote1000:
                        _denomination1000Value.Text = cassette.Count.ToString();
                        SetFullVisibility(cassette.Count, _denomination1000Full);
                        break;
                    case Denominations.BankNote5000:
                        _denomination5000Value.Text = cassette.Count.ToString();
                        SetFullVisibility(cassette.Count, _denomination5000Full);
                        break;
                }
            }
        }

        private void SetFullVisibility(int cassetteBankNotesCount, System.Windows.Controls.Label label)
        {
            if (cassetteBankNotesCount >= Constants.BankNoteCassetteLimitCount)
            {
                label.Visibility = Visibility.Visible;
            }
            else
            {
                label.Visibility = Visibility.Hidden;
            }
        }
    }
}