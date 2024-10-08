using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

public class ResourceTest
{
    private readonly HttpClient _httpClient;
    private const string organication= "d87bc490-828f-46c8-aa44-ded7729eaa82";
    private const string repository= "b509fabc-88bd-425c-aac4-c14443828941";
    private const string RequestUri = $"http://localhost:5000/organizations/{organication}/repositories/{repository}/resources"; // Replace with your API endpoint
    private const string FilePath = "test.txt"; // Path to the file you want to send

    public ResourceTest()
    {
        _httpClient = new HttpClient();
    }

    [Fact]
    public async Task PostApi_WithMultipartData_Returns200()
    {
        // Arrange
        
        await File.WriteAllTextAsync(FilePath, "This is a test file content."); 
        var name = "test";
        var resourceType = "txt";

        // Act
        var response = await SendMultipartDataAsync(FilePath, name, resourceType);

        // Assert
        Assert.Equal(200, (int)response.StatusCode);
    }

    private async Task<HttpResponseMessage> SendMultipartDataAsync(string filePath, string name, string resourceType)
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
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

        return await _httpClient.PostAsync(RequestUri, content);
    }
}
