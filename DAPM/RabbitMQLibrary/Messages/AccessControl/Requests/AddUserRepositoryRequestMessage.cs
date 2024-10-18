using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class AddUserRepositoryRequestMessage
{
    public UserDto User { get; set; }
    public RepositoryDto Repository { get; set; }
}