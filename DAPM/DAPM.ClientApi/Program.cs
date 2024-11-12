using System.Security.Claims;
using DAPM.ClientApi.AccessControl;
using DAPM.ClientApi.Services;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using RabbitMQ.Client;
using RabbitMQLibrary.Implementation;
using RabbitMQLibrary.Extensions;
using DAPM.ClientApi.Consumers;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults;
using Microsoft.OpenApi.Models;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults.FromPipelineOrchestrator;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.AddServiceDefaults();

builder.WebHost.UseKestrel(o => o.Limits.MaxRequestBodySize = null);

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartBoundaryLengthLimit = int.MaxValue;
    x.MultipartHeadersCountLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
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

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DAPM Client API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Access control services
builder.Services.AddSingleton(configuration.GetSection("AccessControl").Get<AccessControlConfig>());
builder.Services.AddSingleton<ITokenFetcher, TokenFetcher>();
builder.Services.AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>();
builder.Services.AddSingleton<IApiHttpClient, ApiHttpClient>();
builder.Services.AddSingleton<IAccessControlService, AccessControlService>();

builder.Services.AddQueueMessageConsumer<GetOrganizationsProcessResultConsumer, GetOrganizationsProcessResult>();
builder.Services.AddQueueMessageConsumer<PostItemResultConsumer, PostItemProcessResult>();
builder.Services.AddQueueMessageConsumer<GetRepositoriesProcessResultConsumer, GetRepositoriesProcessResult>();
builder.Services.AddQueueMessageConsumer<GetResourcesProcessResultConsumer, GetResourcesProcessResult>();
builder.Services.AddQueueMessageConsumer<GetPipelinesProcessResultConsumer, GetPipelinesProcessResult>();
builder.Services.AddQueueMessageConsumer<GetResourceFilesProcessResultConsumer, GetResourceFilesProcessResult>();
builder.Services.AddQueueMessageConsumer<CollabHandshakeProcessResultConsumer, CollabHandshakeProcessResult>();
builder.Services.AddQueueMessageConsumer<PostPipelineCommandProcessResultConsumer, PostPipelineCommandProcessResult>();
builder.Services.AddQueueMessageConsumer<GetPipelineExecutionStatusProcessResultConsumer, GetPipelineExecutionStatusRequestResult>();
// Add services to the container.


builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddScoped<IPipelineService, PipelineService>();
builder.Services.AddSingleton<ITicketService, TicketService>();
builder.Services.AddScoped<ISystemService, SystemService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Keycloak
builder.Services.AddKeycloakWebApiAuthentication(configuration, options =>
{
    options.BackchannelHttpHandler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AccessCheckMiddleware>();

app.MapControllers();

app.MapGet("/", (ClaimsPrincipal user) =>
{
    app.Logger.LogInformation(user.Identity.Name);
}).RequireAuthorization();

IdentityModelEventSource.ShowPII = true;

app.Run();
