using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class GetRepositoriesForUserResponseMessageConsumer : IQueueConsumer<GetRepositoriesForUserResponseMessage>
{
    private readonly IAccessControlService accessControlService;

    public GetRepositoriesForUserResponseMessageConsumer(IAccessControlService accessControlService)
    {
        this.accessControlService = accessControlService;
    }

    public Task ConsumeAsync(GetRepositoriesForUserResponseMessage message)
    {
        accessControlService.HandleGetRepositoriesForUserResponseMessage(message);
        return Task.CompletedTask;
    }
}