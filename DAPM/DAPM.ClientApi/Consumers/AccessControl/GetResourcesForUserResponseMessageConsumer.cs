using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class GetResourcesForUserResponseMessageConsumer : IQueueConsumer<GetResourcesForUserResponseMessage>
{
    public Task ConsumeAsync(GetResourcesForUserResponseMessage message)
    {
        throw new NotImplementedException();
    }
}