using MassTransit;
using Perfect.AnalyzerService.Application.BannedWords.Interfaces;
using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.OddLetters.Interfaces;
using Perfect.Messages.Commands;

namespace Perfect.AnalyzerService.Api.Consumers;

public class AnalyzeFileConsumer : IConsumer<AnalyzeFileCommand>
{
    private readonly IFileServiceClient _fileServiceClient;
    private readonly IOddLetterAnalyzerService _oddLetterAnalyzerService;
    private readonly IBannedWordAnalyzerService _bannedWordAnalyzerService;

    public AnalyzeFileConsumer(IFileServiceClient fileServiceClient, IOddLetterAnalyzerService oddLetterAnalyzerService, IBannedWordAnalyzerService bannedWordAnalyzerService)
    {
        _fileServiceClient = fileServiceClient;
        _oddLetterAnalyzerService = oddLetterAnalyzerService;
        _bannedWordAnalyzerService = bannedWordAnalyzerService;
    }

    public async Task Consume(ConsumeContext<AnalyzeFileCommand> context)
    {
        var message = context.Message;

        // TODO: Handle file not found.
        var file = await _fileServiceClient.GetFileAsync(message.FileName, context.CancellationToken);

        await Task.WhenAll(
            _oddLetterAnalyzerService.ExecuteAsync(new(message.CorrelationId, file.FileName, file.Content), context.CancellationToken),
            _bannedWordAnalyzerService.ExecuteAsync(new(message.CorrelationId, file.FileName, file.Content), context.CancellationToken)
       );
    }
}