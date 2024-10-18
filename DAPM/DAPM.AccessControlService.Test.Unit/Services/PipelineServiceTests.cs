using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class PipelineServiceTests
{
    private async Task<PipelineService> CreateService()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var repository = new PipelineRepository(connection);
        await repository.InitializeScheme(TestHelper.PipelineInitSql);

        return new PipelineService(repository);
    }
    
    [Fact]
    public async Task AddUserPipeline_ShouldAddPipeline()
    {
        var service = await CreateService();

        var user = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};

        await service.AddUserPipeline(user, pipeline);

        var pipelines = await service.GetPipelinesForUser(user);
        Assert.Contains(pipelines, p => p.Id == pipeline.Id);
    }
}