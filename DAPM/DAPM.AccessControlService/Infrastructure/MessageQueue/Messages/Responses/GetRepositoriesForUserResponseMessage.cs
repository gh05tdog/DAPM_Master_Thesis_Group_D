using DAPM.AccessControlService.Core.Dtos;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;

public class GetRepositoriesForUserResponseMessage : IQueueMessage
{
    public Guid MessageId { get; set; }
    public TimeSpan TimeToLive { get; set; }
    public ICollection<RepositoryDto> RepositoryDtos { get; set; }
}