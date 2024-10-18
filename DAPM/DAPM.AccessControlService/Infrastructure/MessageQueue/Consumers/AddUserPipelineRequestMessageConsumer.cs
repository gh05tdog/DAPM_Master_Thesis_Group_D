using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class AddUserPipelineRequestMessageConsumer : IQueueConsumer<AddUserPipelineRequestMessage>
{
    private readonly IPipelineService pipelineService;
    private readonly IQueueProducer<AddUserPipelineResponseMessage> queueProducer;

    public AddUserPipelineRequestMessageConsumer(IPipelineService pipelineService, IQueueProducer<AddUserPipelineResponseMessage> queueProducer)
    {
        this.pipelineService = pipelineService;
        this.queueProducer = queueProducer;
    }

    public async Task ConsumeAsync(AddUserPipelineRequestMessage message)
    {
        await pipelineService.AddUserPipeline(message.User, message.Pipeline);
        
        var response = new AddUserPipelineResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive
        };
        
        queueProducer.PublishMessage(response);
    }
}