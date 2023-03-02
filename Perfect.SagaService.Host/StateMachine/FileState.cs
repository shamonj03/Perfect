using MassTransit;

namespace Perfect.SagaService.Host.StateMachine
{
    public class FileState : SagaStateMachineInstance,
        ISagaVersion
    {
        public int Version { get; set; }

        public Guid CorrelationId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string? CurrentState { get; set; }

        public int FileAnalyzedStatus { get; set; }

        public int? OddLetterCount { get; set; }

        public int? BannedWordCount { get; set; }

        public Guid? AnalyzedFileTimeoutTokenId { get; set; }
    }
}
