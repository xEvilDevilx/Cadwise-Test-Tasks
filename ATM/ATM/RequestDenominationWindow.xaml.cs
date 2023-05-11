using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

using ATM.Common.Enum;
using ATM.Common.Utils;

namespace ATM
{
    /// <summary>
    /// Interaction logic for RequestDenominationWindow.xaml
    /// </summary>
    public partial class RequestDenominationWindow : Window
    {
        /// <summary>
        /// Cash amount from input field
        /// </summary>
        public int EnteredCashAmount { get { return int.Parse(_cashAmount.Text); } }

        /// <summary>
        /// Denomination choosed by user
        /// </summary>
        public Denominations SelectedDenomination { get { return (Denominations)int.Parse((string)_denominations.SelectedItem); } }

        public RequestDenominationWindow()
        {
            InitializeComponent();

            InitDenominationItems();
        }

        private void OkBtn(object sender, RoutedEventArgs rea)
        {
            this.DialogResult = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs tcea)
        {
            tcea.Handled = TextValidationUtils.IsNumberEntered(tcea.Text);
        }

        private void InitDenominationItems()
        {
            foreach (Denominations denomination in Enum.GetValues(typeof(Denominations)))
            {
                _denominations.Items.Add(((int)denomination).ToString());
            }
            _denominations.SelectedIndex = 0;
        }
    }
}