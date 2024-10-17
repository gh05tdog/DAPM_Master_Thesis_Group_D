using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class GetResourcesForUserResponseMessageConsumer : IQueueConsumer<GetResourcesForUserResponseMessage>
{
    private readonly IAccessControlService accessControlService;

    public GetResourcesForUserResponseMessageConsumer(IAccessControlService accessControlService)
    {
        this.accessControlService = accessControlService;
    }

    public Task ConsumeAsync(GetResourcesForUserResponseMessage message)
    {
        accessControlService.HandleGetResourcesForUserResponseMessage(message);
        return Task.CompletedTask;
    }
}