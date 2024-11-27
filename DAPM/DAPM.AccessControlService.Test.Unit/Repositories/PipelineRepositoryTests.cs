using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure;
using DAPM.AccessControlService.Infrastructure.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;

namespace DAPM.AccessControlService.Test.Unit.Repositories;

public class PipelineRepositoryTests
{
    private readonly IPipelineRepository repository;

    public PipelineRepositoryTests()
    {
        var connection = new DbConnectionFactory(TestHelper.ConnectionString);
        repository = new PipelineRepository(connection, new PipelineTableInitializer(connection));
    }
    
    [Fact]
    public async Task AddUserPipeline_ShouldAddPipeline()
    {
        var userId = new UserId(Guid.NewGuid());
        var pipelineId = new PipelineId(Guid.NewGuid());

        await repository.CreateUserPipeline(new UserPipeline(userId, pipelineId));

        var pipelines = await repository.ReadPipelinesForUser(userId);
        Assert.Contains(pipelines, p => p.Id == pipelineId.Id);
    }
    
    [Fact]
    public async Task RemoveUserPipeline_ShouldRemovePipeline()
    {
        var userId = new UserId(Guid.NewGuid());
        var pipelineId = new PipelineId(Guid.NewGuid());

        await repository.CreateUserPipeline(new UserPipeline(userId, pipelineId));
        await repository.DeleteUserPipeline(new UserPipeline(userId, pipelineId));

        var pipelines = await repository.ReadPipelinesForUser(userId);
        Assert.DoesNotContain(pipelines, p => p.Id == pipelineId.Id);
    }
    
    [Fact]
    public async Task ReadAllPipelines_ShouldReturnPipelines()
    {
        var userId = new UserId(Guid.NewGuid());
        var pipelineId = new PipelineId(Guid.NewGuid());

        await repository.CreateUserPipeline(new UserPipeline(userId, pipelineId));

        var pipelines = await repository.ReadAllUserPipelines();
        Assert.Contains(pipelines, p => p.PipelineId.Id == pipelineId.Id);
    }
}