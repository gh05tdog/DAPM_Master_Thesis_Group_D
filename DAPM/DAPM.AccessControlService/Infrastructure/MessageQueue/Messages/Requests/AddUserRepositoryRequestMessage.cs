using DAPM.AccessControlService.Core.Dtos;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;

public class AddUserRepositoryRequestMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public UserDto User { get; set; }
    public RepositoryDto Repository { get; set; }
}