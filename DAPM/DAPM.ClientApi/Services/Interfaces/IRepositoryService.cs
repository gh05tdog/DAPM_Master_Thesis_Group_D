using DAPM.ClientApi.Models.DTOs;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IRepositoryService
    {
        public Guid GetRepositoryById(Guid organizationId, Guid repositoryId, Guid userIds);
        public Guid GetResourcesOfRepository(Guid organizationId, Guid repositoryId, Guid userId);
        public Guid GetPipelinesOfRepository(Guid organizationId, Guid repositoryId, Guid userIds);
        public Guid PostResourceToRepository(Guid organizationId, Guid repositoryId, string name, IFormFile resourceFile, string resourceType, Guid userId);
        public Guid PostOperatorToRepository(Guid organizationId, Guid repositoryId, string name, IFormFile sourceCodeFile, IFormFile dockerfileFile, string resourceType, Guid userId);
        public Guid PostPipelineToRepository(Guid organizationId, Guid repositoryId, PipelineApiDto pipeline, Guid userId);
    }
}
