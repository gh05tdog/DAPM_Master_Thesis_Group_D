using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class AddUserResourceRequestMessageConsumer : IQueueConsumer<AddUserResourceRequestMessage>
{
    private readonly IResourceService resourceService;
    private readonly IQueueProducer<AddUserResourceRequestMessage> queueProducer;
    public async Task ConsumeAsync(AddUserResourceRequestMessage message)
    {
        await resourceService.AddUserResource(message.UserDto, message.ResourceDto);
        
        var response = new AddUserResourceRequestMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive
        };
        
        queueProducer.PublishMessage(response);
    }
}