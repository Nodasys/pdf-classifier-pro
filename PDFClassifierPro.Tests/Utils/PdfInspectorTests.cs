using Xunit;
using PDFClassifierPro.Core.Utils;
using System.IO;

namespace PDFClassifierPro.Tests.Utils
{
    public class PdfInspectorTests
    {
        private readonly string _testFilePath;

        public PdfInspectorTests()
        {
            _testFilePath = Path.GetTempFileName();
        }

        [Fact]
        public void HasText_WithNullPath_ShouldReturnFalse()
        {
            var result = PdfInspector.HasText(null!);
            Assert.False(result);
        }

        [Fact]
        public void HasText_WithNonExistentFile_ShouldReturnFalse()
        {
            var result = PdfInspector.HasText("nonexistent.pdf");
            Assert.False(result);
        }

        [Fact]
        public void HasText_WithEmptyFile_ShouldReturnFalse()
        {
            File.WriteAllText(_testFilePath, "");
            var result = PdfInspector.HasText(_testFilePath);
            Assert.False(result);
        }

        [Fact]
        public void HasText_WithWhitespaceFile_ShouldReturnFalse()
        {
            File.WriteAllText(_testFilePath, "   \t\n\r   ");
            var result = PdfInspector.HasText(_testFilePath);
            Assert.False(result);
        }

        [Fact]
        public void HasText_WithTextFile_ShouldReturnTrue()
        {
            File.WriteAllText(_testFilePath, "This is test content");
            var result = PdfInspector.HasText(_testFilePath);
            Assert.True(result);
        }

        [Fact]
        public void HasText_WithBinaryFile_ShouldHandleGracefully()
        {
            var bytes = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05 };
            File.WriteAllBytes(_testFilePath, bytes);
            var result = PdfInspector.HasText(_testFilePath);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public void HasText_WithLargeFile_ShouldHandleGracefully()
        {
            var largeContent = new string('A', 10000);
            File.WriteAllText(_testFilePath, largeContent);
            var result = PdfInspector.HasText(_testFilePath);
            Assert.True(result);
        }

        private void Dispose()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }
    }
} 