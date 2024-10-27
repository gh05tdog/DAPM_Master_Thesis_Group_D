using System.Data;
using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Queries;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Infrastructure.TableInitializers;
using Dapper;

namespace DAPM.AccessControlService.Infrastructure.Repositories;

public class PipelineRepository : IPipelineRepository, IUserPipelineQueries
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
    
    public async Task<ICollection<UserPipeline>> ReadAllUserPipelines()
    {
        const string sql = @"
                SELECT UserId, PipelineId
                FROM UserPipelines;
            ";
        
        var userPipelines = await dbConnection.QueryAsync<(string, string)>(sql);
        return userPipelines
            .Select(x => new UserPipeline(new UserId(Guid.Parse(x.Item1)), new PipelineId(Guid.Parse(x.Item2))))
            .ToList();

    }

    public async Task<bool> UserHasAccessToPipeline(UserPipeline userPipeline)
    {
        const string sql = @"
                SELECT CASE WHEN EXISTS (
                    SELECT 1
                    FROM UserPipelines
                    WHERE UserId = @UserId AND PipelineId = @PipelineId
                ) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END;
            ";
        return await dbConnection.ExecuteScalarAsync<bool>(sql, new { UserId = userPipeline.UserId.Id, PipelineId = userPipeline.PipelineId.Id });
    }
}