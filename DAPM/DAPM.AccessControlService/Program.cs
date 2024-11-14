using System.Data;
using System.Data.SqlClient;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.OpenApi.Models;

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

// Add table initializers
builder.Services.AddSingleton<ITableInitializer<UserRepository>, RepositoryTableInitializer>();
builder.Services.AddSingleton<ITableInitializer<UserResource>, ResourceTableInitializer>();
builder.Services.AddSingleton<ITableInitializer<UserOrganization>, OrganizationTableInitializer>();
builder.Services.AddSingleton<ITableInitializer<UserPipeline>, PipelineTableInitializer>();

// Add repositories
builder.Services.AddSingleton<IPipelineRepository, PipelineRepository>();
builder.Services.AddSingleton<IRepositoryRepository, RepositoryRepository>();
builder.Services.AddSingleton<IResourceRepository, ResourceRepository>();
builder.Services.AddSingleton<IOrganizationRepository, OrganizationRepository>();

// Add query services
builder.Services.AddSingleton<IUserPipelineQueries, PipelineRepository>();
builder.Services.AddSingleton<IUserRepositoryQueries, RepositoryRepository>();
builder.Services.AddSingleton<IUserResourceQueries, ResourceRepository>();
builder.Services.AddSingleton<IUserOrganizationQueries, OrganizationRepository>();

// Add services
builder.Services.AddSingleton<IPipelineService, PipelineService>();
builder.Services.AddSingleton<IRepositoryService, RepositoryService>();
builder.Services.AddSingleton<IResourceService, ResourceService>();
builder.Services.AddSingleton<IOrganizationService, OrganizationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DAPM Access Control", Version = "v1" });
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

// Keycloak
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", builder =>
    {
        builder.RequireRealmRoles("Manager");
    });
    options.AddPolicy("PipelineManager", builder =>
    {
        builder.RequireRealmRoles("PipelineManager");
    });
    options.AddPolicy("RepositoryManager", builder =>
    {
        builder.RequireRealmRoles("RepositoryManager");
    });
    options.AddPolicy("ResourceManager", builder =>
    {
        builder.RequireRealmRoles("ResourceManager");
    });
    options.AddPolicy("OrganizationManager", builder =>
    {
        builder.RequireRealmRoles("OrganizationManager");
    });
}).AddKeycloakAuthorization(builder.Configuration);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
