namespace Perfect.AnalyzerService.Application.BannedWords.Models
{
    public record BannedWordAnalyzerRequest(Guid CorrelationId, string FileName, string Content);
}