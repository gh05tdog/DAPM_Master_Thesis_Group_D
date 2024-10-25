using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;
using DAPM.AccessControlService.Infrastructure.Database.TableInitializers;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class PipelineRepository : IPipelineRepository
{
    private readonly IDbConnection dbConnection;

    public PipelineRepository(IDbConnection dbConnection, ITableInitializer<UserPipeline> tableInitializer)
    {
        this.dbConnection = dbConnection;
        tableInitializer.InitializeTable().Wait();
    }
    
    public async Task CreateUserPipeline(UserPipeline userPipeline)
    {
        const string sql = @"
                INSERT INTO UserPipelines (UserId, PipelineId)
                VALUES (@UserId, @PipelineId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userPipeline.UserId.Id, PipelineId = userPipeline.PipelineId.Id });
    }

    public async Task<ICollection<PipelineId>> ReadPipelinesForUser(UserId userId)
    {
        const string sql = @"
                SELECT PipelineId
                FROM UserPipelines
                WHERE UserId = @UserId;
            ";
        
        var pipelineIds = await dbConnection.QueryAsync<String>(sql, new { UserId = userId.Id });
        return pipelineIds.Select(id => new PipelineId(Guid.Parse(id))).ToList();
    }
    
    public async Task DeleteUserPipeline(UserPipeline userPipeline)
    {
        const string sql = @"
                DELETE FROM UserPipelines
                WHERE UserId = @UserId AND PipelineId = @PipelineId;
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userPipeline.UserId.Id, PipelineId = userPipeline.PipelineId.Id });
    }
}