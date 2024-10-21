namespace TestUtility;

public interface IApiHttpClient
{
    Task<AddUserPipelineResponseMessage> AddUserPipelineAsync(AddUserPipelineRequestMessage request);
    Task<AddUserResourceResponseMessage> AddUserResourceAsync(AddUserResourceRequestMessage request);
    Task<AddUserRepositoryResponseMessage> AddUserRepositoryAsync(AddUserRepositoryRequestMessage request);
    Task<GetPipelinesForUserResponseMessage> GetPipelinesForUserAsync(GetPipelinesForUserRequestMessage request);
    Task<GetResourcesForUserResponseMessage> GetResourcesForUserAsync(GetResourcesForUserRequestMessage request);
    Task<GetRepositoriesForUserResponseMessage> GetRepositoriesForUserAsync(GetRepositoriesForUserRequestMessage request);
}
