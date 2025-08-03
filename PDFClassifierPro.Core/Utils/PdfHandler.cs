using System.Drawing;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace PDFClassifierPro.Core.Utils
{
    public class PdfHandler
    {
        public bool IsScannedDocument(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            try
            {
                var text = ExtractText(filePath);
                return string.IsNullOrWhiteSpace(text);
            }
            catch
            {
                return true;
            }
        }

        public string ExtractText(string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;
            try
            {
                using var stream = File.OpenRead(filePath);
                var buffer = new byte[1024];
                var text = new StringBuilder();
                while (stream.Read(buffer, 0, buffer.Length) > 0)
                {
                    text.Append(System.Text.Encoding.UTF8.GetString(buffer));
                }
                return text.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public List<RectangleF> GetTextBounds(string filePath)
        {
            var bounds = new List<RectangleF>();
            if (!File.Exists(filePath)) return bounds;
            try
            {
                bounds.Add(new RectangleF(100, 100, 200, 50));
                bounds.Add(new RectangleF(300, 200, 150, 30));
            }
            catch
            {
            }
            return bounds;
        }
    }
} 