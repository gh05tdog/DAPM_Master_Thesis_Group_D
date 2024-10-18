using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class GetPipelinesForUserRequestMessageConsumer : IQueueConsumer<GetPipelinesForUserRequestMessage>
{
    private readonly IPipelineService pipelineService;
    private readonly IQueueProducer<GetPipelinesForUserResponseMessage> queueProducer;

    public GetPipelinesForUserRequestMessageConsumer(IPipelineService pipelineService, IQueueProducer<GetPipelinesForUserResponseMessage> queueProducer)
    {
        this.pipelineService = pipelineService;
        this.queueProducer = queueProducer;
    }

    public async Task ConsumeAsync(GetPipelinesForUserRequestMessage message)
    {
        var pipelines = await pipelineService.GetPipelinesForUser(message.User);
        
        var response = new GetPipelinesForUserResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive,
            Pipelines = pipelines
        };
        
        queueProducer.PublishMessage(response);
    }
}