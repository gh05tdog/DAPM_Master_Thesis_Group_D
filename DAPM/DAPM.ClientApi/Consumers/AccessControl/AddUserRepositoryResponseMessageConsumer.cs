using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class AddUserRepositoryResponseMessageConsumer : IQueueConsumer<AddUserRepositoryResponseMessage>
{
    public Task ConsumeAsync(AddUserRepositoryResponseMessage message)
    {
        throw new NotImplementedException();
    }
}