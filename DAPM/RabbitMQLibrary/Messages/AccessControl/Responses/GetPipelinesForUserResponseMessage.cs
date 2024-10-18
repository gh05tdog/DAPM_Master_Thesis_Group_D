using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class GetPipelinesForUserResponseMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public ICollection<PipelineDto> Pipelines { get; set; }
}