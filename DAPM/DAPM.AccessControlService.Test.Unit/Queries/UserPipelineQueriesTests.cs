using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Queries;

public class UserPipelineQueriesTests
{
    private readonly IUserPipelineQueries userPipelineQueries;
    private readonly IPipelineRepository pipelineRepository;
    
    public UserPipelineQueriesTests()
    {
        var connection = new SqlliteConnectionFactory();
        var repository = new PipelineRepository(connection, new PipelineTableInitializer(connection));
        userPipelineQueries = repository;
        pipelineRepository = repository;
    }
    
    [Fact]
    public async Task UserHasAccessToPipeline_WhenUserHasAccessToPipeline_ReturnsTrue()
    {
        var userPipeline = new UserPipeline(new UserId(Guid.NewGuid()), new PipelineId(Guid.NewGuid()));
        await pipelineRepository.CreateUserPipeline(userPipeline);
        
        var result = await userPipelineQueries.UserHasAccessToPipeline(userPipeline);
        
        Assert.True(result);
    }
}