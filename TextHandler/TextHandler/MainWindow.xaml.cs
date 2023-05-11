using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

using TextHandler.Common.Exceptions;
using TextHandler.Interfaces;
using TextHandler.Processors;

namespace TextHandler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITextProcessor _textProcessor;
        private ITextStreamProcessor _fileProcessor;

        public MainWindow()
        {
            InitializeComponent();

            // In this case better to use DI like 'AutoFac', 'Unity', 'Ninject', etc
            _textProcessor = new TextProcessor();
            _fileProcessor = new TextStreamProcessor(_textProcessor);
        }

        private void BtnInputFilesBrowseClick(object sender, RoutedEventArgs rea)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select a folder with input text files",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar,
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _inputFilesDir.Text = dialog.SelectedPath;
                _popup.IsOpen = false;
            }
        }

        private void BtnOutputFilesBrowseClick(object sender, RoutedEventArgs rea)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Select a folder with output text files",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar,
                ShowNewFolderButton = true
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _outputFilesDir.Text = dialog.SelectedPath;
                _popup.IsOpen = false;
            }
        }

        private async void BtnStartClick(object sender, RoutedEventArgs rea)
        {
            if (string.IsNullOrEmpty(_inputFilesDir.Text))
            {
                ShowWarningPopup("Choose the input text files directory please");
                return;
            }

            if (string.IsNullOrEmpty(_outputFilesDir.Text))
            {
                ShowWarningPopup("Choose the output text files directory please");
                return;
            }

            if (string.IsNullOrEmpty(_minTextLength.Text))
            {
                ShowWarningPopup("Choose the minimal text length please");
                return;
            }

            SetStatus(true);

            DirectoryInfo dirInfo = new DirectoryInfo(_inputFilesDir.Text);
            FileInfo[] files = dirInfo.GetFiles("*.txt");

            Task[] tasks = new Task[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                tasks[i] = FileProcess(files[i].Name, files[i].FullName);
                await tasks[i];
            }

            Task.WaitAll(tasks);

            SetStatus(false);
            ShowInfoPopup("All text files processed and saved to the output directory");
        }

        private async Task FileProcess(string fileName, string filePath)
        {
            string outputFilePath = Path.Combine(_outputFilesDir.Text, fileName);
            int minWordLength = int.Parse(_minTextLength.Text);
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            try
            {
                using (Stream inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (Stream outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    await _fileProcessor.Process(inputStream, outputStream, minWordLength, _useRemovePunctuations.IsChecked);
                }
            }
            catch (TextStreamProcessorException ex)
            {
                // log current exception
            }
        }

        private void ShowWarningPopup(string message)
        {
            ShowPopup(message, Color.FromArgb(255, 255, 182, 193));
        }

        private void ShowInfoPopup(string message)
        {
            ShowPopup(message, Color.FromArgb(255, 144, 234, 144));
        }

        private void ShowPopup(string message, Color color)
        {
            _popupText.Text = message;
            _popupText.Background = new SolidColorBrush(color);
            _popup.IsOpen = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetStatus(bool isInProgress)
        {
            _inputFilesBrowseBtn.IsEnabled = !isInProgress;
            _outputFilesBrowseBtn.IsEnabled = !isInProgress;
            _minTextLength.IsEnabled = !isInProgress;
            if (isInProgress)
            {
                _startBtn.Content = "In progress...";
            }
            else
            {
                _startBtn.Content = "Start";
            }
            _startBtn.IsEnabled = !isInProgress;
        }
    }
}