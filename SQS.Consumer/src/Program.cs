using System.Reflection;
using Amazon.SQS;
using MediatR;
using SQS.Consumer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext,services) =>
    {
        services.Configure<QueueSettings>(hostContext.Configuration.GetSection(QueueSettings.Key));
        services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
        services.AddHostedService<Worker>();
        services.AddMediatR(mr => mr.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    })
    .Build();
host.Run();
