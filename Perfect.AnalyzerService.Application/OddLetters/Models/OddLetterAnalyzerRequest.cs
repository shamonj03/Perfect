namespace Perfect.AnalyzerService.Application.OddLetters.Models
{
    public record OddLetterAnalyzerRequest(Guid CorrelationId,string FileName, string Content);
}
