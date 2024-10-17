using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data.SqlClient;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class PipelineRepository : IPipelineRepository
{
    private readonly string connectionString;
    
    public PipelineRepository(string connectionString)
    {
        this.connectionString = connectionString;
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
        
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql);
    }

    public async Task AddUserPipeline(UserId userId, PipelineId pipelineId)
    {
        const string sql = @"
                INSERT INTO UserPipelines (UserId, PipelineId)
                VALUES (@UserId, @PipelineId);
            ";
        
        await using var connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sql, new { UserId = userId.Id, PipelineId = pipelineId.Id });
    }

    public async Task<ICollection<PipelineId>> GetPipelinesForUser(UserId userId)
    {
        const string sql = @"
                SELECT PipelineId
                FROM UserPipelines
                WHERE UserId = @UserId;
            ";
        
        await using var connection = new SqlConnection(connectionString);
        var pipelineIds = await connection.QueryAsync<Guid>(sql, new { UserId = userId.Id });
        return pipelineIds.Select(id => new PipelineId(id)).ToList();
    }
}