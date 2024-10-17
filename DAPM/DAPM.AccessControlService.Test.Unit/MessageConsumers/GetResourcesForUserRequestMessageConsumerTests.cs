using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Moq;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class GetResourcesForUserRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockResourceService = new Mock<IResourceService>();
        var mockQueueProducer = new Mock<IQueueProducer<GetResourcesForUserResponseMessage>>();

        var userDto = new UserDto(Guid.NewGuid());
        var resources = new List<ResourceDto> { new(Guid.NewGuid()) };

        mockResourceService.Setup(service => service.GetResourcesForUser(userDto))
            .ReturnsAsync(resources);

        var consumer = new GetResourcesForUserRequestMessageConsumer(mockResourceService.Object, mockQueueProducer.Object);

        var requestMessage = new GetResourcesForUserRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            UserDto = userDto
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<GetResourcesForUserResponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive &&
            response.ResourceDtos == resources
        )), Times.Once);
    }
}