using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class GetPipelinesForUserResponseMessageConsumer : IQueueConsumer<GetPipelinesForUserResponseMessage>
{
    public Task ConsumeAsync(GetPipelinesForUserResponseMessage message)
    {
        throw new NotImplementedException();
    }
}