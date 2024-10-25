using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class RemoveUserOrganizationRequestMessage
{
    public UserDto User { get; set; }
    public OrganizationDto Organization { get; set; }
}