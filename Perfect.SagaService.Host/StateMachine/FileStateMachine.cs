using DnsClient.Internal;
using MassTransit;
using Microsoft.Extensions.Logging;
using Perfect.Messages.Commands;
using Perfect.Messages.Events;

namespace Perfect.SagaService.Host.StateMachine
{
    public class FileStateMachine : MassTransitStateMachine<FileState>
    {
        public FileStateMachine(ILogger<FileStateMachine> logger)
        {
            InstanceState(x => x.CurrentState);

            Event(() => FileReceived, x => 
                x.CorrelateBy(y => y.FileName, y => y.Message.FileName)
                    .SelectId(y => y.CorrelationId ?? NewId.NextGuid())
            );
            Event(() => OddLettersAnalyzed, x => x.CorrelateById(y => y.Message.FileId));
            Event(() => BannedWordsAnalzyed, x => x.CorrelateById(y => y.Message.FileId));

            Schedule(() => AnalyzedFileTimeout, 
                instance => instance.AnalyzedFileTimeoutTokenId, s => 
                { 
                    s.Delay = TimeSpan.FromSeconds(5); 
                    s.Received = r => r.CorrelateById(context => context.Message.FileId); 
                }
            );

            CompositeEvent(() => FileAnalzed, x => x.FileAnalyzedStatus, OddLettersAnalyzed, BannedWordsAnalzyed);

            Initially(
                When(FileReceived)
                    .Then(context => context.Saga.FileName = context.Message.FileName)
                    .SendAsync(context => context.Init<AnalyzeFileCommand>(new
                    {
                        CorrelationId = context.CorrelationId,
                        FileName = context.Message.FileName

                    }))
                    .Schedule(AnalyzedFileTimeout, context =>
                        context.Init<AnalyzedFileTimeoutExpired>(new 
                        {
                            FileId = context.Saga.CorrelationId, 
                            FileName = context.Saga.FileName 
                        })
                    )
                    .TransitionTo(AnalyzeFile)
            );

            During(AnalyzeFile,
                Ignore(FileReceived),
                When(AnalyzedFileTimeout.Received)
                    .Then(context =>
                        Console.Write("Analyzing files timed out...")
                    )
                    .Finalize(),
                When(OddLettersAnalyzed)
                    .Then(context => context.Saga.OddLetterCount = context.Message.Count),
                When(BannedWordsAnalzyed)
                    .Then(context => context.Saga.BannedWordCount = context.Message.Count), 
                When(FileAnalzed)
                    .Unschedule(AnalyzedFileTimeout).
                    Then(x =>
                        Console.WriteLine("We did it!")
                    )
                    .Finalize()
            );

            During(Final, Ignore(FileReceived));
            //SetCompletedWhenFinalized();
        }

        public State? AnalyzeFile { get; set; }

        public Event<FileReceivedEvent>? FileReceived { get; set; }
        public Event<OddLettersAnalyzedEvent>? OddLettersAnalyzed { get; set; }
        public Event<BannedWordsAnalzyedEvent>? BannedWordsAnalzyed { get; set; }

        public Schedule<FileState, AnalyzedFileTimeoutExpired> AnalyzedFileTimeout { get; private set; }

        public Event? FileAnalzed { get; set; }
    }
}
