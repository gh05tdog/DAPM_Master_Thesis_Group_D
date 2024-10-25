using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class RemoveUserPipelineRequestMessage
{
    public UserDto User { get; set; }
    public PipelineDto Pipeline { get; set; }
}