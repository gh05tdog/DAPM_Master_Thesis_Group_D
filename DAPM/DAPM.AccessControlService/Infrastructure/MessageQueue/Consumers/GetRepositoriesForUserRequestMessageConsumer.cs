using DAPM.AccessControlService.Core.Services.Abstractions;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;

public class GetRepositoriesForUserRequestMessageConsumer : IQueueConsumer<GetRepositoriesForUserRequestMessage>
{
    private readonly IRepositoryService repositoryService;
    private readonly IQueueProducer<GetRepositoriesForUserResponseMessage> queueProducer;
    private readonly ILogger<GetRepositoriesForUserRequestMessageConsumer> logger;
    
    public GetRepositoriesForUserRequestMessageConsumer(IRepositoryService repositoryService, IQueueProducer<GetRepositoriesForUserResponseMessage> queueProducer, ILogger<GetRepositoriesForUserRequestMessageConsumer> logger)
    {
        this.repositoryService = repositoryService;
        this.queueProducer = queueProducer;
        this.logger = logger;
    }

    public async Task ConsumeAsync(GetRepositoriesForUserRequestMessage message)
    {
        logger.LogInformation($"Received {nameof(GetRepositoriesForUserRequestMessage)}");
        
        var repositories = await repositoryService.GetRepositoriesForUser(message.User);
        
        var response = new GetRepositoriesForUserResponseMessage
        {
            MessageId = message.MessageId,
            TimeToLive = message.TimeToLive,
            Repositories = repositories
        };
        
        queueProducer.PublishMessage(response);
    }
}