using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public class ApiHttpClient(IApiHttpClientFactory httpClientFactory) : IApiHttpClient
{
    public async Task<bool> AddUserPipelineAsync(UserPipelineDto request)
    {
        return await SendRequestAsync<UserPipelineDto, bool>(ApiRoutes.AddUserPipelineRoute, request);
    }

    public async Task<bool> AddUserResourceAsync(UserResourceDto request)
    {
        return await SendRequestAsync<UserResourceDto, bool>(ApiRoutes.AddUserResourceRoute, request);
    }

    public async Task<bool> AddUserRepositoryAsync(UserRepositoryDto request)
    {
        return await SendRequestAsync<UserRepositoryDto, bool>(ApiRoutes.AddUserRepositoryRoute, request);
    }
    
    public async Task<bool> AddUserOrganizationAsync(UserOrganizationDto request)
    {
        return await SendRequestAsync<UserOrganizationDto, bool>(ApiRoutes.AddUserOrganizationRoute, request);
    }

    public async Task<ICollection<PipelineDto>> GetPipelinesForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<PipelineDto>>(ApiRoutes.GetUserPipelinesRoute, request.Id);
    }

    public async Task<ICollection<ResourceDto>> GetResourcesForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<ResourceDto>>(ApiRoutes.GetUserResourcesRoute, request.Id);
    }

    public async Task<ICollection<RepositoryDto>> GetRepositoriesForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<RepositoryDto>>(ApiRoutes.GetUserRepositoriesRoute, request.Id);
    }
    
    public async Task<ICollection<OrganizationDto>> GetOrganizationsForUserAsync(UserDto request)
    {
        return await GetRequestAsync<ICollection<OrganizationDto>>(ApiRoutes.GetUserOrganizationsRoute, request.Id);
    }

    public async Task<UserAccessResponseDto> GetUserAccessAsync(UserAccessRequestDto request)
    {
        return await SendRequestAsync<UserAccessRequestDto, UserAccessResponseDto>(CheckAccessRoute, request);
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