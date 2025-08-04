using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.IO;
using PDFClassifierPro.Core.Ocr;
using PDFClassifierPro.Core.Utils;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using PdfiumViewer;

namespace PDFClassifierPro.UI.Views.Controls
{
    public partial class PdfViewerControl : global::System.Windows.Controls.UserControl
    {
        private string? _currentPdfPath;
        private PdfViewer _pdfViewer;
        private PdfDocument? _currentDocument;

        public PdfViewerControl()
        {
            InitializeComponent();

            _pdfViewer = new PdfViewer
            {
                Dock = DockStyle.Fill,
                ZoomMode = PdfViewerZoomMode.FitWidth
            };

            winFormsHost.Child = _pdfViewer;
        }

        public async void LoadPdf(string path)
        {
            if (!File.Exists(path)) return;

            _currentPdfPath = path;
            
            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(() =>
                        {
                            LoadPdfDocument(path);
                        });
                        
                        if (!PdfInspector.HasText(path))
                        {
                            var bmp = GetCurrentPageImage();
                            if (bmp != null)
                            {
                                var ocr = new OcrEngine();
                                var text = ocr.RunOcr(bmp);
                                System.Diagnostics.Debug.WriteLine("[OCR] " + text);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[PDF Processing] Error: {ex.Message}");
                    }
                });
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PDF Loading] Error: {ex.Message}");
                System.Windows.MessageBox.Show($"Error loading PDF: {ex.Message}", "PDF Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        private void LoadPdfDocument(string path)
        {
            try
            {
                _currentDocument?.Dispose();
                _currentDocument = PdfDocument.Load(path);
                _pdfViewer.Document = _currentDocument;
                
                System.Diagnostics.Debug.WriteLine($"[PDF] Loaded document with {_currentDocument.PageCount} pages");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PDF] Error loading document: {ex.Message}");
                System.Windows.MessageBox.Show($"Error loading PDF document: {ex.Message}", "PDF Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }

        public Bitmap? GetCurrentPageImage()
        {
            if (_currentDocument == null || _pdfViewer.Document == null) 
                return null;
            
            try
            {
                var currentPage = 0; // Start with first page for now
                if (currentPage < 0 || currentPage >= _currentDocument.PageCount)
                    return null;
                
                using var image = _currentDocument.Render(currentPage, 300, 300, true);
                return new Bitmap(image);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating page image: {ex.Message}");
                return null;
            }
        }

        public int GetPageCount()
        {
            return _currentDocument?.PageCount ?? 0;
        }

        public int GetCurrentPage()
        {
            return 1; // For now, always return page 1
        }

        public void GoToPreviousPage()
        {
            // Navigation will be implemented later
            System.Diagnostics.Debug.WriteLine("Previous page navigation not yet implemented");
        }

        public void GoToNextPage()
        {
            // Navigation will be implemented later
            System.Diagnostics.Debug.WriteLine("Next page navigation not yet implemented");
        }
    }
}
