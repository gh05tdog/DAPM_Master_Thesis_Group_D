using DAPM.ClientApi.AccessControl;
using DAPM.ClientApi.Extensions;
using DAPM.ClientApi.Models;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DAPM.ClientApi.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("organizations/")]
    public class ResourceController : BaseController
    {
        private readonly IResourceService resourceService;

        public ResourceController(IResourceService resourceService, IAccessControlService accessControlService) : base(accessControlService)
        {
            this.resourceService = resourceService;
        }

        [HttpGet("{organizationId}/repositories/{repositoryId}/resources/{resourceId}")]
        [SwaggerOperation(Description = "Gets a resource by id from a specific repository. The result of this endpoint does not include the resource file. There needs to be " +
            "a collaboration agreement to retrieve this information.")]
        public async Task<ActionResult<Guid>> GetResourceById(Guid organizationId, Guid repositoryId, Guid resourceId)
        {
            if (!await HasOrganizationAccess(organizationId))
                return UnauthorizedResponse("organization", organizationId);
            
            if (!await HasRepositoryAccess(repositoryId))
                return UnauthorizedResponse("repository", repositoryId);

            if (!await HasResourceAccess(resourceId))
                return UnauthorizedResponse("resource", resourceId);
            
            Guid id = resourceService.GetResourceById(organizationId, repositoryId, resourceId, this.UserId());
            return Ok(new ApiResponse { RequestName = "GetResourceById", TicketId = id });
        }

        [HttpGet("{organizationId}/repositories/{repositoryId}/resources/{resourceId}/file")]
        [SwaggerOperation(Description = "Gets a resource file by id from a specific repository. There needs to be " +
            "a collaboration agreement to retrieve this information.")]
        public async Task<ActionResult<Guid>> GetResourceFileById(Guid organizationId, Guid repositoryId, Guid resourceId)
        {
            if (!await HasOrganizationAccess(organizationId))
                return UnauthorizedResponse("organization", organizationId);
            
            if (!await HasRepositoryAccess(repositoryId))
                return UnauthorizedResponse("repository", repositoryId);

            if (!await HasResourceAccess(resourceId))
                return UnauthorizedResponse("resource", resourceId);
            
            Guid id = resourceService.GetResourceFileById(organizationId, repositoryId, resourceId, this.UserId());
            return Ok(new ApiResponse { RequestName = "GetResourceFileById", TicketId = id });
        }
    }
    
}
