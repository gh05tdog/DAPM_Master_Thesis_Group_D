namespace DAPM.AccessControlService.Test.Unit;

public static class TestHelper
{
    public const string PipelineInitSql = @"
                CREATE TABLE IF NOT EXISTS UserPipelines (
                    UserId TEXT NOT NULL,
                    PipelineId TEXT NOT NULL,
                    PRIMARY KEY (UserId, PipelineId)
                );
            ";
    
    public const string RepositoryInitSql = @"
                CREATE TABLE IF NOT EXISTS UserRepositories (
                    UserId TEXT NOT NULL,
                    RepositoryId TEXT NOT NULL,
                    PRIMARY KEY (UserId, RepositoryId)
                );
            ";
    
    public const string ResourceInitSql = @"
                CREATE TABLE IF NOT EXISTS UserResources (
                    UserId TEXT NOT NULL,
                    ResourceId TEXT NOT NULL,
                    PRIMARY KEY (UserId, ResourceId)
                );
            ";
    
}