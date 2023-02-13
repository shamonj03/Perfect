using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Perfect.Messages.Commands;
using Perfect.Messages.Events;
using Perfect.SagaService.Application.Settings;
using Perfect.SagaService.Host.StateMachine;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        var configuration = ctx.Configuration;

        // Application Settings
        services.Configure<AzureServiceBusSettings>(configuration.GetSection(AzureServiceBusSettings.Section));
        services.PostConfigure<AzureServiceBusSettings>(x =>
        {
            x.ConnectionString = configuration.GetConnectionString(AzureServiceBusSettings.Section);
        });

        services.Configure<RabbitMqSettings>(configuration.GetSection(RabbitMqSettings.Section));
        services.PostConfigure<RabbitMqSettings>(x =>
        {
            x.ConnectionString = configuration.GetConnectionString(RabbitMqSettings.Section);
        });

        services.Configure<MongoSettings>(configuration.GetSection(MongoSettings.Section));
        services.PostConfigure<MongoSettings>(x =>
        {
            x.ConnectionString = configuration.GetConnectionString(MongoSettings.Section);
        });

        EndpointConvention.Map<AnalyzeFileCommand>(new Uri("queue:analyzer-service-analyze-command"));

        services.AddMassTransit(x =>
        {
            //x.AddSagaRepository<FileState>();
            x.AddSagaStateMachine<FileStateMachine, FileState>()
                .MongoDbRepository(r =>
                {
                    var connectionString = configuration.GetConnectionString(MongoSettings.Section);
                    var mongoSettings = configuration.GetSection(MongoSettings.Section).Get<MongoSettings>();

                    r.Connection = connectionString
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

                cfg.ReceiveEndpoint(settings.Value.SagaQueue, x =>
                {
                    const int ConcurrencyLimit = 20; 
                    x.PrefetchCount = ConcurrencyLimit; 
                    x.UseMessageRetry(r => r.Interval(5, 1000));

                    x.UseInMemoryOutbox();
                    x.ConfigureSaga<FileState>(context, s =>
                    {
                        s.Message<FileReceivedEvent>(x => x.UsePartitioner(ConcurrencyLimit, y => y.Message.FileName));
                        s.Message<OddLettersAnalyzedEvent>(x => x.UsePartitioner(ConcurrencyLimit, y => y.Message.FileId));
                        s.Message<BannedWordsAnalzyedEvent>(x => x.UsePartitioner(ConcurrencyLimit, y => y.Message.FileId));
                    });
                });
            });
        });
    })
    .Build()
    .RunAsync();