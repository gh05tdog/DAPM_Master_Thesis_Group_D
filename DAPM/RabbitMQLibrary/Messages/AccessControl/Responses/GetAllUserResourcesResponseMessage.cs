using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetAllUserResourcesResponseMessage
{
    public ICollection<UserResourceDto> Resources { get; set; }
}