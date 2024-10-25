using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetOrganizationsForUserResponseMessage
{
    public ICollection<OrganizationDto> Organizations { get; set; }
}