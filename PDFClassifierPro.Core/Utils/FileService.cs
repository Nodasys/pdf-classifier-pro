using System.IO;
using System.Security.Cryptography;

namespace PDFClassifierPro.Core.Utils
{
    public class FileService
    {
        public bool IsValidPdfFile(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            
            try
            {
                using var stream = File.OpenRead(filePath);
                var buffer = new byte[4];
                stream.Read(buffer, 0, 4);
                
                return buffer[0] == 0x25 && buffer[1] == 0x50 && 
                       buffer[2] == 0x44 && buffer[3] == 0x46;
            }
            catch
            {
                return false;
            }
        }

        public string GetFileHash(string filePath)
        {
            if (!File.Exists(filePath)) return string.Empty;
            
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha256.ComputeHash(stream);
            return Convert.ToBase64String(hash);
        }

        public bool CreateBackup(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            
            var backupPath = filePath + ".backup";
            try
            {
                File.Copy(filePath, backupPath, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetSafeOutputPath(string inputPath, string suffix = "_redacted")
        {
            var directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
            var fileName = Path.GetFileNameWithoutExtension(inputPath);
            var extension = Path.GetExtension(inputPath);
            
            var outputFileName = fileName + suffix + extension;
            var outputPath = Path.Combine(directory, outputFileName);
            
            var counter = 1;
            while (File.Exists(outputPath))
            {
                outputFileName = fileName + suffix + $"_{counter}" + extension;
                outputPath = Path.Combine(directory, outputFileName);
                counter++;
            }
            
            return outputPath;
        }
    }
} 