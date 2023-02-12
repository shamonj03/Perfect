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

            State(() => AnalyzeFile);
            Event(() => FileReceived, x => x.CorrelateById(y => y.Message.FileId));
            Event(() => OddLettersAnalyzed, x => x.CorrelateById(y => y.Message.FileId));
            Event(() => BannedWordsAnalzyed, x => x.CorrelateById(y => y.Message.FileId));

            Initially(
                When(FileReceived)
                    .Then(context => 
                        context.Saga.FileName = context.Message.FileName)
                    .SendAsync(context => context.Init<AnalyzeFileCommand>(new
                    {
                        CorrelationId = context.CorrelationId,
                        FileName = context.Message.FileName
                    }))
                    .TransitionTo(AnalyzeFile)
            );

            CompositeEvent(FileAnalzed, x => x.FileAnalyzedStatus,
                OddLettersAnalyzed, BannedWordsAnalzyed);

            During(AnalyzeFile,
                When(OddLettersAnalyzed)
                    .Then(context => context.Saga.OddLetterCount = context.Message.Count),
                When(BannedWordsAnalzyed)
                    .Then(context => context.Saga.BannedWordCount = context.Message.Count),
                When(FileAnalzed)
                    .Then(_ =>
                        Console.WriteLine("We did it!"))
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }

        public State? AnalyzeFile { get; set; }

        public Event<FileReceivedEvent>? FileReceived { get; set; }
        public Event<OddLettersAnalyzedEvent>? OddLettersAnalyzed { get; set; }
        public Event<BannedWordsAnalzyedEvent>? BannedWordsAnalzyed { get; set; }

        public Event? FileAnalzed { get; set; }
    }
}
