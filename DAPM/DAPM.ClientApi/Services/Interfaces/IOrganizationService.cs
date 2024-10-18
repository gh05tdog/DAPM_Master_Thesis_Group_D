using DAPM.ClientApi.Models;
using System.Xml.Linq;
using DAPM.ClientApi.Models.DTOs;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IOrganizationService
    {
        public Guid GetOrganizations(Guid userId);
        public Guid GetOrganizationById(Guid organizationId, Guid userId);
        public Guid GetRepositoriesOfOrganization(Guid organizationId, Guid userId);
        public Guid PostRepositoryToOrganization(Guid organizationId, string name, Guid userId);
    }
}
