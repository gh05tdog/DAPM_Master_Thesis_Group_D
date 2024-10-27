using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetAllUserPipelinesResponseMessage
{
    public ICollection<UserPipelineDto> Pipelines { get; set; }
}