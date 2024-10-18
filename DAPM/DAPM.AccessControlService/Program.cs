using System.Data;
using System.Data.SqlClient;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using RabbitMQLibrary.Extensions;
using RabbitMQLibrary.Implementation;
using RabbitMQLibrary.Messages.AccessControl.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.WebHost.UseKestrel(o => o.Limits.MaxRequestBodySize = null);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure database connection
builder.Services.AddTransient<IDbConnection>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});

// RabbitMQ
var config = builder.Configuration;
var host = config["RabbitMQ:Host"];
var port = int.Parse(config["RabbitMQ:Port"]);
var username = config["RabbitMQ:Username"];
var password = config["RabbitMQ:Password"];

builder.Services.AddQueueing(new QueueingConfigurationSettings
{
    RabbitMqConsumerConcurrency = 5,
    RabbitMqHostname = host,
    RabbitMqPort = port,
    RabbitMqPassword = password,
    RabbitMqUsername = username
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors("AllowAll");


app.UseAuthorization();

app.MapControllers();

app.Run();
