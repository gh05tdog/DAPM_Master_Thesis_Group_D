using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public class ApiHttpClient(IApiHttpClientFactory httpClientFactory) : IApiHttpClient
{
    private const string PipelineRoute = "pipeline";
    private const string ResourceRoute = "resource";
    private const string RepositoryRoute = "repository";
    private const string OrganizationRoute = "organization";
    private const string AddUserPipelineRoute = $"{PipelineRoute}/add-user-pipeline";
    private const string AddUserResourceRoute = $"{ResourceRoute}/add-user-resource";
    private const string AddUserRepositoryRoute = $"{RepositoryRoute}/add-user-repository";
    private const string AddUserOrganizationRoute = $"{OrganizationRoute}/add-user-organization";
    private const string GetUserPipelinesRoute = $"{PipelineRoute}/get-pipelines-for-user";
    private const string GetUserResourcesRoute = $"{ResourceRoute}/get-resources-for-user";
    private const string GetUserRepositoriesRoute = $"{RepositoryRoute}/get-repositories-for-user";
    private const string GetUserOrganizationsRoute = $"{OrganizationRoute}/get-organizations-for-user";
    private const string CheckAccessRoute = "check-access";
    
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
    
    public async Task<bool> AddUserOrganizationAsync(UserOrganizationDto request)
    {
        return await SendRequestAsync<UserOrganizationDto, bool>(AddUserOrganizationRoute, request);
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
    
    public async Task<ICollection<OrganizationDto>> GetOrganizationsForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<OrganizationDto>>(GetUserOrganizationsRoute, request.Id);
    }

    public async Task<UserAccessResponseDto> GetUserAccessAsync(UserAccessRequestDto request)
    {
        return await SendRequestAsync<UserAccessRequestDto, UserAccessResponseDto>(CheckAccessRoute, request);
    }

    private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(string route, TRequest request)
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync(route, request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
    
    private async Task<TResponse> GetRequestAsync<TResponse>(string route, Guid userId)
    {
        var client = httpClientFactory.CreateClient();
        var response = await client.GetAsync($"{route}/{userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }
}