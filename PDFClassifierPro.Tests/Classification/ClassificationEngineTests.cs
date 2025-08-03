using Xunit;
using PDFClassifierPro.Core.Classification;

namespace PDFClassifierPro.Tests.Classification
{
    public class ClassificationEngineTests
    {
        private readonly ClassificationEngine _engine;

        public ClassificationEngineTests()
        {
            _engine = new ClassificationEngine();
        }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            Assert.NotNull(_engine);
        }

        [Fact]
        public void AnalyzeDocument_WithTopSecretText_ShouldReturnTopSecret()
        {
            var text = "This document contains TOP SECRET information";
            var result = _engine.AnalyzeDocument(text);
            Assert.Equal(ClassificationLevel.TopSecret, result);
        }

        [Fact]
        public void AnalyzeDocument_WithSecretText_ShouldReturnSecret()
        {
            var text = "This document contains SECRET information";
            var result = _engine.AnalyzeDocument(text);
            Assert.Equal(ClassificationLevel.Secret, result);
        }

        [Fact]
        public void AnalyzeDocument_WithConfidentialText_ShouldReturnConfidential()
        {
            var text = "This document contains CONFIDENTIAL information";
            var result = _engine.AnalyzeDocument(text);
            Assert.Equal(ClassificationLevel.Confidential, result);
        }

        [Fact]
        public void AnalyzeDocument_WithUnclassifiedText_ShouldReturnUnclassified()
        {
            var text = "This document contains PUBLIC information";
            var result = _engine.AnalyzeDocument(text);
            Assert.Equal(ClassificationLevel.Unclassified, result);
        }

        [Fact]
        public void AnalyzeDocument_WithEmptyText_ShouldReturnUnclassified()
        {
            var result = _engine.AnalyzeDocument("");
            Assert.Equal(ClassificationLevel.Unclassified, result);
        }

        [Fact]
        public void AnalyzeDocument_WithNullText_ShouldReturnUnclassified()
        {
            var result = _engine.AnalyzeDocument(null!);
            Assert.Equal(ClassificationLevel.Unclassified, result);
        }

        [Fact]
        public void AnalyzeDocument_WithNoKeywords_ShouldReturnUnclassified()
        {
            var text = "This is a regular document with no classification keywords";
            var result = _engine.AnalyzeDocument(text);
            Assert.Equal(ClassificationLevel.Unclassified, result);
        }

        [Fact]
        public void ExtractSensitiveTerms_WithSSN_ShouldExtractSSN()
        {
            var text = "My SSN is 123-45-6789";
            var result = _engine.ExtractSensitiveTerms(text);
            Assert.Contains("123-45-6789", result);
        }

        [Fact]
        public void ExtractSensitiveTerms_WithEmail_ShouldExtractEmail()
        {
            var text = "Contact me at test@example.com";
            var result = _engine.ExtractSensitiveTerms(text);
            Assert.Contains("test@example.com", result);
        }

        [Fact]
        public void ExtractSensitiveTerms_WithIPAddress_ShouldExtractIP()
        {
            var text = "Server IP is 192.168.1.1";
            var result = _engine.ExtractSensitiveTerms(text);
            Assert.Contains("192.168.1.1", result);
        }

        [Fact]
        public void ExtractSensitiveTerms_WithMultipleTerms_ShouldExtractAll()
        {
            var text = "SSN: 123-45-6789, Email: test@example.com, IP: 192.168.1.1";
            var result = _engine.ExtractSensitiveTerms(text);
            Assert.Equal(3, result.Count);
            Assert.Contains("123-45-6789", result);
            Assert.Contains("test@example.com", result);
            Assert.Contains("192.168.1.1", result);
        }

        [Fact]
        public void ExtractSensitiveTerms_WithNoTerms_ShouldReturnEmptyList()
        {
            var text = "This text contains no sensitive information";
            var result = _engine.ExtractSensitiveTerms(text);
            Assert.Empty(result);
        }

        [Fact]
        public void ExtractSensitiveTerms_WithEmptyText_ShouldReturnEmptyList()
        {
            var result = _engine.ExtractSensitiveTerms("");
            Assert.Empty(result);
        }

        [Fact]
        public void ExtractSensitiveTerms_WithNullText_ShouldReturnEmptyList()
        {
            var result = _engine.ExtractSensitiveTerms(null!);
            Assert.Empty(result);
        }
    }
} 