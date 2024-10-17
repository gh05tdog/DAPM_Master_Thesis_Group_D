using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class GetResourcesForUserRequestMessageConsumer : IQueueConsumer<GetResourcesForUserRequestMessage>
{
    private readonly IResourceService resourceService;
    private readonly IQueueProducer<GetResourcesForUserResponseMessage> queueProducer;

    public GetResourcesForUserRequestMessageConsumer(IResourceService resourceService, IQueueProducer<GetResourcesForUserResponseMessage> queueProducer)
    {
        this.resourceService = resourceService;
        this.queueProducer = queueProducer;
    }

    public async Task ConsumeAsync(GetResourcesForUserRequestMessage message)
    {
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