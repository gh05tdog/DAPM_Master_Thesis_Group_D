using RabbitMQLibrary.Interfaces;

namespace RabbitMQLibrary.Messages.AccessControl.Responses;

public class AddUserResourceResponseMessage
{
    public bool Success { get; set; }
}