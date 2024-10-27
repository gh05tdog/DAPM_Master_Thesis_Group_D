using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DAPM.AccessControlService.Test.EndToEnd.Utilities;
using DAPM.Test.EndToEnd;
using DAPM.Test.EndToEnd.TestUtilities;
using Xunit;

[Collection("ApiHttpCollection")]
public class ResourceTest(ApiHttpFixture apiHttpFixture)
{
    private readonly HttpClient _httpClient = new();
    private const string FilePath = "test.txt"; // Path to the file you want to send
    private readonly DapmClientApiHttpClient client = apiHttpFixture.AuthenticatedClient;
    private readonly string baseUrl = apiHttpFixture.BaseUrl;
    private readonly TokenFetcher TokenFetcher = apiHttpFixture.TokenFetcher;
    private readonly AccessControlAdder AccessControlAdder = apiHttpFixture.AccessControlAdder;

    [Fact]
    public async Task PostApi_WithMultipartData_Returns200()
    {
        // Arrange
        await AccessControlAdder.AddUserOrganizationAsync(TestHelper.UserId, TestHelper.OrganizationId);
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
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

        content.Add(fileContent, "ResourceFile", Path.GetFileName(filePath)); // Field name and filename

        // Set headers
        var token = await TokenFetcher.GetTokenAsync();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);     
        
        var requestUri = $"{baseUrl}/organizations/{organization}/repositories/{repository}/resources";
        return await _httpClient.PostAsync(requestUri, content);
    }
}
