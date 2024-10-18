using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class GetResourcesForUserRequestMessageConsumer : IQueueConsumer<GetResourcesForUserRequestMessage>
{
    private readonly IResourceService resourceService;
    private readonly IQueueProducer<GetResourcesForUserResponseMessage> queueProducer;
    private readonly ILogger<GetResourcesForUserRequestMessageConsumer> logger;
    
    public GetResourcesForUserRequestMessageConsumer(IResourceService resourceService, IQueueProducer<GetResourcesForUserResponseMessage> queueProducer, ILogger<GetResourcesForUserRequestMessageConsumer> logger)
    {
        this.resourceService = resourceService;
        this.queueProducer = queueProducer;
        this.logger = logger;
    }

    public async Task ConsumeAsync(GetResourcesForUserRequestMessage message)
    {
        logger.LogInformation($"Received {nameof(GetResourcesForUserRequestMessage)}");
        
        var resources = await resourceService.GetResourcesForUser(message.User);
        
        var response = new GetResourcesForUserResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive,
            Resources = resources
        };
        
        queueProducer.PublishMessage(response);
    }
}