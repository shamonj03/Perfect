using Perfect.AnalyzerService.Application.Common;
using Perfect.AnalyzerService.Application.FileService;
using Perfect.AnalyzerService.Application.OddLetters.Interfaces;
using Perfect.AnalyzerService.Application.OddLetters.Models;
using Perfect.Messages.Events;
using System.Text;

namespace Perfect.AnalyzerService.Application.OddLetters
{
    public class OddLetterAnalyzerService : IOddLetterAnalyzerService
    {
        private readonly IFileServiceClient _fileServiceClient;
        private readonly IOddLetterAnalyzer _oddLetterAnalyzer;
        private readonly IMessageSender _messageSender;

        public OddLetterAnalyzerService(IFileServiceClient fileServiceClient, IOddLetterAnalyzer oddLetterAnalyzer, IMessageSender messageSender)
        {
            _fileServiceClient = fileServiceClient;
            _oddLetterAnalyzer = oddLetterAnalyzer;
            _messageSender = messageSender;
        }

        public async Task ExecuteAsync(OddLetterAnalyzerRequest request, CancellationToken cancellationToken)
        {
            // TODO: Handle file not found.
            var file = await _fileServiceClient.GetFileAsync(request.FileName, cancellationToken);
            var content = Encoding.UTF8.GetString(file.Content);
            var result = _oddLetterAnalyzer.Analyze(content);

            await _messageSender.PublishEventAsync(new OddLettersAnalyzedEvent
            {
                Count = result
            }, cancellationToken);
        }
    }
}
