using System.Windows.Controls;
using System.Windows.Forms.Integration;
using PdfiumViewer;
using System.IO;
using PDFClassifierPro.Core.Ocr;
using PDFClassifierPro.Core.Utils;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;

namespace PDFClassifierPro.UI.Views.Controls
{
    public partial class PdfViewerControl : global::System.Windows.Controls.UserControl
    {
        private PdfViewer _viewer;

        public PdfViewerControl()
        {
            InitializeComponent();

            _viewer = new PdfViewer
            {
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            var host = new WindowsFormsHost
            {
                Child = _viewer
            };

            winFormsHost.Child = _viewer;
        }

        public void LoadPdf(string path)
        {
            if (!File.Exists(path)) return;

            _viewer.Document?.Dispose();
            _viewer.Document = PdfiumViewer.PdfDocument.Load(path);

            if (!PdfInspector.HasText(path))
            {
                try
                {
                    var bmp = GetCurrentPageImage();
                    if (bmp != null)
                    {
                        var ocr = new OcrEngine();
                        var text = ocr.RunOcr(bmp);
                        System.Diagnostics.Debug.WriteLine("[OCR] " + text);
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.WriteLine("[OCR] Failed to process image");
                }
            }
        }

        public Bitmap? GetCurrentPageImage()
        {
            if (_viewer.Document == null) return null;
            
            try
            {
                var currentPage = 0;
                using var img = _viewer.Document.Render(currentPage, 300, 300, true);
                return new Bitmap(img);
            }
            catch
            {
                return null;
            }
        }
    }
}
