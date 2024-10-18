using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetRepositoriesForUserResponseMessage
{
    public ICollection<RepositoryDto> Repositories { get; set; }
}