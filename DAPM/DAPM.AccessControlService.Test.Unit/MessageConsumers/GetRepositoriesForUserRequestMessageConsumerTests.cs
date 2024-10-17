using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Moq;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class GetRepositoriesForUserRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockRepositoryService = new Mock<IRepositoryService>();
        var mockQueueProducer = new Mock<IQueueProducer<GetRepositoriesForUserResponseMessage>>();

        var userDto = new UserDto(Guid.NewGuid());
        var repositories = new List<RepositoryDto> { new(Guid.NewGuid()) };

        mockRepositoryService.Setup(service => service.GetRepositoriesForUser(userDto))
            .ReturnsAsync(repositories);

        var consumer = new GetRepositoriesForUserRequestMessageConsumer(mockRepositoryService.Object, mockQueueProducer.Object);

        var requestMessage = new GetRepositoriesForUserRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            UserDto = userDto
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<GetRepositoriesForUserResponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive &&
            response.RepositoryDtos == repositories
        )), Times.Once);
    }
}