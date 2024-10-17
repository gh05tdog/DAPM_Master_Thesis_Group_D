using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class GetRepositoriesForUserResponseMessageConsumer : IQueueConsumer<GetRepositoriesForUserResponseMessage>
{
    public Task ConsumeAsync(GetRepositoriesForUserResponseMessage message)
    {
        throw new NotImplementedException();
    }
}