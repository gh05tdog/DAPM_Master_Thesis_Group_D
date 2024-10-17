using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class GetResourcesForUserRequestMessageConsumer : IQueueConsumer<GetPipelinesForUserRequestMessage>
{
    private readonly IResourceService resourceService;
    private readonly IQueueProducer<GetResourcesForUserResponseMessage> queueProducer;
    
    public async Task ConsumeAsync(GetPipelinesForUserRequestMessage message)
    {
        var resources = await resourceService.GetResourcesForUser(message.UserDto);
        
        var response = new GetResourcesForUserResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive,
            ResourceDtos = resources
        };
        
        queueProducer.PublishMessage(response);
    }
}