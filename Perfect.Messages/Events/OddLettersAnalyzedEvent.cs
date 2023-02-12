namespace Perfect.Messages.Events
{
    public record OddLettersAnalyzedEvent
    {
        public Guid FileId { get; init; }
        public string FileName { get; init; } = string.Empty;
        public int Count { get; init; }
    }
}
