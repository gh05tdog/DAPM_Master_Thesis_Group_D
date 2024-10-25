using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Database;
using DAPM.AccessControlService.Test.Unit.Repositories.TableInitializers;
using Microsoft.Data.Sqlite;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class PipelineServiceTests
{
    private PipelineService CreateService()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var repository = new PipelineRepository(connection, new PipelineTableInitializer(connection));

        return new PipelineService(repository);
    }
    
    [Fact]
    public async Task AddUserPipeline_ShouldAddPipeline()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};

        await service.AddUserPipeline(user, pipeline);

        var pipelines = await service.GetPipelinesForUser(user);
        Assert.Contains(pipelines, p => p.Id == pipeline.Id);
    }
    
    [Fact]
    public async Task RemoveUserPipeline_ShouldRemovePipeline()
    {
        var service = CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};

        await service.AddUserPipeline(user, pipeline);
        await service.RemoveUserPipeline(user, pipeline);

        var pipelines = await service.GetPipelinesForUser(user);
        Assert.DoesNotContain(pipelines, p => p.Id == pipeline.Id);
    }
}