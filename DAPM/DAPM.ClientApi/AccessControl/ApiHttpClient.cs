using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public class ApiHttpClient(IApiHttpClientFactory httpClientFactory) : IApiHttpClient
{
    private const string AddUserPipelineRoute = "add-user-pipeline";
    private const string AddUserResourceRoute = "add-user-resource";
    private const string AddUserRepositoryRoute = "add-user-repository";
    private const string GetUserPipelinesRoute = "get-pipelines-for-user";
    private const string GetUserResourcesRoute = "get-resources-for-user";
    private const string GetUserRepositoriesRoute = "get-repositories-for-user";
    
    public async Task<bool> AddUserPipelineAsync(UserPipelineDto request)
    {
        return await SendRequestAsync<UserPipelineDto, bool>(AddUserPipelineRoute, request);
    }

    public async Task<bool> AddUserResourceAsync(UserResourceDto request)
    {
        return await SendRequestAsync<UserResourceDto, bool>(AddUserResourceRoute, request);
    }

    public async Task<bool> AddUserRepositoryAsync(UserRepositoryDto request)
    {
        return await SendRequestAsync<UserRepositoryDto, bool>(AddUserRepositoryRoute, request);
    }

    public async Task<ICollection<PipelineDto>> GetPipelinesForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<PipelineDto>>(GetUserPipelinesRoute, request.Id);
    }

    public async Task<ICollection<ResourceDto>> GetResourcesForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<ResourceDto>>(GetUserResourcesRoute, request.Id);
    }

    public async Task<ICollection<RepositoryDto>> GetRepositoriesForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<RepositoryDto>>(GetUserRepositoriesRoute, request.Id);
    }
    
    private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(string route, TRequest request)
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync(route, request);
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
    
    private async Task<TResponse> GetRequestAsync<TResponse>(string route, Guid userId)
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync($"{route}/{userId}");
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
}