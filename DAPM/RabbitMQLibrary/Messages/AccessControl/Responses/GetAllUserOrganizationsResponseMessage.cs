using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetAllUserOrganizationsResponseMessage
{
    public ICollection<UserOrganizationDto> Organizations { get; set; }
}