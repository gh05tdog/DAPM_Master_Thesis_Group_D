using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using Moq;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class GetRepositoriesForUserRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockRepositoryService = new Mock<IRepositoryService>();
        var mockQueueProducer = new Mock<IQueueProducer<GetRepositoriesForUserResponseMessage>>();

        var userDto = new UserDto{Id = Guid.NewGuid()};
        var repositories = new List<RepositoryDto> { new RepositoryDto{Id = Guid.NewGuid()} };

        mockRepositoryService.Setup(service => service.GetRepositoriesForUser(userDto))
            .ReturnsAsync(repositories);

        var consumer = new GetRepositoriesForUserRequestMessageConsumer(mockRepositoryService.Object, mockQueueProducer.Object);

        var requestMessage = new GetRepositoriesForUserRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            User = userDto
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<GetRepositoriesForUserResponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive &&
            response.Repositories == repositories
        )), Times.Once);
    }
}