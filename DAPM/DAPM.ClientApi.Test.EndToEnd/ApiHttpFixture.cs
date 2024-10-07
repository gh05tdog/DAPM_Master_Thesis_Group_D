using DAPM.Test.EndToEnd.TestUtilities;
using Microsoft.Extensions.Configuration;

namespace DAPM.Test.EndToEnd;

public class ApiHttpFixture : IDisposable
{
    public readonly DapmClientApiHttpClient Client;

    public ApiHttpFixture()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)  
            .Build();
        var baseAddress = new Uri(config["ClientApiSettings:BaseUrl"]);
        Client = new DapmClientApiHttpClient(baseAddress);
    }

    public void Dispose()
    {
    }
}