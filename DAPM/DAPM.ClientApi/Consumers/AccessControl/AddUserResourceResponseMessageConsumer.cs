using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class AddUserResourceResponseMessageConsumer : IQueueConsumer<AddUserResourceReponseMessage>
{
    public Task ConsumeAsync(AddUserResourceReponseMessage message)
    {
        throw new NotImplementedException();
    }
}