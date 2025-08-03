using System.Drawing;
using Tesseract;
using System.IO;

namespace PDFClassifierPro.Core.Ocr
{
    public class OcrEngine
    {
        private TesseractEngine engine;

        public OcrEngine(string tessdataPath = "./Resources/tessdata", string lang = "eng")
        {
            engine = new TesseractEngine(tessdataPath, lang, EngineMode.Default);
        }

        public string RunOcr(Bitmap image)
        {
            using var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var bytes = stream.ToArray();
            using var pix = Pix.LoadFromMemory(bytes);
            using var page = engine.Process(pix);
            return page.GetText();
        }
    }
}
