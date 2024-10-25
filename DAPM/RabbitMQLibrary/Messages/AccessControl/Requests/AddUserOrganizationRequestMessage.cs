using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class AddUserOrganizationRequestMessage
{
    public UserDto User { get; set; }
    public OrganizationDto Organization { get; set; }
}