namespace Perfect.Messages.Events
{
    public record AnalyzedFileTimeoutExpired
    {
        public Guid FileId { get; init; }
        public string FileName { get; init; } = string.Empty;
    }
}
