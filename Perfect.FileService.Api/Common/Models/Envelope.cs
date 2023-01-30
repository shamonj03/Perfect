using Perfect.FileService.Api.Common.Interfaces;

namespace Perfect.FileService.Api.Common.Models
{
    public record Envelope(string Error) : IEnvelope;
}
