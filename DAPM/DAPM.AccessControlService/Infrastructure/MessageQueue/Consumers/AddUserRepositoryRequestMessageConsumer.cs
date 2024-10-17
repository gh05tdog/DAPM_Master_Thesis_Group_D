using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class AddUserRepositoryRequestMessageConsumer : IQueueConsumer<AddUserRepositoryRequestMessage>
{
    private readonly IRepositoryService repositoryService;
    private readonly IQueueProducer<AddUserRepositoryResponseMessage> queueProducer;
    
    public async Task ConsumeAsync(AddUserRepositoryRequestMessage message)
    {
        await repositoryService.AddUserRepository(message.UserDto, message.RepositoryDto);
        
        var response = new AddUserRepositoryResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive
        };
        
        queueProducer.PublishMessage(response);
    }
}