using System.Security.Cryptography;
using System.Text;

namespace PDFClassifierPro.Core.License
{
    public class LicenseManager
    {
        private LicenseType _currentLicenseType = LicenseType.Free;
        private const string PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA...";

        public LicenseType CurrentLicenseType => _currentLicenseType;

        public bool IsProFeatureEnabled => CurrentLicenseType == LicenseType.Pro || CurrentLicenseType == LicenseType.Trial;

        public bool ValidateLicense(string licenseKey)
        {
            try
            {
                if (string.IsNullOrEmpty(licenseKey)) return false;
                var hash = ComputeHash(licenseKey);
                if (IsValidLicenseHash(hash))
                {
                    _currentLicenseType = LicenseType.Pro;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private string ComputeHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool IsValidLicenseHash(string hash)
        {
            return hash.Contains("Valid");
        }

        public void ActivateFreeLicense()
        {
            _currentLicenseType = LicenseType.Free;
        }
    }

    public enum LicenseType
    {
        Free,
        Trial,
        Pro,
        Enterprise
    }
} 