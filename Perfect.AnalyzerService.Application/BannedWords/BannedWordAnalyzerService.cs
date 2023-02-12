using Perfect.AnalyzerService.Application.BannedWords.Interfaces;
using Perfect.AnalyzerService.Application.BannedWords.Models;
using Perfect.AnalyzerService.Application.Common;
using Perfect.Messages.Events;

namespace Perfect.AnalyzerService.Application.BannedWords
{
    public class BannedWordAnalyzerService : IBannedWordAnalyzerService
    {
        private IBannedWordAnalyzer _bannedWordAnalyzer;
        private readonly IMessageSender _messageSender;

        public BannedWordAnalyzerService(IBannedWordAnalyzer bannedWordAnalyzer, IMessageSender messageSender)
        {
            _bannedWordAnalyzer = bannedWordAnalyzer;
            _messageSender = messageSender;
        }

        public Task ExecuteAsync(BannedWordAnalyzerRequest request, CancellationToken cancellationToken)
        {
            var result = _bannedWordAnalyzer.Analyze(request.Content);

            return _messageSender.PublishEventAsync(new BannedWordsAnalzyedEvent
            {
                Count = result
            }, cancellationToken);
        }
    }
}
