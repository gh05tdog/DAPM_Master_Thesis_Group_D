using DAPM.AccessControlService.Core.Dtos;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;

public class GetPipelinesForUserResponseMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public ICollection<PipelineDto> Pipelines { get; set; }
}