using MassTransit;
using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.OddLetters.Interfaces;
using Perfect.Messages.Commands;
using System.Text;

namespace Perfect.AnalyzerService.Api.Consumers;

public class AnalyzeFileConsumer : IConsumer<AnalyzeFileCommand>
{
    private readonly IFileServiceClient _fileServiceClient;
    private readonly IOddLetterAnalyzerService _oddLetterAnalyzerService;

    public AnalyzeFileConsumer(IFileServiceClient fileServiceClient, IOddLetterAnalyzerService oddLetterAnalyzerService)
    {
        _fileServiceClient = fileServiceClient;
        _oddLetterAnalyzerService = oddLetterAnalyzerService;
    }

    public async Task Consume(ConsumeContext<AnalyzeFileCommand> context)
    {
        // TODO: Handle file not found.
        var file = await _fileServiceClient.GetFileAsync(context.Message.FileName, context.CancellationToken);
        var content = Encoding.UTF8.GetString(file.Content);
        await _oddLetterAnalyzerService.ExecuteAsync(new (content), context.CancellationToken);
    }
}