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
using Fluent;

namespace PDFClassifierPro.UI
{
    public partial class MainWindow : RibbonWindow
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
        }

        private void OnOpenPdfClicked(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
                Title = "Select a PDF file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                _currentFilePath = openFileDialog.FileName;
                Viewer.LoadPdf(_currentFilePath);
                StatusText.Text = $"Loaded: {System.IO.Path.GetFileName(_currentFilePath)}";
                
                if (_licenseManager.IsProFeatureEnabled && _pdfHandler.IsScannedDocument(_currentFilePath))
                {
                    PerformAutoOcr();
                }
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
        }

        private void OnClearRedactionsClicked(object sender, RoutedEventArgs e)
        {
            _redactionAreas.Clear();
            StatusText.Text = "All redactions cleared";
        }

        private void OnApplyClassificationClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            
            var text = _pdfHandler.ExtractText(_currentFilePath);
            var classification = _classificationEngine.AnalyzeDocument(text);
            
            StatusText.Text = $"Document classified as: {classification}";
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
                }
                else
                {
                    StatusText.Text = "OCR failed. No image available.";
                }
            }
            catch
            {
                StatusText.Text = "OCR failed. Please try again.";
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
    }
}
