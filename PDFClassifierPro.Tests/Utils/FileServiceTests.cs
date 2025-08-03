using Xunit;
using PDFClassifierPro.Core.Utils;
using System.IO;

namespace PDFClassifierPro.Tests.Utils
{
    public class FileServiceTests
    {
        private readonly FileService _service;
        private readonly string _testFilePath;

        public FileServiceTests()
        {
            _service = new FileService();
            _testFilePath = Path.GetTempFileName();
        }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            Assert.NotNull(_service);
        }

        [Fact]
        public void IsValidPdfFile_WithNullPath_ShouldReturnFalse()
        {
            var result = _service.IsValidPdfFile(null!);
            Assert.False(result);
        }

        [Fact]
        public void IsValidPdfFile_WithNonExistentFile_ShouldReturnFalse()
        {
            var result = _service.IsValidPdfFile("nonexistent.pdf");
            Assert.False(result);
        }

        [Fact]
        public void IsValidPdfFile_WithValidPdfHeader_ShouldReturnTrue()
        {
            var pdfHeader = new byte[] { 0x25, 0x50, 0x44, 0x46 };
            File.WriteAllBytes(_testFilePath, pdfHeader);
            var result = _service.IsValidPdfFile(_testFilePath);
            Assert.True(result);
        }

        [Fact]
        public void IsValidPdfFile_WithInvalidHeader_ShouldReturnFalse()
        {
            var invalidHeader = new byte[] { 0x00, 0x01, 0x02, 0x03 };
            File.WriteAllBytes(_testFilePath, invalidHeader);
            var result = _service.IsValidPdfFile(_testFilePath);
            Assert.False(result);
        }

        [Fact]
        public void GetFileHash_WithNullPath_ShouldReturnEmptyString()
        {
            var result = _service.GetFileHash(null!);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetFileHash_WithNonExistentFile_ShouldReturnEmptyString()
        {
            var result = _service.GetFileHash("nonexistent.pdf");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void GetFileHash_WithValidFile_ShouldReturnHash()
        {
            File.WriteAllText(_testFilePath, "test content");
            var result = _service.GetFileHash(_testFilePath);
            Assert.NotEmpty(result);
            Assert.True(result.Length > 20);
        }

        [Fact]
        public void GetFileHash_WithSameContent_ShouldReturnSameHash()
        {
            var content = "test content";
            File.WriteAllText(_testFilePath, content);
            var hash1 = _service.GetFileHash(_testFilePath);
            
            var testFile2 = Path.GetTempFileName();
            File.WriteAllText(testFile2, content);
            var hash2 = _service.GetFileHash(testFile2);
            
            Assert.Equal(hash1, hash2);
            
            if (File.Exists(testFile2))
                File.Delete(testFile2);
        }

        [Fact]
        public void CreateBackup_WithNullPath_ShouldReturnFalse()
        {
            var result = _service.CreateBackup(null!);
            Assert.False(result);
        }

        [Fact]
        public void CreateBackup_WithNonExistentFile_ShouldReturnFalse()
        {
            var result = _service.CreateBackup("nonexistent.pdf");
            Assert.False(result);
        }

        [Fact]
        public void CreateBackup_WithValidFile_ShouldReturnTrue()
        {
            File.WriteAllText(_testFilePath, "test content");
            var result = _service.CreateBackup(_testFilePath);
            Assert.True(result);
            Assert.True(File.Exists(_testFilePath + ".backup"));
        }

        [Fact]
        public void CreateBackup_ShouldCreateBackupFile()
        {
            var content = "test content";
            File.WriteAllText(_testFilePath, content);
            _service.CreateBackup(_testFilePath);
            
            var backupPath = _testFilePath + ".backup";
            Assert.True(File.Exists(backupPath));
            Assert.Equal(content, File.ReadAllText(backupPath));
        }

        [Fact]
        public void GetSafeOutputPath_WithNullInput_ShouldHandleGracefully()
        {
            var result = _service.GetSafeOutputPath(null!);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSafeOutputPath_WithValidPath_ShouldReturnPath()
        {
            var result = _service.GetSafeOutputPath(_testFilePath);
            Assert.NotNull(result);
            Assert.Contains("_redacted", result);
        }

        [Fact]
        public void GetSafeOutputPath_WithCustomSuffix_ShouldUseSuffix()
        {
            var result = _service.GetSafeOutputPath(_testFilePath, "_custom");
            Assert.Contains("_custom", result);
        }

        [Fact]
        public void GetSafeOutputPath_WithExistingFile_ShouldIncrementCounter()
        {
            var outputPath = _service.GetSafeOutputPath(_testFilePath);
            File.WriteAllText(outputPath, "existing file");
            
            var newOutputPath = _service.GetSafeOutputPath(_testFilePath);
            Assert.NotEqual(outputPath, newOutputPath);
            Assert.Contains("_1", newOutputPath);
        }

        private void Dispose()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
            if (File.Exists(_testFilePath + ".backup"))
                File.Delete(_testFilePath + ".backup");
        }
    }
} 