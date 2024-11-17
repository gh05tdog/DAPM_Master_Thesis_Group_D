using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class PipelineServiceTests
{
    private PipelineService CreateService()
    {
        var connection = new SqlliteConnectionFactory();
        
        var repository = new PipelineRepository(connection, new PipelineTableInitializer(connection));

        return new PipelineService(repository, repository);
    }
    
    [Fact]
    public async Task AddUserPipeline_ShouldAddPipeline()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};
        var userPipeline = new UserPipelineDto{UserId = user.Id, PipelineId = pipeline.Id};

        await service.AddUserPipeline(userPipeline);

        var pipelines = await service.GetPipelinesForUser(user);
        Assert.Contains(pipelines, p => p.Id == pipeline.Id);
    }
    
    [Fact]
    public async Task RemoveUserPipeline_ShouldRemovePipeline()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};
        var userPipeline = new UserPipelineDto{UserId = user.Id, PipelineId = pipeline.Id};

        await service.AddUserPipeline(userPipeline);
        await service.RemoveUserPipeline(userPipeline);

        var pipelines = await service.GetPipelinesForUser(user);
        Assert.DoesNotContain(pipelines, p => p.Id == pipeline.Id);
    }
    
    [Fact]
    public async Task ReadAllPipelines_ShouldReturnPipelines()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};
        var userPipeline = new UserPipelineDto{UserId = user.Id, PipelineId = pipeline.Id};

        await service.AddUserPipeline(userPipeline);

        var pipelines = await service.GetAllUserPipelines();
        Assert.Contains(pipelines, p => p.PipelineId == pipeline.Id);
    }
    
    [Fact]
    public async Task UserHasAccessToPipeline_WhenUserHasAccessToPipeline_ReturnsTrue()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};
        var userPipeline = new UserPipelineDto{UserId = user.Id, PipelineId = pipeline.Id};

        await service.AddUserPipeline(userPipeline);

        var result = await service.UserHasAccessToPipeline(userPipeline);

        Assert.True(result);
    }
    
    [Fact]
    public async Task UserHasAccessToPipeline_WhenUserDoesNotHaveAccessToPipeline_ReturnsFalse()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};
        var userPipeline = new UserPipelineDto{UserId = user.Id, PipelineId = pipeline.Id};

        var result = await service.UserHasAccessToPipeline(userPipeline);

        Assert.False(result);
    }
}