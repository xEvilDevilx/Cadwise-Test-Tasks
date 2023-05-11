using System.Windows;

namespace ATM
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        /// <summary>
        /// Constructs a Notification fialog with specified text message
        /// </summary>
        /// <param name="message">Specified text message</param>
        public NotificationWindow(string message)
        {
            InitializeComponent();

            _textField.Text = message;
        }
    }
}