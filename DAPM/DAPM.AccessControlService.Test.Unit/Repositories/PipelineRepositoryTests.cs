using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Infrastructure.Database;
using Microsoft.Data.Sqlite;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class PipelineRepositoryTests
{
    private IDbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
    }
    
    
    [Fact]
    public async Task AddUserPipeline_ShouldAddPipeline()
    {
        using var connection = CreateInMemoryDatabase();
        var repository = new PipelineRepository(connection);
        await repository.InitializeScheme();

        var userId = new UserId(Guid.NewGuid());
        var pipelineId = new PipelineId(Guid.NewGuid());

        await repository.AddUserPipeline(userId, pipelineId);

        var pipelines = await repository.GetPipelinesForUser(userId);
        Assert.Contains(pipelines, p => p.Id == pipelineId.Id);
    }
}