using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using Dapper;
using System.Data;

namespace DAPM.AccessControlService.Infrastructure.Database;

public class PipelineRepository : IPipelineRepository
{
    private readonly IDbConnection dbConnection;
    private bool initialized;

    public PipelineRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task InitializeScheme()
    {
        const string sql = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserPipelines' AND xtype='U')
            BEGIN
                CREATE TABLE UserPipelines (
                    UserId NVARCHAR(50) NOT NULL,
                    PipelineId NVARCHAR(50) NOT NULL,
                    PRIMARY KEY (UserId, PipelineId)
                );
            END
        ";
        
        await dbConnection.ExecuteAsync(sql);
        initialized = true;
    }
    
    public async Task InitializeScheme(string sql)
        {
            await dbConnection.ExecuteAsync(sql);
            initialized = true;
        }

    public async Task AddUserPipeline(UserId userId, PipelineId pipelineId)
    {
        if (!initialized)
            await InitializeScheme();
       
        const string sql = @"
                INSERT INTO UserPipelines (UserId, PipelineId)
                VALUES (@UserId, @PipelineId);
            ";
        
        await dbConnection.ExecuteAsync(sql, new { UserId = userId.Id, PipelineId = pipelineId.Id });
    }

    public async Task<ICollection<PipelineId>> GetPipelinesForUser(UserId userId)
    {
        if (!initialized)
            await InitializeScheme();
        
        const string sql = @"
                SELECT PipelineId
                FROM UserPipelines
                WHERE UserId = @UserId;
            ";
        
        var pipelineIds = await dbConnection.QueryAsync<String>(sql, new { UserId = userId.Id });
        return pipelineIds.Select(id => new PipelineId(Guid.Parse(id))).ToList();
    }
}