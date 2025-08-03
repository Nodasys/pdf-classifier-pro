using Xunit;
using PDFClassifierPro.Core.Utils;
using System.IO;

namespace PDFClassifierPro.Tests.Utils
{
    public class PdfHandlerTests
    {
        private readonly PdfHandler _handler;
        private readonly string _testFilePath;

        public PdfHandlerTests()
        {
            _handler = new PdfHandler();
            _testFilePath = Path.GetTempFileName();
        }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            Assert.NotNull(_handler);
        }

        [Fact]
        public void IsScannedDocument_WithNullPath_ShouldReturnFalse()
        {
            var result = _handler.IsScannedDocument(null!);
            Assert.False(result);
        }

        [Fact]
        public void IsScannedDocument_WithNonExistentFile_ShouldReturnFalse()
        {
            var result = _handler.IsScannedDocument("nonexistent.pdf");
            Assert.False(result);
        }

        [Fact]
        public void IsScannedDocument_WithEmptyFile_ShouldReturnTrue()
        {
            File.WriteAllText(_testFilePath, "");
            var result = _handler.IsScannedDocument(_testFilePath);
            Assert.True(result);
        }

        [Fact]
        public void ExtractText_WithNullPath_ShouldReturnEmptyString()
        {
            var result = _handler.ExtractText(null!);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ExtractText_WithNonExistentFile_ShouldReturnEmptyString()
        {
            var result = _handler.ExtractText("nonexistent.pdf");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ExtractText_WithEmptyFile_ShouldReturnEmptyString()
        {
            File.WriteAllText(_testFilePath, "");
            var result = _handler.ExtractText(_testFilePath);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void ExtractText_WithTextFile_ShouldReturnContent()
        {
            var content = "This is test content";
            File.WriteAllText(_testFilePath, content);
            var result = _handler.ExtractText(_testFilePath);
            Assert.Contains(content, result);
        }

        [Fact]
        public void GetTextBounds_WithNullPath_ShouldReturnEmptyList()
        {
            var result = _handler.GetTextBounds(null!);
            Assert.Empty(result);
        }

        [Fact]
        public void GetTextBounds_WithNonExistentFile_ShouldReturnEmptyList()
        {
            var result = _handler.GetTextBounds("nonexistent.pdf");
            Assert.Empty(result);
        }

        [Fact]
        public void GetTextBounds_WithValidFile_ShouldReturnBounds()
        {
            File.WriteAllText(_testFilePath, "test content");
            var result = _handler.GetTextBounds(_testFilePath);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        private void Dispose()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
        }
    }
} 