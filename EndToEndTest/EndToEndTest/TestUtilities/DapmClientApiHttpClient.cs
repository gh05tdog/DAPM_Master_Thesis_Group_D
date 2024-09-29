using System.Net.Http.Json;
using EndToEndTest.Models;

namespace EndToEndTest.TestUtilities;

public class DapmClientApiHttpClient(Uri? baseAddress = null)
{
    private static readonly Uri LocalHostBaseAddress = new("http://localhost:5000");
    private readonly Uri baseAddress = baseAddress ?? LocalHostBaseAddress;

    public async Task<ICollection<Organization>> GetOrganizationsAsync()
    {
        var uri = "organizations";
        var ticketResponse = await GetAsync(uri);
        var ticketResult = await GetTicketResultAsync<GetOrganizationsTicketResult>(ticketResponse);
        return ticketResult.Result.Organizations;
    }
    
    public async Task<Organization> GetOrganizationByIdAsync(Guid id)
    {
        var uri = $"organizations/{id}";
        var ticketResponse = await GetAsync(uri);
        var ticketResult = await GetTicketResultAsync<GetOrganizationTicketResult>(ticketResponse);
        return ticketResult.Result.Organizations.First();
    }
    
    public async Task<ICollection<Repository>> GetRepositoriesAsync(Guid organizationId)
    {
        var uri = $"organizations/{organizationId}/repositories";
        var ticketResponse = await GetAsync(uri);
        var ticketResult = await GetTicketResultAsync<GetRepositoriesResult>(ticketResponse);
        return ticketResult.Result.Repositories;
    }

    public async Task<PostRepositoryResult> PostRepositoryAsync(Guid organizationId, string repositoryName)
    {
        var uri = $"organizations/{organizationId}/repositories";
        var ticketResponse = await PostAsync(uri, new PostRepositoryRequest(repositoryName));
        var ticketResult = await GetTicketResultAsync<PostRepositoryResult>(ticketResponse);
        return ticketResult.Result;
    }

    private async Task<Ticket<T>> GetTicketResultAsync<T>(TicketResponse ticketResponse)
    {
        var uri = $"status/{ticketResponse.TicketId}";
        var maxTries = 100;
        
        using var client = new HttpClient();
        client.BaseAddress = baseAddress;
        
        for (var i = 0; i < maxTries; i++)
        {
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var ticket = await response.Content.ReadFromJsonAsync<Ticket<T>>();
            if (ticket?.Status == TicketStatus.Completed)
                return ticket;
            await Task.Delay(50);
        }

        throw new Exception($"Reached max tries '{maxTries}'");
    }

    private async Task<TicketResponse> GetAsync(string uri)
    {
        using var client = new HttpClient();
        client.BaseAddress = baseAddress;

        var response = await client.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<TicketResponse>() ?? 
               throw new ArgumentNullException(nameof(TicketResponse));
    }

    private async Task<TicketResponse> PostAsync(string uri, object obj)
    {
        using var client = new HttpClient();
        client.BaseAddress = baseAddress;
        
        var response = await client.PostAsJsonAsync(uri, obj);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<TicketResponse>() ?? 
            throw new ArgumentNullException(nameof(TicketResponse));
    }
}