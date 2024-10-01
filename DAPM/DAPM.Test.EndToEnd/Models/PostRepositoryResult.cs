namespace DAPM.Test.EndToEnd.Models;

public record PostRepositoryResult(
    ItemIds ItemIds,
    string ItemType,
    bool Succeeded,
    string Message
);

public record ItemIds(
    Guid OrganizationId,
    Guid RepositoryId,
    Guid? ResourceId,
    Guid? PipelineId,
    Guid? ExecutionId
);

