using Microsoft.Extensions.Configuration;
using TestUtilities;

namespace DAPM.AccessControlService.Test.EndToEnd;

public class TestFixture
{
     public readonly IHttpClientFactory HttpClientFactory;
     public readonly AccessControlAdder AccessControlAdder;
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
     public const string RemoveUserPipelineRoute = $"{PipelineRoute}/remove-user-pipeline";
     public const string RemoveUserResourceRoute = $"{ResourceRoute}/remove-user-resource";
     public const string RemoveUserRepositoryRoute = $"{RepositoryRoute}/remove-user-repository";
     public const string RemoveUserOrganizationRoute = $"{OrganizationRoute}/remove-user-organization";
     public const string GetAllUserPipelinesRoute = $"{PipelineRoute}/get-all-user-pipelines";
     public const string GetAllUserResourcesRoute = $"{ResourceRoute}/get-all-user-resources";
     public const string GetAllUserRepositoriesRoute = $"{RepositoryRoute}/get-all-user-repositories";
     public const string GetAllUserOrganizationsRoute = $"{OrganizationRoute}/get-all-user-organizations";
     public const string AccessControlCheckAccessRoute = "check-access";
     
     public TestFixture()
     {
         var fixture = new TestUtility();
         HttpClientFactory = fixture.HttpClientFactory;
         AccessControlAdder = new AccessControlAdder(Users.Manager);
     }
}