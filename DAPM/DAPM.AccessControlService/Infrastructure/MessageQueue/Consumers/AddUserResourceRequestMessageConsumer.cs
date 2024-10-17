using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class AddUserResourceRequestMessageConsumer : IQueueConsumer<AddUserResourceRequestMessage>
{
    private readonly IResourceService resourceService;
    private readonly IQueueProducer<AddUserResourceReponseMessage> queueProducer;

    public AddUserResourceRequestMessageConsumer(IResourceService resourceService, IQueueProducer<AddUserResourceReponseMessage> queueProducer)
    {
        this.resourceService = resourceService;
        this.queueProducer = queueProducer;
    }

    public async Task ConsumeAsync(AddUserResourceRequestMessage message)
    {
        await resourceService.AddUserResource(message.User, message.Resource);
        
        var response = new AddUserResourceReponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive
        };
        
        queueProducer.PublishMessage(response);
    }
}