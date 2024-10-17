using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class AddUserRepositoryResponseMessageConsumer : IQueueConsumer<AddUserRepositoryResponseMessage>
{
    private readonly IAccessControlService accessControlService;

    public AddUserRepositoryResponseMessageConsumer(IAccessControlService accessControlService)
    {
        this.accessControlService = accessControlService;
    }

    public Task ConsumeAsync(AddUserRepositoryResponseMessage message)
    {
        accessControlService.HandleAddUserRepositoryResponseMessage(message);
        return Task.CompletedTask;
    }
}