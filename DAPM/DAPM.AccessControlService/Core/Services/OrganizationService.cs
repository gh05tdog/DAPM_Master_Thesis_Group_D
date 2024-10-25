using DAPM.AccessControlService.Core.Domain.Entities;
using DAPM.AccessControlService.Core.Domain.Repositories;
using DAPM.AccessControlService.Core.Extensions;
using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Core.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        this.organizationRepository = organizationRepository;
    }

    public async Task<bool> AddUserOrganization(UserDto user, OrganizationDto organization)
    {
        var userId = user.ToUserId();
        var organizationId = organization.ToOrganizationId();
        await organizationRepository.CreateUserOrganization(new UserOrganization(userId, organizationId));
        return true;
    }

    public async Task<ICollection<OrganizationDto>> GetOrganizationsForUser(UserDto user)
    {
        var userId = user.ToUserId();
        var organizationIds = await organizationRepository.ReadOrganizationsForUser(userId);
        return organizationIds.Select(o => new OrganizationDto{Id = o.Id}).ToList();
    }
}