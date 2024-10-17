using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Consumers.AccessControl;

public class AddUserPipelineResponseMessageConsumer : IQueueConsumer<AddUserPipelineResponseMessage>
{
    private readonly IAccessControlService accessControlService;

    public AddUserPipelineResponseMessageConsumer(IAccessControlService accessControlService)
    {
        this.accessControlService = accessControlService;
    }

    public Task ConsumeAsync(AddUserPipelineResponseMessage message)
    {
        accessControlService.HandleAddUserPipelineResponseMessage(message);
        return Task.CompletedTask;
    }
}