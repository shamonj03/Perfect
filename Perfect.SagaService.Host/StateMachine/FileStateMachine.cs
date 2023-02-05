using MassTransit;
using Perfect.Messages.Commands;
using Perfect.Messages.Events;

namespace Perfect.SagaService.Host.StateMachine
{
    public class FileStateMachine : MassTransitStateMachine<FileState>
    {
        public FileStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => FileReceived, x =>
                x.CorrelateBy(y => y.FileName, y => y.Message.FileName)
                 .SelectId(_ => NewId.NextGuid())
            );

            Initially(
                When(FileReceived)
                    .Then(context => 
                        context.Saga.FileName = context.Message.FileName)
                    .SendAsync(context => context.Init<AnalyzeFileCommand>(new
                    {
                        FileName = context.Message.FileName
                    }))
                    .TransitionTo(AnalyzeFile)
            );
        }

        public State AnalyzeFile { get; set; }

        public Event<FileReceivedEvent> FileReceived { get; private set; }
    }
}
