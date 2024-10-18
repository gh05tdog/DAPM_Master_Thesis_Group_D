using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class GetPipelinesForUserResponseMessageConsumer : IQueueConsumer<GetPipelinesForUserResponseMessage>
{
    private readonly IAccessControlService accessControlService;

    public GetPipelinesForUserResponseMessageConsumer(IAccessControlService accessControlService)
    {
        this.accessControlService = accessControlService;
    }

    public Task ConsumeAsync(GetPipelinesForUserResponseMessage message)
    {
        accessControlService.HandleGetPipelinesForUserResponseMessage(message);
        return Task.CompletedTask;
    }
}