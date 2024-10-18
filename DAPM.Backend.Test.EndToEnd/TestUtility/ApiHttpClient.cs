using DAPM.ClientApi.AccessControl;

namespace TestUtility;

public class ApiHttpClient(IApiHttpClientFactory httpClientFactory) : IApiHttpClient
{
    private const string AddUserPipelineRoute = "add-user-pipeline";
    private const string AddUserResourceRoute = "add-user-resource";
    private const string AddUserRepositoryRoute = "add-user-repository";
    private const string GetUserPipelinesRoute = "get-pipelines-for-user";
    private const string GetUserResourcesRoute = "get-resources-for-user";
    private const string GetUserRepositoriesRoute = "get-repositories-for-user";
    
    public async Task<AddUserPipelineResponseMessage> AddUserPipelineAsync(AddUserPipelineRequestMessage request)
    {
        return await SendRequestAsync<AddUserPipelineRequestMessage, AddUserPipelineResponseMessage>(AddUserPipelineRoute, request);
    }

    public async Task<AddUserResourceResponseMessage> AddUserResourceAsync(AddUserResourceRequestMessage request)
    {
        return await SendRequestAsync<AddUserResourceRequestMessage, AddUserResourceResponseMessage>(AddUserResourceRoute, request);
    }

    public async Task<AddUserRepositoryResponseMessage> AddUserRepositoryAsync(AddUserRepositoryRequestMessage request)
    {
        return await SendRequestAsync<AddUserRepositoryRequestMessage, AddUserRepositoryResponseMessage>(AddUserRepositoryRoute, request);
    }

    public async Task<GetPipelinesForUserResponseMessage> GetPipelinesForUserAsync(GetPipelinesForUserRequestMessage request)
    {
        return await GetRequestAsync<GetPipelinesForUserResponseMessage>(GetUserPipelinesRoute, request.User.Id);
    }

    public async Task<GetResourcesForUserResponseMessage> GetResourcesForUserAsync(GetResourcesForUserRequestMessage request)
    {
        return await GetRequestAsync<GetResourcesForUserResponseMessage>(GetUserResourcesRoute, request.User.Id);
    }

    public async Task<GetRepositoriesForUserResponseMessage> GetRepositoriesForUserAsync(GetRepositoriesForUserRequestMessage request)
    {
        return await GetRequestAsync<GetRepositoriesForUserResponseMessage>(GetUserRepositoriesRoute, request.User.Id);
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