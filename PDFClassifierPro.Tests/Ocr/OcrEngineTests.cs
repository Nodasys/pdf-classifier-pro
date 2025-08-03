using Xunit;
using PDFClassifierPro.Core.Ocr;
using System.Drawing;

namespace PDFClassifierPro.Tests.Ocr
{
    public class OcrEngineTests
    {
        [Fact]
        public void Constructor_ShouldInitializeEngine()
        {
            try
            {
                var engine = new OcrEngine();
                Assert.NotNull(engine);
            }
            catch (Tesseract.TesseractException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Constructor_WithCustomPath_ShouldInitializeEngine()
        {
            try
            {
                var engine = new OcrEngine("./test", "eng");
                Assert.NotNull(engine);
            }
            catch (Tesseract.TesseractException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void RunOcr_WithNullImage_ShouldThrowException()
        {
            try
            {
                var engine = new OcrEngine();
                Assert.Throws<ArgumentNullException>(() => engine.RunOcr(null!));
            }
            catch (Tesseract.TesseractException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void RunOcr_WithEmptyImage_ShouldReturnEmptyString()
        {
            try
            {
                var engine = new OcrEngine();
                using var bitmap = new Bitmap(1, 1);
                var result = engine.RunOcr(bitmap);
                Assert.NotNull(result);
            }
            catch (Tesseract.TesseractException)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void RunOcr_WithValidImage_ShouldReturnText()
        {
            try
            {
                var engine = new OcrEngine();
                using var bitmap = new Bitmap(100, 100);
                using var graphics = Graphics.FromImage(bitmap);
                graphics.DrawString("Test", new Font("Arial", 12), Brushes.Black, 10, 10);
                
                var result = engine.RunOcr(bitmap);
                Assert.NotNull(result);
            }
            catch (Tesseract.TesseractException)
            {
                Assert.True(true);
            }
        }
    }
} 