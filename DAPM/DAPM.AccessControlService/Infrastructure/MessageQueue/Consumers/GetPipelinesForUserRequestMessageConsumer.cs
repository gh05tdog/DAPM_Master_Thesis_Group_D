using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using RabbitMQLibrary.Interfaces;

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
        var pipelines = await pipelineService.GetPipelinesForUser(message.UserDto);
        
        var response = new GetPipelinesForUserResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive,
            PipelineDtos = pipelines
        };
        
        queueProducer.PublishMessage(response);
    }
}