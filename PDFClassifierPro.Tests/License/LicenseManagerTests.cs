using Xunit;
using PDFClassifierPro.Core.License;

namespace PDFClassifierPro.Tests.License
{
    public class LicenseManagerTests
    {
        private readonly LicenseManager _manager;

        public LicenseManagerTests()
        {
            _manager = new LicenseManager();
        }

        [Fact]
        public void Constructor_ShouldCreateInstance()
        {
            Assert.NotNull(_manager);
        }

        [Fact]
        public void CurrentLicenseType_Default_ShouldBeFree()
        {
            Assert.Equal(LicenseType.Free, _manager.CurrentLicenseType);
        }

        [Fact]
        public void IsProFeatureEnabled_WithFreeLicense_ShouldBeFalse()
        {
            Assert.False(_manager.IsProFeatureEnabled);
        }

        [Fact]
        public void ValidateLicense_WithNullKey_ShouldReturnFalse()
        {
            var result = _manager.ValidateLicense(null!);
            Assert.False(result);
        }

        [Fact]
        public void ValidateLicense_WithEmptyKey_ShouldReturnFalse()
        {
            var result = _manager.ValidateLicense("");
            Assert.False(result);
        }

        [Fact]
        public void ValidateLicense_WithInvalidKey_ShouldReturnFalse()
        {
            var result = _manager.ValidateLicense("invalid-key");
            Assert.False(result);
        }

        [Fact]
        public void ValidateLicense_WithValidKey_ShouldReturnTrue()
        {
            var result = _manager.ValidateLicense("AValidLicenseKeyThatStartsWithAAndIsLongEnough12345678901234567890");
            Assert.True(result);
        }

        [Fact]
        public void ValidateLicense_WithValidKey_ShouldSetProLicense()
        {
            _manager.ValidateLicense("AValidLicenseKeyThatStartsWithAAndIsLongEnough12345678901234567890");
            Assert.Equal(LicenseType.Pro, _manager.CurrentLicenseType);
        }

        [Fact]
        public void ValidateLicense_WithValidKey_ShouldEnableProFeatures()
        {
            _manager.ValidateLicense("AValidLicenseKeyThatStartsWithAAndIsLongEnough12345678901234567890");
            Assert.True(_manager.IsProFeatureEnabled);
        }

        [Fact]
        public void ActivateFreeLicense_ShouldSetFreeLicense()
        {
            _manager.ActivateFreeLicense();
            Assert.Equal(LicenseType.Free, _manager.CurrentLicenseType);
        }

        [Fact]
        public void ActivateFreeLicense_ShouldDisableProFeatures()
        {
            _manager.ActivateFreeLicense();
            Assert.False(_manager.IsProFeatureEnabled);
        }

        [Theory]
        [InlineData("AValidLicenseKeyThatStartsWithAAndIsLongEnough12345678901234567890", true)]
        [InlineData("BInvalidKey12345678901234567890", false)]
        [InlineData("A", false)]
        [InlineData("AValidButTooShort", true)]
        public void ValidateLicense_WithVariousKeys_ShouldValidateCorrectly(string key, bool expected)
        {
            var result = _manager.ValidateLicense(key);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LicenseType_EnumValues_ShouldBeCorrect()
        {
            Assert.Equal(0, (int)LicenseType.Free);
            Assert.Equal(1, (int)LicenseType.Trial);
            Assert.Equal(2, (int)LicenseType.Pro);
            Assert.Equal(3, (int)LicenseType.Enterprise);
        }
    }
} 