using MassTransit;

namespace Perfect.SagaService.Host.StateMachine
{
    public class FileState : SagaStateMachineInstance,
        ISagaVersion
    {
        public Guid CorrelationId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string CurrentState { get; set; } = string.Empty;

        public int Version { get; set; }
    }
}
