using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Perfect.Messages.Commands;
using Perfect.Messages.Events;
using Perfect.SagaService.Host.Configuration.Models;
using Perfect.SagaService.Host.StateMachine;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;

        // Application Settings
        services.Configure<AzureServiceBusSettings>(configuration.GetSection(AzureServiceBusSettings.Section));
        services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.Section));


        EndpointConvention.Map<AnalyzeFileCommand>(new Uri("queue:analyzer-service-analyze-command"));

        services.AddMassTransit(x =>
        {
            //x.AddSagaRepository<FileState>();
            x.AddSagaStateMachine<FileStateMachine, FileState>()
                .MongoDbRepository(r =>
                {
                    var mongoSettings = configuration.GetSection(MongoSettings.Section).Get<MongoSettings>();

                    r.Connection = mongoSettings?.ConnectionString
                        ?? throw new ArgumentNullException(nameof(MongoSettings.ConnectionString));
                    r.DatabaseName = mongoSettings?.DatabaseName
                        ?? throw new ArgumentNullException(nameof(MongoSettings.DatabaseName));
                    r.CollectionName = mongoSettings?.CollectionName
                        ?? throw new ArgumentNullException(nameof(MongoSettings.CollectionName));
                });


            //x.UsingAzureServiceBus((context, cfg) =>
            //{
            //    var settings = context.GetRequiredService<IOptions<AzureServiceBusSettings>>();
            //    cfg.Host(settings.Value.ConnectionString);
            //
            //    cfg.SubscriptionEndpoint<FileReceivedEvent>("saga-file-recieved-event", e =>
            //    {
            //        e.Consumer<FileReceivedConsumer>();
            //    });
            //});

            x.UsingRabbitMq((context, cfg) =>
            {
                var settings = context.GetRequiredService<IOptions<RabbitMqSettings>>();
                cfg.Host(settings.Value.ConnectionString);

                cfg.ReceiveEndpoint("file-saga-queue", x =>
                {
                    const int ConcurrencyLimit = 20;

                    x.UseInMemoryOutbox();
                    x.ConfigureSaga<FileState>(context, s =>
                    {
                        s.Message<FileReceivedEvent>(x => x.UsePartitioner(ConcurrencyLimit, y => y.Message.FileId));
                        s.Message<OddLettersAnalyzedEvent>(x => x.UsePartitioner(ConcurrencyLimit, y => y.Message.FileId));
                        s.Message<BannedWordsAnalzyedEvent>(x => x.UsePartitioner(ConcurrencyLimit, y => y.Message.FileId));
                    });
                });
            });
        });
    })
    .Build()
    .RunAsync();