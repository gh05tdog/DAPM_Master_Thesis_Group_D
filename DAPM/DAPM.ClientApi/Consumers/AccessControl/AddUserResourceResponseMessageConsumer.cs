using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class AddUserResourceResponseMessageConsumer : IQueueConsumer<AddUserResourceReponseMessage>
{
    private readonly IAccessControlService accessControlService;

    public AddUserResourceResponseMessageConsumer(IAccessControlService accessControlService)
    {
        this.accessControlService = accessControlService;
    }

    public Task ConsumeAsync(AddUserResourceReponseMessage message)
    {
        accessControlService.HandleAddUserResourceResponseMessage(message);
        return Task.CompletedTask;
    }
}