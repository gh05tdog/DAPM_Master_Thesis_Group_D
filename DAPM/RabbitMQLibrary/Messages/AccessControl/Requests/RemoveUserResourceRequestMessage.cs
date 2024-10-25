using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class RemoveUserResourceRequestMessage
{
    public UserDto User { get; set; }
    public ResourceDto Resource { get; set; }
}