namespace Perfect.Messages.Commands;

public record AnalyzeFileCommand
{
    public Guid CorrelationId { get; init; }
    public string FileName { get; init; } = string.Empty;
}