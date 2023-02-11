using MassTransit;
using Perfect.AnalyzerService.Application.OddLetters.Interfaces;
using Perfect.AnalyzerService.Application.OddLetters.Models;
using Perfect.Messages.Commands;

namespace Perfect.AnalyzerService.Api.Consumers;

public class AnalyzeFileConsumer : IConsumer<AnalyzeFileCommand>
{
    private readonly IOddLetterAnalyzerService _oddLetterAnalyzerService;

    public AnalyzeFileConsumer(IOddLetterAnalyzerService oddLetterAnalyzerService)
    {
        _oddLetterAnalyzerService = oddLetterAnalyzerService;
    }

    public Task Consume(ConsumeContext<AnalyzeFileCommand> context)
    {
        return _oddLetterAnalyzerService.ExecuteAsync(new OddLetterAnalyzerRequest(context.Message.FileName), context.CancellationToken);
    }
}