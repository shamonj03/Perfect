using Perfect.AnalyzerService.Application.BannedWords.Models;

namespace Perfect.AnalyzerService.Application.BannedWords.Interfaces
{
    public interface IBannedWordAnalyzerService
    {
        Task ExecuteAsync(BannedWordAnalyzerRequest request, CancellationToken cancellationToken);
    }
}
