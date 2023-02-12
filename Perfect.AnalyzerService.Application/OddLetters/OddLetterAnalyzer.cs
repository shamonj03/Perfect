using Perfect.AnalyzerService.Application.OddLetters.Interfaces;

namespace Perfect.AnalyzerService.Application.OddLetters
{
    public class OddLetterAnalyzer : IOddLetterAnalyzer
    {
        public int Analyze(string content)
        {
            return content.Count(x => x % 2 == 0);
        }
    }
}
