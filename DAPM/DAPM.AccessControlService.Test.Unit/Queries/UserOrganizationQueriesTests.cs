using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Queries;

public class UserOrganizationQueriesTests
{
    private readonly IUserOrganizationQueries userOrganizationQueries;
    private readonly IOrganizationRepository organizationRepository;
    
    public UserOrganizationQueriesTests()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        var repository = new OrganizationRepository(connection, new OrganizationTableInitializer(connection));
        userOrganizationQueries = repository;
        organizationRepository = repository;
    }
    
    [Fact]
    public async Task UserHasAccessToOrganization_WhenUserHasAccessToOrganization_ReturnsTrue()
    {
        var userOrganization = new UserOrganization(new UserId(Guid.NewGuid()), new OrganizationId(Guid.NewGuid()));
        await organizationRepository.CreateUserOrganization(userOrganization);
        
        var result = await userOrganizationQueries.UserHasAccessToOrganization(userOrganization);
        
        Assert.True(result);
    }
}