using MassTransit;

namespace Perfect.SagaService.Host.StateMachine
{
    public class FileState : SagaStateMachineInstance,
        ISagaVersion
    {
        public Guid CorrelationId { get; set; }

        public string FileName { get; set; }

        public string CurrentState { get; set; }

        public int Version { get; set; }
    }
}
