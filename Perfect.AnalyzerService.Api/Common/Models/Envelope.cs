using Perfect.AnalyzerService.Api.Common.Interfaces;

namespace Perfect.AnalyzerService.Api.Common.Models
{
    public record Envelope(string Error) : IEnvelope;
}
