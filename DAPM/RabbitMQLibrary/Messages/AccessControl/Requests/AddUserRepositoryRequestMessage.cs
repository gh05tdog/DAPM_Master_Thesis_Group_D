using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class AddUserRepositoryRequestMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public UserDto User { get; set; }
    public RepositoryDto Repository { get; set; }
}