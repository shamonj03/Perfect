using Perfect.AnalyzerService.Application.Common;
using Perfect.AnalyzerService.Application.OddLetters.Interfaces;
using Perfect.AnalyzerService.Application.OddLetters.Models;
using Perfect.Messages.Events;

namespace Perfect.AnalyzerService.Application.OddLetters
{
    public class OddLetterAnalyzerService : IOddLetterAnalyzerService
    {
        private readonly IOddLetterAnalyzer _oddLetterAnalyzer;
        private readonly IMessageSender _messageSender;

        public OddLetterAnalyzerService(IOddLetterAnalyzer oddLetterAnalyzer, IMessageSender messageSender)
        {
            _oddLetterAnalyzer = oddLetterAnalyzer;
            _messageSender = messageSender;
        }

        public Task ExecuteAsync(OddLetterAnalyzerRequest request, CancellationToken cancellationToken)
        {
            var result = _oddLetterAnalyzer.Analyze(request.Content);

            return _messageSender.PublishEventAsync(new OddLettersAnalyzedEvent
            {
                Count = result
            }, cancellationToken);
        }
    }
}
