using System.Windows;

namespace PDFClassifierPro.UI.Views
{
    public partial class LicenseActivationDialog : Window
    {
        public string? LicenseKey { get; private set; }

        public LicenseActivationDialog()
        {
            InitializeComponent();
            LicenseKeyTextBox.Focus();
        }

        private void OnActivateClicked(object sender, RoutedEventArgs e)
        {
            LicenseKey = LicenseKeyTextBox.Text?.Trim();
            
            if (string.IsNullOrEmpty(LicenseKey))
            {
                MessageBox.Show("Please enter a valid license key.", "Invalid License", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            DialogResult = true;
            Close();
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 