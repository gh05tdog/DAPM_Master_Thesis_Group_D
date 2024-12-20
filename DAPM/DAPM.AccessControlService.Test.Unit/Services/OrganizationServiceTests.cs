using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class OrganizationServiceTests
{
    private OrganizationService CreateService()
    {
        var connection = new DbConnectionFactory(TestHelper.ConnectionString);
        var repository = new OrganizationRepository(connection, new OrganizationTableInitializer(connection));

        return new OrganizationService(repository, repository);
    }
    
    [Fact]
    public async Task AddUserOrganization_ShouldAddOrganization()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var organization = new OrganizationDto{Id = Guid.NewGuid()};
        var userOrganization = new UserOrganizationDto{UserId = user.Id, OrganizationId = organization.Id};

        await service.AddUserOrganization(userOrganization);

        var organizations = await service.GetOrganizationsForUser(user);
        Assert.Contains(organizations, p => p.Id == organization.Id);
    }
    
    [Fact]
    public async Task RemoveUserOrganization_ShouldRemoveOrganization()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var organization = new OrganizationDto{Id = Guid.NewGuid()};
        var userOrganization = new UserOrganizationDto{UserId = user.Id, OrganizationId = organization.Id};
        
        await service.AddUserOrganization(userOrganization);
        await service.RemoveUserOrganization(userOrganization);

        var organizations = await service.GetOrganizationsForUser(user);
        Assert.DoesNotContain(organizations, p => p.Id == organization.Id);
    }
    
    [Fact]
    public async Task ReadAllOrganizations_ShouldReturnOrganizations()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var organization = new OrganizationDto{Id = Guid.NewGuid()};
        var userOrganization = new UserOrganizationDto{UserId = user.Id, OrganizationId = organization.Id};

        await service.AddUserOrganization(userOrganization);

        var organizations = await service.GetAllUserOrganizations();
        Assert.Contains(organizations, p => p.OrganizationId == organization.Id);
    }
    
    [Fact]
    public async Task UserHasAccessToOrganization_WhenUserHasAccessToOrganization_ReturnsTrue()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var organization = new OrganizationDto{Id = Guid.NewGuid()};
        var userOrganization = new UserOrganizationDto{UserId = user.Id, OrganizationId = organization.Id};

        await service.AddUserOrganization(userOrganization);

        var result = await service.UserHasAccessToOrganization(userOrganization);
        
        Assert.True(result);
    }
    
    [Fact]
    public async Task UserHasAccessToOrganization_WhenUserDoesNotHaveAccessToOrganization_ReturnsFalse()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var organization = new OrganizationDto{Id = Guid.NewGuid()};
        var userOrganization = new UserOrganizationDto{UserId = user.Id, OrganizationId = organization.Id};

        var result = await service.UserHasAccessToOrganization(userOrganization);
        
        Assert.False(result);
    }
}