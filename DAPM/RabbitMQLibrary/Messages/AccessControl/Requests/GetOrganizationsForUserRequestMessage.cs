using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class GetOrganizationsForUserRequestMessage
{
    public UserDto User { get; set; }
}