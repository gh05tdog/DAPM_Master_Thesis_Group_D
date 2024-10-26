using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetAllUserRepositoriesResponseMessage
{
    public ICollection<UserRepositoryDto> Repositories { get; set; }
}