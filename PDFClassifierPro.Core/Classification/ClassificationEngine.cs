using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace PDFClassifierPro.Core.Classification
{
    public class ClassificationEngine
    {
        public ClassificationLevel AnalyzeDocument(string text)
        {
            if (string.IsNullOrEmpty(text))
                return ClassificationLevel.Unclassified;

            var patterns = new Dictionary<ClassificationLevel, string[]>
            {
                { ClassificationLevel.TopSecret, new[] { "TOP SECRET", "CLASSIFIED", "RESTRICTED" } },
                { ClassificationLevel.Secret, new[] { "SECRET", "SENSITIVE" } },
                { ClassificationLevel.Confidential, new[] { "CONFIDENTIAL", "INTERNAL USE", "PRIVATE" } },
                { ClassificationLevel.Unclassified, new[] { "UNCLASSIFIED", "PUBLIC", "OPEN" } }
            };

            foreach (var pattern in patterns)
            {
                if (pattern.Value.Any(keyword => Regex.IsMatch(text, keyword, RegexOptions.IgnoreCase)))
                {
                    return pattern.Key;
                }
            }

            return ClassificationLevel.Unclassified;
        }

        public List<string> ExtractSensitiveTerms(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new List<string>();

            var sensitivePatterns = new[]
            {
                @"\b\d{3}-\d{2}-\d{4}\b",
                @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b",
                @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"
            };

            var terms = new List<string>();
            foreach (var pattern in sensitivePatterns)
            {
                var matches = Regex.Matches(text, pattern);
                terms.AddRange(matches.Select(m => m.Value));
            }

            return terms.Distinct().ToList();
        }
    }

    public enum ClassificationLevel
    {
        Unclassified,
        Confidential,
        Secret,
        TopSecret
    }
} 