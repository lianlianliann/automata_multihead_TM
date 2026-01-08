using System.Text.RegularExpressions;
using TM_MULTIHEAD_PHISHING_DETECTOR.Models;

public class MHTMEngine
{
    public AnalysisResult Process(string text)
    {
        var result = new AnalysisResult();

        // Store original user input
        result.OriginalText = text;

        // 🔹 Preprocess text before analysis
        string cleanText = PreprocessText(text);
        result.ProcessedText = cleanText;

        // Run all heads using preprocessed text
        var (score1, t1) = new Head1().Run(cleanText);
        var (score2, t2) = new Head2().Run(cleanText);
        var (score3, t3) = new Head3().Run(cleanText);

        // Store individual normalized scores
        result.Head1Score = score1;
        result.Head2Score = score2;
        result.Head3Score = score3;

        // Store trigger details
        result.Head1Triggers = t1;
        result.Head2Triggers = t2;
        result.Head3Triggers = t3;

        // Apply paper-defined weights
        double head1Contribution = score1 * 0.40;
        double head2Contribution = score2 * 0.30;
        double head3Contribution = score3 * 0.30;

        // Final normalized score (0.0–1.0)
        double finalNormalizedScore =
            head1Contribution + head2Contribution + head3Contribution;

        // Convert to percentage (0–100)
        result.ConfidenceScore = finalNormalizedScore * 100;

        // Classification threshold 
        // 0 - 49 = CREDIBLE
        // 50 - 100 = SUSPICIOUS
        result.Classification = result.ConfidenceScore >= 50
            ? "Suspicious"
            : "Credible";

        return result;
    }

    // 🔹 Centralized preprocessing logic
    private string PreprocessText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        // Remove URLs
        text = Regex.Replace(
            text,
            @"https?:\/\/\S+|www\.\S+",
            "",
            RegexOptions.IgnoreCase
        );

        // Remove numbers
        text = Regex.Replace(text, @"\d+", "");

        // Remove emojis and special unicode symbols
        text = Regex.Replace(
            text,
            @"[\p{Cs}\p{So}\p{Sk}]+",
            ""
        );

        // Normalize whitespace
        text = Regex.Replace(text, @"\s+", " ").Trim();

        return text;
    }
}
