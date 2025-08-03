using System.IO;
using System.Text;

namespace PDFClassifierPro.Core.Utils
{
    public class PdfInspector
    {
        public static bool HasText(string path)
        {
            if (!File.Exists(path)) return false;
            try
            {
                using var stream = File.OpenRead(path);
                var buffer = new byte[1024];
                var text = new StringBuilder();
                var bytesRead = 0;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var chunk = new byte[bytesRead];
                    Array.Copy(buffer, chunk, bytesRead);
                    text.Append(System.Text.Encoding.UTF8.GetString(chunk));
                }
                var textContent = text.ToString();
                return !string.IsNullOrWhiteSpace(textContent) && textContent.Trim().Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
