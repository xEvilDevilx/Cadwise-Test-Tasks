using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

using ATM.Common.Enum;
using ATM.Common.Utils;
using ATM.Data;

namespace ATM
{
    /// <summary>
    /// Interaction logic for CashWindow.xaml
    /// </summary>
    public partial class CashWindow : Window
    {
        /// <summary>
        /// Constructes a CashWindow dialog with enterable bank notes fields
        /// </summary>
        public CashWindow()
        {
            InitializeComponent();

            ClearBankNotes();
            InitEnterCash(true);
            _needToFillBankNotesCountNoti.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Constructs a CashWindow dialog
        /// </summary>
        /// <param name="bankNotes">Bank notes data</param>
        /// <param name="isEnterCashEnable">Marks bank notes fields like readonly or not</param>
        public CashWindow(BankNotes[] bankNotes, bool isEnterCashEnable = false)
        {
            InitializeComponent();

            ClearBankNotes();
            InitEnterCash(isEnterCashEnable);
            InitBankNotes(bankNotes);
            _needToFillBankNotesCountNoti.Visibility = Visibility.Hidden;
        }

        private void InitEnterCash(bool isEnterCashEnable)
        {
            if (isEnterCashEnable)
            {
                _denomination10Value.IsReadOnly = !isEnterCashEnable;
                _denomination50Value.IsReadOnly = !isEnterCashEnable;
                _denomination100Value.IsReadOnly = !isEnterCashEnable;
                _denomination500Value.IsReadOnly = !isEnterCashEnable;
                _denomination1000Value.IsReadOnly = !isEnterCashEnable;
                _denomination5000Value.IsReadOnly = !isEnterCashEnable;

                _okBtnPanel.Visibility = Visibility.Hidden;
                _okCancelBtnPanel.Visibility = Visibility.Visible;
            }
            else
            {
                _denomination10Value.IsReadOnly = isEnterCashEnable;
                _denomination50Value.IsReadOnly = isEnterCashEnable;
                _denomination100Value.IsReadOnly = isEnterCashEnable;
                _denomination500Value.IsReadOnly = isEnterCashEnable;
                _denomination1000Value.IsReadOnly = isEnterCashEnable;
                _denomination5000Value.IsReadOnly = isEnterCashEnable;

                _okBtnPanel.Visibility = Visibility.Visible;
                _okCancelBtnPanel.Visibility = Visibility.Hidden;
            }
        }

        private void ClearBankNotes()
        {
            _denomination10Value.Text = "0";
            _denomination50Value.Text = "0";
            _denomination100Value.Text = "0";
            _denomination500Value.Text = "0";
            _denomination1000Value.Text = "0";
            _denomination5000Value.Text = "0";
        }

        private void InitBankNotes(BankNotes[] bankNotes)
        {
            foreach (BankNotes bankNote in bankNotes)
            {
                switch (bankNote.Denomination)
                {
                    case Denominations.BankNote10:
                        _denomination10Value.Text = bankNote.Count.ToString();
                        break;
                    case Denominations.BankNote50:
                        _denomination50Value.Text = bankNote.Count.ToString();
                        break;
                    case Denominations.BankNote100:
                        _denomination100Value.Text = bankNote.Count.ToString();
                        break;
                    case Denominations.BankNote500:
                        _denomination500Value.Text = bankNote.Count.ToString();
                        break;
                    case Denominations.BankNote1000:
                        _denomination1000Value.Text = bankNote.Count.ToString();
                        break;
                    case Denominations.BankNote5000:
                        _denomination5000Value.Text = bankNote.Count.ToString();
                        break;
                }
            }
        }

        private void OkBtn(object sender, RoutedEventArgs rea)
        {
            if ((string.IsNullOrEmpty(_denomination10Value.Text) || _denomination10Value.Text == "0")
                && (string.IsNullOrEmpty(_denomination50Value.Text) || _denomination50Value.Text == "0")
                && (string.IsNullOrEmpty(_denomination100Value.Text) || _denomination100Value.Text == "0")
                && (string.IsNullOrEmpty(_denomination500Value.Text) || _denomination500Value.Text == "0")
                && (string.IsNullOrEmpty(_denomination1000Value.Text) || _denomination1000Value.Text == "0")
                && (string.IsNullOrEmpty(_denomination5000Value.Text) || _denomination5000Value.Text == "0"))
            {
                _needToFillBankNotesCountNoti.Visibility = Visibility.Visible;
                return;
            }

            _needToFillBankNotesCountNoti.Visibility = Visibility.Hidden;
            this.DialogResult = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs tcea)
        {
            tcea.Handled = TextValidationUtils.IsNumberEntered(tcea.Text);
        }

        /// <summary>
        /// Gets bank notes data from fields
        /// </summary>
        /// <returns>Bank notes data from fields</returns>
        public BankNotes[] GetBankNotesWindowData()
        {
            List<BankNotes> resultBankNotes = new List<BankNotes>();
            if (!string.IsNullOrEmpty(_denomination10Value.Text) && _denomination10Value.Text != "0")
            {
                int count = int.Parse(_denomination10Value.Text);
                resultBankNotes.Add(new BankNotes(Denominations.BankNote10, count));
            }

            if (!string.IsNullOrEmpty(_denomination50Value.Text) && _denomination50Value.Text != "0")
            {
                int count = int.Parse(_denomination50Value.Text);
                resultBankNotes.Add(new BankNotes(Denominations.BankNote50, count));
            }

            if (!string.IsNullOrEmpty(_denomination100Value.Text) && _denomination100Value.Text != "0")
            {
                int count = int.Parse(_denomination100Value.Text);
                resultBankNotes.Add(new BankNotes(Denominations.BankNote100, count));
            }

            if (!string.IsNullOrEmpty(_denomination500Value.Text) && _denomination500Value.Text != "0")
            {
                int count = int.Parse(_denomination500Value.Text);
                resultBankNotes.Add(new BankNotes(Denominations.BankNote500, count));
            }

            if (!string.IsNullOrEmpty(_denomination1000Value.Text) && _denomination1000Value.Text != "0")
            {
                int count = int.Parse(_denomination1000Value.Text);
                resultBankNotes.Add(new BankNotes(Denominations.BankNote1000, count));
            }

            if (!string.IsNullOrEmpty(_denomination5000Value.Text) && _denomination5000Value.Text != "0")
            {
                int count = int.Parse(_denomination5000Value.Text);
                resultBankNotes.Add(new BankNotes(Denominations.BankNote5000, count));
            }

            return resultBankNotes.ToArray();
        }
    }
}