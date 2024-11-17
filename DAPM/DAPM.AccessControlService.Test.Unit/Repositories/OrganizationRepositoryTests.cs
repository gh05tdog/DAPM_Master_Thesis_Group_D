using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class OrganizationRepositoryTests
{
    private readonly IOrganizationRepository repository;

    public OrganizationRepositoryTests()
    {
        var connection = new DbConnectionFactory(TestHelper.ConnectionString);
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
    
    [Fact]
    public async Task RemoveUserOrganization_ShouldRemoveOrganization()
    {
        var userId = new UserId(Guid.NewGuid());
        var organizationId = new OrganizationId(Guid.NewGuid());

        await repository.CreateUserOrganization(new UserOrganization(userId, organizationId));
        await repository.DeleteUserOrganization(new UserOrganization(userId, organizationId));

        var organizations = await repository.ReadOrganizationsForUser(userId);
        Assert.DoesNotContain(organizations, p => p.Id == organizationId.Id);
    }
    
    [Fact]
    public async Task ReadAllOrganizations_ShouldReturnOrganizations()
    {
        var userId = new UserId(Guid.NewGuid());
        var organizationId = new OrganizationId(Guid.NewGuid());

        await repository.CreateUserOrganization(new UserOrganization(userId, organizationId));

        var organizations = await repository.ReadAllUserOrganizations();
        Assert.Contains(organizations, p => p.OrganizationId.Id == organizationId.Id);
    }
}