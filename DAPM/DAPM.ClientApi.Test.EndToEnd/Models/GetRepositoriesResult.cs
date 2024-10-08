namespace DAPM.Test.EndToEnd.Models;

public record GetRepositoriesResult(
    Repository[] Repositories
);

public record Repository(
    Guid Id,
    string Name,
    Guid OrganizationId
);

