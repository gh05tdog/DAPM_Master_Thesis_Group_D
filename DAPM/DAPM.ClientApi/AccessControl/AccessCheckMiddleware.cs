using System.Security.Claims;
using System.Text;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.AccessControl;

public class AccessCheckMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IApiHttpClient _apiHttpClient;
    
    public AccessCheckMiddleware(RequestDelegate next, IApiHttpClient apiHttpClient)
    {
        _next = next;
        _apiHttpClient = apiHttpClient;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var organizationId = context.Request.RouteValues["organizationId"] as string;
        var repositoryId = context.Request.RouteValues["repositoryId"] as string;
        var pipelineId = context.Request.RouteValues["pipelineId"] as string;
        var resourceId = context.Request.RouteValues["resourceId"] as string;
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var userAccessRequest = new UserAccessRequestDto
        {
            User = userId is not null ? new UserDto { Id = Guid.Parse(userId) } : null,
            Organization = organizationId is not null ? new OrganizationDto { Id = Guid.Parse(organizationId) } : null,
            Repository = repositoryId is not null ? new RepositoryDto { Id = Guid.Parse(repositoryId) } : null,
            Pipeline = pipelineId is not null ? new PipelineDto { Id = Guid.Parse(pipelineId) } : null,
            Resource = resourceId is not null ? new ResourceDto { Id = Guid.Parse(resourceId) } : null
        };
        
        var userAccess = await _apiHttpClient.GetUserAccessAsync(userAccessRequest);

        var hasAccess = true;
        var sb = new StringBuilder();
        sb.AppendLine("User does not have access to:");
        
        if (organizationId is not null && !userAccess.HasOrganizationAccess)
        {
            hasAccess = false;
            sb.AppendLine($"Organization with id {organizationId}");
        }
        
        if (repositoryId is not null && !userAccess.HasRepositoryAccess)
        {
            hasAccess = false;
            sb.AppendLine($"Repository with id {repositoryId}");
        }
        
        if (pipelineId is not null && !userAccess.HasPipelineAccess)
        {
            hasAccess = false;
            sb.AppendLine($"Pipeline with id {pipelineId}");
        }
        
        if (resourceId is not null && !userAccess.HasResourceAccess)
        {
            hasAccess = false;
            sb.AppendLine($"Resource with id {resourceId}");
        }
        
        if (!hasAccess)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(sb.ToString());
            return;
        }
        
        await _next(context);
    }
}