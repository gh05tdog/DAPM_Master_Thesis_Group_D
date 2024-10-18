using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class AddUserRepositoryRequestMessageConsumer : IQueueConsumer<AddUserRepositoryRequestMessage>
{
    private readonly IRepositoryService repositoryService;
    private readonly IQueueProducer<AddUserRepositoryResponseMessage> queueProducer;

    public AddUserRepositoryRequestMessageConsumer(IRepositoryService repositoryService, IQueueProducer<AddUserRepositoryResponseMessage> queueProducer)
    {
        this.repositoryService = repositoryService;
        this.queueProducer = queueProducer;
    }

    public async Task ConsumeAsync(AddUserRepositoryRequestMessage message)
    {
        await repositoryService.AddUserRepository(message.User, message.Repository);
        
        var response = new AddUserRepositoryResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive
        };
        
        queueProducer.PublishMessage(response);
    }
}