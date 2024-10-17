using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class PipelineRepository : IPipelineRepository
{
    private readonly IDbConnection dbConnection;

    public PipelineRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeScheme()
    {
        const string sql = @"
                CREATE TABLE IF NOT EXISTS UserPipelines (
                    UserId UNIQUEIDENTIFIER NOT NULL,
                    PipelineId UNIQUEIDENTIFIER NOT NULL,
                    PRIMARY KEY (UserId, PipelineId)
                );
            ";
        
        await dbConnection.ExecuteAsync(sql);
    }

    public async Task AddUserPipeline(UserId userId, PipelineId pipelineId)
    {
        const string sql = @"
                INSERT INTO UserPipelines (UserId, PipelineId)
                VALUES (@UserId, @PipelineId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userId.Id, PipelineId = pipelineId.Id });
    }

    public async Task<ICollection<PipelineId>> GetPipelinesForUser(UserId userId)
    {
        const string sql = @"
                SELECT PipelineId
                FROM UserPipelines
                WHERE UserId = @UserId;
            ";
        
        var pipelineIds = await dbConnection.QueryAsync<Guid>(sql, new { UserId = userId.Id });
        return pipelineIds.Select(id => new PipelineId(id)).ToList();
    }
}