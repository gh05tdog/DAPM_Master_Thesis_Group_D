using DAPM.AccessControlService.Test.EndToEnd.Utilities;
using Microsoft.Extensions.Configuration;

namespace DAPM.AccessControlService.Test.EndToEnd;

public class TestFixture
{
     public readonly IHttpClientFactory HttpClientFactory;
     public const string AddUserPipelineRoute = "add-user-pipeline";
     public const string AddUserResourceRoute = "add-user-resource";
     public const string AddUserRepositoryRoute = "add-user-repository";
     public const string GetUserPipelinesRoute = "get-pipelines-for-user";
     public const string GetUserResourcesRoute = "get-resources-for-user";
     public const string GetUserRepositoriesRoute = "get-repositories-for-user";
     
     public TestFixture()
     {
         var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
         var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: false)
             .AddJsonFile($"appsettings.{environment}.json", optional: true)  
             .Build();
         
         HttpClientFactory = new HttpClientFactory(new Uri(config["ApiHttpClientFactorySettings:BaseUrl"]));
     }
}