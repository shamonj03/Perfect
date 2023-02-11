using Perfect.AnalyzerService.Application.OddLetters.Models;

namespace Perfect.AnalyzerService.Application.OddLetters.Interfaces
{
    public interface IOddLetterAnalyzerService
    {
        Task ExecuteAsync(OddLetterAnalyzerRequest request, CancellationToken cancellationToken);
    }
}
