using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetResourcesForUserResponseMessage
{
    public ICollection<ResourceDto> Resources { get; set; }
}