using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Messages.AccessControl.Responses;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class GetResourcesForUserRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockResourceService = new Mock<IResourceService>();
        var mockQueueProducer = new Mock<IQueueProducer<GetResourcesForUserResponseMessage>>();
        var mockLogger = new Mock<ILogger<GetResourcesForUserRequestMessageConsumer>>();

        var userDto = new UserDto{Id = Guid.NewGuid()};
        var resources = new List<ResourceDto> { new ResourceDto{Id = Guid.NewGuid()} };

        mockResourceService.Setup(service => service.GetResourcesForUser(userDto))
            .ReturnsAsync(resources);

        var consumer = new GetResourcesForUserRequestMessageConsumer(mockResourceService.Object, mockQueueProducer.Object, mockLogger.Object);

        var requestMessage = new GetResourcesForUserRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            User = userDto
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<GetResourcesForUserResponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive &&
            response.Resources == resources
        )), Times.Once);
    }
}