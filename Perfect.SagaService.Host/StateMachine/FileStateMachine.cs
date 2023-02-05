using MassTransit;
using Perfect.Messages.Events;

namespace Perfect.SagaService.Host.StateMachine
{
    public class FileStateMachine : MassTransitStateMachine<FileState>
    {
        public FileStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => FileReceivedEvent, x =>
                x.CorrelateBy(y => y.FileName, y => y.Message.FileName)
                 .SelectId(_ => NewId.NextGuid())
            );

            Initially(
                When(FileReceivedEvent)
                .Then(context => 
                    context.Saga.FileName = context.Message.FileName)
                .Then(context => 
                    Console.WriteLine(context.Message.FileName))
                .TransitionTo(FileReceived)
            );
        }

        public State FileReceived { get; set; }

        public Event<FileReceivedEvent> FileReceivedEvent { get; private set; }
    }
}
