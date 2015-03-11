using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TimeTracking
{
    public partial class InputWindow : Window
    {
        private readonly Func<string, bool> _isValid;

        public InputWindow(string title, string helpText, Func<string, bool> isValid)
        {
            _isValid = isValid;
            InitializeComponent();

            Title = title;
            TextBox.Text = helpText;

            TextBox.TextChanged += TextChanged;

            SaveButton.IsEnabled = false;
            SaveButton.Background = new SolidColorBrush(Colors.LightGray);
        }

        public static string GetText(string title, string helpText, Func<string, bool> isValid)
        {
            var newWindow = new InputWindow(title, helpText, isValid);

            newWindow.ShowDialog();

            return newWindow.EnteredText;
        }

        protected string EnteredText { get; set; }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isValid(TextBox.Text))
            {
                SaveButton.IsEnabled = true;
                SaveButton.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                SaveButton.IsEnabled = false;
                SaveButton.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void SaveButtonClick(object sender, MouseButtonEventArgs e)
        {
            EnteredText = TextBox.Text;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (EnteredText == null)
                e.Cancel = true;
        }
    }
}
