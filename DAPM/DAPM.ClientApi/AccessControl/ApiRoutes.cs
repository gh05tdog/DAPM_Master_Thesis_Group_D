namespace DAPM.ClientApi.AccessControl;

public class ApiRoutes
{
   private const string PipelineRoute = "pipeline";
   private const string ResourceRoute = "resource";
   private const string RepositoryRoute = "repository";
   private const string OrganizationRoute = "organization";
   public const string AddUserPipelineRoute = $"{PipelineRoute}/add-user-pipeline";
   public const string AddUserResourceRoute = $"{ResourceRoute}/add-user-resource";
   public const string AddUserRepositoryRoute = $"{RepositoryRoute}/add-user-repository";
   public const string AddUserOrganizationRoute = $"{OrganizationRoute}/add-user-organization";
   public const string GetUserPipelinesRoute = $"{PipelineRoute}/get-pipelines-for-user";
   public const string GetUserResourcesRoute = $"{ResourceRoute}/get-resources-for-user";
   public const string GetUserRepositoriesRoute = $"{RepositoryRoute}/get-repositories-for-user";
   public const string GetUserOrganizationsRoute = $"{OrganizationRoute}/get-organizations-for-user";
   public const string CheckAccessRoute = "check-access";
}