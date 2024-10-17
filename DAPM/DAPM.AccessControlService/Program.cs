using System.Data;
using System.Data.SqlClient;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using RabbitMQLibrary.Implementation;
using RabbitMQLibrary.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure database connection
builder.Services.AddTransient<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});

// RabbitMQ
builder.Services.AddQueueing(new QueueingConfigurationSettings
{
    RabbitMqConsumerConcurrency = 5,
    RabbitMqHostname = "rabbitmq",
    RabbitMqPort = 5672,
    RabbitMqPassword = "guest",
    RabbitMqUsername = "guest"
});

// Add repositories
builder.Services.AddSingleton<IPipelineRepository, PipelineRepository>();
builder.Services.AddSingleton<IRepositoryRepository, RepositoryRepository>();
builder.Services.AddSingleton<IResourceRepository, ResourceRepository>();

// Add services
builder.Services.AddSingleton<IPipelineService, PipelineService>();
builder.Services.AddSingleton<IRepositoryService, RepositoryService>();
builder.Services.AddSingleton<IResourceService, ResourceService>();

// Add message queue consumers
builder.Services.AddQueueMessageConsumer<AddUserPipelineRequestMessageConsumer, AddUserPipelineRequestMessage>();
builder.Services.AddQueueMessageConsumer<AddUserRepositoryRequestMessageConsumer, AddUserRepositoryRequestMessage>();
builder.Services.AddQueueMessageConsumer<AddUserResourceRequestMessageConsumer, AddUserResourceRequestMessage>();
builder.Services.AddQueueMessageConsumer<GetPipelinesForUserRequestMessageConsumer, GetPipelinesForUserRequestMessage>();
builder.Services.AddQueueMessageConsumer<GetRepositoriesForUserRequestMessageConsumer, GetRepositoriesForUserRequestMessage>();
builder.Services.AddQueueMessageConsumer<GetResourcesForUserRequestMessageConsumer, GetResourcesForUserRequestMessage>();

var app = builder.Build();

app.Run();
