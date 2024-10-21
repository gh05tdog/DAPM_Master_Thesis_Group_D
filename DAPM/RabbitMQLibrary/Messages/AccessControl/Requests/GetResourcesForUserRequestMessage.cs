using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class GetResourcesForUserRequestMessage
{
    public UserDto User { get; set; }
}