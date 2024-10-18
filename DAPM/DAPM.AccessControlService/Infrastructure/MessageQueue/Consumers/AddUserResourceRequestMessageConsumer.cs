using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class AddUserResourceRequestMessageConsumer : IQueueConsumer<AddUserResourceRequestMessage>
{
    private readonly IResourceService resourceService;
    private readonly IQueueProducer<AddUserResourceReponseMessage> queueProducer;
    private readonly ILogger<AddUserResourceRequestMessageConsumer> logger;

    public AddUserResourceRequestMessageConsumer(IResourceService resourceService, IQueueProducer<AddUserResourceReponseMessage> queueProducer, ILogger<AddUserResourceRequestMessageConsumer> logger)
    {
        this.resourceService = resourceService;
        this.queueProducer = queueProducer;
        this.logger = logger;
    }

    public async Task ConsumeAsync(AddUserResourceRequestMessage message)
    {
        logger.LogInformation($"Received {nameof(AddUserResourceRequestMessage)}");
        
        await resourceService.AddUserResource(message.User, message.Resource);
        
        var response = new AddUserResourceReponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive
        };
        
        queueProducer.PublishMessage(response);
    }
}