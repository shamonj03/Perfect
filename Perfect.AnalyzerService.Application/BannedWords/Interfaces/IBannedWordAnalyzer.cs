namespace Perfect.AnalyzerService.Application.BannedWords.Interfaces
{
    public interface IBannedWordAnalyzer
    {
        int Analyze(string content);
    }
}
