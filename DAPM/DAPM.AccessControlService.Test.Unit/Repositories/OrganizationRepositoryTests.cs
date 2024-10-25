using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class OrganizationRepositoryTests
{
    private readonly IOrganizationRepository repository;

    public OrganizationRepositoryTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        repository = new OrganizationRepository(connection, new OrganizationTableInitializer(connection));
    }
    
    [Fact]
    public async Task AddUserOrganization_ShouldAddOrganization()
    {
        var userId = new UserId(Guid.NewGuid());
        var organizationId = new OrganizationId(Guid.NewGuid());

        await repository.CreateUserOrganization(new UserOrganization(userId, organizationId));

        var organizations = await repository.ReadOrganizationsForUser(userId);
        Assert.Contains(organizations, p => p.Id == organizationId.Id);
    }
}