using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class GetRepositoriesForUserRequestMessage
{
    public UserDto User { get; set; }
}