using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class AddUserPipelineRequestMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public UserDto User { get; set; }
    public PipelineDto Pipeline { get; set; }
}