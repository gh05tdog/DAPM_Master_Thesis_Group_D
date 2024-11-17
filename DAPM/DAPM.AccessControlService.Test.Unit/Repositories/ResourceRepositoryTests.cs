using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class ResourceRepositoryTests
{
    private readonly ResourceRepository repository;
    
    public ResourceRepositoryTests()
    {
        var connection = new SqlliteConnectionFactory();
        repository = new ResourceRepository(connection, new ResourceTableInitializer(connection));
    }

    [Fact]
    public async Task AddUserResource_ShouldAddResource()
    {
        var userId = new UserId(Guid.NewGuid());
        var resourceId = new ResourceId(Guid.NewGuid());

        await repository.CreateUserResource(new UserResource(userId, resourceId));

        var resources = await repository.ReadResourcesForUser(userId);
        Assert.Contains(resources, p => p.Id == resourceId.Id);
    }
    
    [Fact]
    public async Task RemoveUserResource_ShouldRemoveResource()
    {
        var userId = new UserId(Guid.NewGuid());
        var resourceId = new ResourceId(Guid.NewGuid());

        await repository.CreateUserResource(new UserResource(userId, resourceId));
        await repository.DeleteUserResource(new UserResource(userId, resourceId));

        var resources = await repository.ReadResourcesForUser(userId);
        Assert.DoesNotContain(resources, p => p.Id == resourceId.Id);
    }
    
    [Fact]
    public async Task ReadAllResources_ShouldReturnResources()
    {
        var userId = new UserId(Guid.NewGuid());
        var resourceId = new ResourceId(Guid.NewGuid());

        await repository.CreateUserResource(new UserResource(userId, resourceId));

        var resources = await repository.ReadAllUserResources();
        Assert.Contains(resources, p => p.ResourceId.Id == resourceId.Id);
    }
}