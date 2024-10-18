using RabbitMQLibrary.Models.AccessControl;

namespace RabbitMQLibrary.Messages.AccessControl.Requests;

public class AddUserPipelineRequestMessage
{
    public UserDto User { get; set; }
    public PipelineDto Pipeline { get; set; }
}