using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetPipelinesForUserResponseMessage
{
    public ICollection<PipelineDto> Pipelines { get; set; }
}