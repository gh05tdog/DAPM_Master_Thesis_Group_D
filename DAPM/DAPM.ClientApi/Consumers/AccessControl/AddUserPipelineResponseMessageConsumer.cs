using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class AddUserPipelineResponseMessageConsumer : IQueueConsumer<AddUserPipelineResponseMessage>
{
    public Task ConsumeAsync(AddUserPipelineResponseMessage message)
    {
        throw new NotImplementedException();
    }
}