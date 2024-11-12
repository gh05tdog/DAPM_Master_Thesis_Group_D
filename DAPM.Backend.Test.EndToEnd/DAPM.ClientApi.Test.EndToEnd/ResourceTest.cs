using System.Net.Http.Headers;
using DAPM.Test.EndToEnd;
using DAPM.Test.EndToEnd.TestUtilities;
using TestUtilities;

[Collection("ApiHttpCollection")]
public class ResourceTest(ApiHttpFixture apiHttpFixture)
{
    private const string FilePath = "test.txt";
    private readonly DapmClientApiHttpClient client = apiHttpFixture.Client;
    private readonly IHttpClientFactory httpClientFactory = apiHttpFixture.HttpClientFactory;
    private readonly AccessControlAdder accessControlAdder = apiHttpFixture.AccessControlAdder;

    [Fact]
    public async Task PostApi_WithMultipartData_Returns200()
    {
        // Arrange
        await accessControlAdder.AddUserOrganizationAsync(TestHelper.UserId, TestHelper.OrganizationId);
        var organizationId = (await client.GetOrganizationsAsync()).First().Id;
        var repositoryResult = await client.PostRepositoryAsync(organizationId, Guid.NewGuid().ToString());
        var repositoryId = repositoryResult.ItemIds.RepositoryId;
        
        await File.WriteAllTextAsync(FilePath, "This is a test file content."); 
        var name = "test";
        var resourceType = "txt";

        // Act
        var response = await SendMultipartDataAsync(FilePath, name, resourceType, organizationId, repositoryId);

        // Assert
        Assert.Equal(200, (int)response.StatusCode);
    }

    private async Task<HttpResponseMessage> SendMultipartDataAsync(string filePath, string name, string resourceType, Guid organization, Guid repository)
    {
        using var content = new MultipartFormDataContent();

        // Add string fields
        content.Add(new StringContent(name), "Name");
        content.Add(new StringContent(resourceType), "ResourceType");

        // Add file content
        await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

        content.Add(fileContent, "ResourceFile", Path.GetFileName(filePath)); // Field name and filename

        // Set headers
        var client = httpClientFactory.CreateClientApiClient(Users.Manager);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        
        var requestUri = $"organizations/{organization}/repositories/{repository}/resources";
        return await client.PostAsync(requestUri, content);
    }
}
