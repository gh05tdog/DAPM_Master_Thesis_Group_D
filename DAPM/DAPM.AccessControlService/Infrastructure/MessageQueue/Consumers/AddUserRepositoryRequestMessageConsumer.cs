using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class AddUserRepositoryRequestMessageConsumer : IQueueConsumer<AddUserRepositoryRequestMessage>
{
    private readonly IRepositoryService repositoryService;
    private readonly IQueueProducer<AddUserRepositoryResponseMessage> queueProducer;
    private readonly ILogger<AddUserRepositoryRequestMessageConsumer> logger;
    
    public AddUserRepositoryRequestMessageConsumer(IRepositoryService repositoryService, IQueueProducer<AddUserRepositoryResponseMessage> queueProducer, ILogger<AddUserRepositoryRequestMessageConsumer> logger)
    {
        this.repositoryService = repositoryService;
        this.queueProducer = queueProducer;
        this.logger = logger;
    }

    public async Task ConsumeAsync(AddUserRepositoryRequestMessage requestMessage)
    {
        logger.LogInformation($"Received {nameof(AddUserRepositoryRequestMessage)}");
        
        await repositoryService.AddUserRepository(requestMessage.User, requestMessage.Repository);
        
        var response = new AddUserRepositoryResponseMessage
        {
            MessageId = requestMessage.MessageId,
            TimeToLive = requestMessage.TimeToLive
        };
        
        queueProducer.PublishMessage(response);
    }
}