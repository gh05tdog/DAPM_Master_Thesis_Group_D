using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services;
using DAPM.AccessControlService.Infrastructure.Database;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Services;

public class PipelineServiceTests
{
    private async Task<PipelineService> CreateService()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var repository = new PipelineRepository(connection);
        await repository.InitializeScheme();

        return new PipelineService(repository);
    }
    
    [Fact]
    public async Task AddUserPipeline_ShouldAddPipeline()
    {
        var service = await CreateService();

        var user = new UserDto(Guid.NewGuid());
        var pipeline = new PipelineDto(Guid.NewGuid());

        await service.AddUserPipeline(user, pipeline);

        var pipelines = await service.GetPipelinesForUser(user);
        Assert.Contains(pipelines, p => p.Id == pipeline.Id);
    }
}