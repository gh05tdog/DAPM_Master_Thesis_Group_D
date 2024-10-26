using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class GetPipelinesForUserRequestMessage
{
    public UserDto User { get; set; }
}