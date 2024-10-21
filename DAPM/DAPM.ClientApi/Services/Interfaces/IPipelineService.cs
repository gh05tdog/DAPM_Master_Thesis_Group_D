namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IPipelineService
    {
        public Guid GetPipelineById(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid userId);
        public Guid CreatePipelineExecution(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid userId);
        public Guid PostStartCommand(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId, Guid userId);
        public Guid GetExecutionStatus(Guid organizationId, Guid repositoryId, Guid pipelineId, Guid executionId, Guid userId);
    }
}
