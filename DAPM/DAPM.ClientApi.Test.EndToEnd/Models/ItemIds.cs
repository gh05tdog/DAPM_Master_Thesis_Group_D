namespace DAPM.Test.EndToEnd.Models;
public record ItemIds(
    Guid OrganizationId,
    Guid RepositoryId,
    Guid? ResourceId,
    Guid? PipelineId,
    Guid? ExecutionId
);
