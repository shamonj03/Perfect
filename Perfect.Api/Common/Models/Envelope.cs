using Perfect.Api.Common.Interfaces;

namespace Perfect.Api.Common.Models
{
    public record Envelope(string Error) : IEnvelope;
}
