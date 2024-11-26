using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;

namespace DAPM.AccessControlService.Test.Unit.Queries;

public class UserResourceQueriesTests
{
    private readonly IUserResourceQueries userResourceQueries;
    private readonly IResourceRepository resourceRepository;
    
    public UserResourceQueriesTests()
    {
        var connection = new DbConnectionFactory(TestHelper.ConnectionString);
        var repository = new ResourceRepository(connection, new ResourceTableInitializer(connection));
        userResourceQueries = repository;
        resourceRepository = repository;
    }
    
    [Fact]
    public async Task UserHasAccessToResource_WhenUserHasAccessToResource_ReturnsTrue()
    {
        var userResource = new UserResource(new UserId(Guid.NewGuid()), new ResourceId(Guid.NewGuid()));
        await resourceRepository.CreateUserResource(userResource);
        
        var result = await userResourceQueries.UserHasAccessToResource(userResource);
        
        Assert.True(result);
    }
}