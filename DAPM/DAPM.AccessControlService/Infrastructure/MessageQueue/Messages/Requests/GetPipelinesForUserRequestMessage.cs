using DAPM.AccessControlService.Core.DTOs;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;

public class GetPipelinesForUserRequestMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public UserDto UserDto { get; set; }
}