using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Moq;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class AddUserRepositoryRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockRepositoryService = new Mock<IRepositoryService>();
        var mockQueueProducer = new Mock<IQueueProducer<AddUserRepositoryResponseMessage>>();

        var userDto = new UserDto(Guid.NewGuid());
        var repository = new RepositoryDto(Guid.NewGuid());

        mockRepositoryService.Setup(service => service.AddUserRepository(userDto, repository))
            .Returns(Task.CompletedTask);

        var consumer = new AddUserRepositoryRequestMessageConsumer(mockRepositoryService.Object, mockQueueProducer.Object);

        var requestMessage = new AddUserRepositoryRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            User = userDto,
            Repository = repository
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<AddUserRepositoryResponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive
        )), Times.Once);
    }
}