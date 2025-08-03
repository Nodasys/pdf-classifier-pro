using Xunit;
using PDFClassifierPro.Core.Redaction;
using System.Drawing;
using System.IO;

namespace PDFClassifierPro.Tests.Redaction
{
    public class RedactionEngineTests
    {
        private readonly RedactionEngine _engine;
        private readonly string _testFilePath;
        private readonly string _outputFilePath;

        public RedactionEngineTests()
        {
            _engine = new RedactionEngine();
            _testFilePath = Path.GetTempFileName() + ".pdf";
            _outputFilePath = Path.GetTempFileName() + "_redacted.pdf";
        }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            Assert.NotNull(_engine);
        }

        [Fact]
        public void ApplyVisualRedaction_WithNullInputPath_ShouldNotThrow()
        {
            var areas = new List<RectangleF> { new RectangleF(10, 10, 50, 20) };
            _engine.ApplyVisualRedaction(null!, _outputFilePath, areas);
        }

        [Fact]
        public void ApplyVisualRedaction_WithNonExistentFile_ShouldNotThrow()
        {
            var areas = new List<RectangleF> { new RectangleF(10, 10, 50, 20) };
            _engine.ApplyVisualRedaction("nonexistent.pdf", _outputFilePath, areas);
        }

        [Fact]
        public void ApplyVisualRedaction_WithEmptyAreas_ShouldNotThrow()
        {
            var areas = new List<RectangleF>();
            _engine.ApplyVisualRedaction(_testFilePath, _outputFilePath, areas);
        }

        [Fact]
        public void ApplyTrueRedaction_WithNullInputPath_ShouldNotThrow()
        {
            var areas = new List<RectangleF> { new RectangleF(10, 10, 50, 20) };
            _engine.ApplyTrueRedaction(null!, _outputFilePath, areas);
        }

        [Fact]
        public void ApplyTrueRedaction_WithNonExistentFile_ShouldNotThrow()
        {
            var areas = new List<RectangleF> { new RectangleF(10, 10, 50, 20) };
            _engine.ApplyTrueRedaction("nonexistent.pdf", _outputFilePath, areas);
        }

        [Fact]
        public void ApplyTrueRedaction_WithEmptyAreas_ShouldNotThrow()
        {
            var areas = new List<RectangleF>();
            _engine.ApplyTrueRedaction(_testFilePath, _outputFilePath, areas);
        }

        [Fact]
        public void ApplyVisualRedaction_WithValidAreas_ShouldProcessAreas()
        {
            var areas = new List<RectangleF> 
            { 
                new RectangleF(10, 10, 50, 20),
                new RectangleF(100, 100, 30, 15)
            };
            _engine.ApplyVisualRedaction(_testFilePath, _outputFilePath, areas);
        }

        [Fact]
        public void ApplyTrueRedaction_WithValidAreas_ShouldProcessAreas()
        {
            var areas = new List<RectangleF> 
            { 
                new RectangleF(10, 10, 50, 20),
                new RectangleF(100, 100, 30, 15)
            };
            _engine.ApplyTrueRedaction(_testFilePath, _outputFilePath, areas);
        }

        private void Dispose()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
            if (File.Exists(_outputFilePath))
                File.Delete(_outputFilePath);
        }
    }
} 