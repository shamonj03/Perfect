using Perfect.AnalyzerService.Application.BannedWords.Interfaces;
using System.Text.RegularExpressions;

namespace Perfect.AnalyzerService.Application.BannedWords
{
    public class BannedWordAnalyzer : IBannedWordAnalyzer
    {
        private const string BannedWordPattern = "uwu";

        public int Analyze(string content)
        {
            return Regex.Matches(content, BannedWordPattern).Count;
        }
    }
}
