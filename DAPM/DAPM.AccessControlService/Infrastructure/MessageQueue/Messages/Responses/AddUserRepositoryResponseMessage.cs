using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;

public class AddUserRepositoryResponseMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
}