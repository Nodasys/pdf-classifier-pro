using System.Windows;
using Microsoft.Win32;
using PDFClassifierPro.Core.Ocr;
using PDFClassifierPro.Core.Redaction;
using PDFClassifierPro.Core.Classification;
using PDFClassifierPro.Core.License;
using PDFClassifierPro.Core.Utils;
using PDFClassifierPro.UI.Views;
using System.Collections.Generic;
using System.Drawing;


namespace PDFClassifierPro.UI
{
    public partial class MainWindow : Window
    {
        private LicenseManager? _licenseManager;
        private PdfHandler? _pdfHandler;
        private RedactionEngine? _redactionEngine;
        private ClassificationEngine? _classificationEngine;
        private FileService? _fileService;
        private string? _currentFilePath;
        private List<RectangleF>? _redactionAreas;

        public MainWindow()
        {
            InitializeComponent();
            InitializeServices();
            UpdateLicenseStatus();
        }

        private void InitializeServices()
        {
            _licenseManager = new LicenseManager();
            _pdfHandler = new PdfHandler();
            _redactionEngine = new RedactionEngine();
            _classificationEngine = new ClassificationEngine();
            _fileService = new FileService();
            _redactionAreas = new List<RectangleF>();
            
            _licenseManager.ActivateFreeLicense();
        }

        private void UpdateLicenseStatus()
        {
            LicenseStatus.Text = _licenseManager.IsProFeatureEnabled ? "Pro Version" : "Free Version";
            UpdateUIElements();
        }

        private void UpdateUIElements()
        {
            if (_redactionAreas != null)
            {
                RedactionCountText.Text = _redactionAreas.Count.ToString();
            }
            
            if (_licenseManager != null)
            {
                LicenseStatus.Text = _licenseManager.IsProFeatureEnabled ? "Pro Version" : "Free Version";
            }
        }

        private async void OnOpenPdfClicked(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
                Title = "Select a PDF file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _currentFilePath = openFileDialog.FileName;
                StatusText.Text = "Loading PDF...";
                
                await System.Threading.Tasks.Task.Run(() =>
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        Viewer.LoadPdf(_currentFilePath);
                        StatusText.Text = $"Loaded: {System.IO.Path.GetFileName(_currentFilePath)}";
                        
                        // Update page information
                        var pageCount = Viewer.GetPageCount();
                        var currentPage = Viewer.GetCurrentPage();
                        PageInfoText.Text = $"{currentPage} of {pageCount}";
                        
                        if (_licenseManager.IsProFeatureEnabled && _pdfHandler.IsScannedDocument(_currentFilePath))
                        {
                            PerformAutoOcr();
                        }
                    });
                });
            }
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Save PDF file"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                System.IO.File.Copy(_currentFilePath, saveFileDialog.FileName, true);
                StatusText.Text = "File saved successfully";
            }
        }

        private void OnRedactAreaClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            
            var redactionArea = new RectangleF(100, 100, 200, 50);
            _redactionAreas.Add(redactionArea);
            
            StatusText.Text = $"Added redaction area. Total: {_redactionAreas.Count}";
            UpdateUIElements();
        }

        private void OnClearRedactionsClicked(object sender, RoutedEventArgs e)
        {
            _redactionAreas.Clear();
            StatusText.Text = "All redactions cleared";
            UpdateUIElements();
        }

        private void OnApplyClassificationClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            
            var text = _pdfHandler.ExtractText(_currentFilePath);
            var classification = _classificationEngine.AnalyzeDocument(text);
            var sensitiveTerms = _classificationEngine.ExtractSensitiveTerms(text);
            
            ClassificationText.Text = classification.ToString();
            StatusText.Text = $"Document classified as: {classification}";
            
            SensitiveTermsList.Items.Clear();
            foreach (var term in sensitiveTerms)
            {
                SensitiveTermsList.Items.Add(term);
            }
        }

        private void OnAutoOcrClicked(object sender, RoutedEventArgs e)
        {
            if (!_licenseManager.IsProFeatureEnabled)
            {
                MessageBox.Show("Auto OCR is a Pro feature. Please upgrade to Pro version.", "Pro Feature", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            PerformAutoOcr();
        }

        private void PerformAutoOcr()
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            
            StatusText.Text = "Performing OCR analysis...";
            var ocrEngine = new OcrEngine();
            
            try
            {
                var img = Viewer.GetCurrentPageImage();
                if (img != null)
                {
                    var text = ocrEngine.RunOcr(img);
                    StatusText.Text = $"OCR completed. Found {text.Length} characters.";
                    OcrTextDisplay.Text = text;
                }
                else
                {
                    StatusText.Text = "OCR failed. No image available.";
                    OcrTextDisplay.Text = "No text extracted";
                }
            }
            catch
            {
                StatusText.Text = "OCR failed. Please try again.";
                OcrTextDisplay.Text = "OCR processing failed";
            }
        }

        private void OnExportRedactedClicked(object sender, RoutedEventArgs e)
        {
            if (!_licenseManager.IsProFeatureEnabled)
            {
                MessageBox.Show("Export redacted PDF is a Pro feature. Please upgrade to Pro version.", "Pro Feature", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            if (string.IsNullOrEmpty(_currentFilePath) || _redactionAreas.Count == 0) return;
            
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Save redacted PDF file"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _redactionEngine.ApplyTrueRedaction(_currentFilePath, saveFileDialog.FileName, _redactionAreas);
                StatusText.Text = "Redacted PDF exported successfully";
            }
        }

        private void OnActivateProClicked(object sender, RoutedEventArgs e)
        {
            var licenseDialog = new LicenseActivationDialog();
            if (licenseDialog.ShowDialog() == true)
            {
                var licenseKey = licenseDialog.LicenseKey;
                if (_licenseManager.ValidateLicense(licenseKey))
                {
                    UpdateLicenseStatus();
                    MessageBox.Show("Pro license activated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Invalid license key. Please check and try again.", "Invalid License", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void OnPreviousPageClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            
            Viewer.GoToPreviousPage();
            UpdatePageInfo();
        }

        private void OnNextPageClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            
            Viewer.GoToNextPage();
            UpdatePageInfo();
        }

        private void UpdatePageInfo()
        {
            var pageCount = Viewer.GetPageCount();
            var currentPage = Viewer.GetCurrentPage();
            PageInfoText.Text = $"{currentPage} of {pageCount}";
        }

        private void OnExitClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
