using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class GetPipelinesForUserRequestMessageConsumer : IQueueConsumer<GetPipelinesForUserRequestMessage>
{
    private readonly IPipelineService pipelineService;
    private readonly IQueueProducer<GetPipelinesForUserResponseMessage> queueProducer;
    private readonly ILogger<GetPipelinesForUserRequestMessageConsumer> logger;

    public GetPipelinesForUserRequestMessageConsumer(IPipelineService pipelineService, IQueueProducer<GetPipelinesForUserResponseMessage> queueProducer, ILogger<GetPipelinesForUserRequestMessageConsumer> logger)
    {
        this.pipelineService = pipelineService;
        this.queueProducer = queueProducer;
        this.logger = logger;
    }

    public async Task ConsumeAsync(GetPipelinesForUserRequestMessage message)
    {
        logger.LogInformation($"Received {nameof(GetPipelinesForUserRequestMessage)}");
        
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