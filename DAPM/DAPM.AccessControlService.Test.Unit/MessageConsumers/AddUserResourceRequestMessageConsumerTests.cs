using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Moq;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class AddUserResourceRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockResourceService = new Mock<IResourceService>();
        var mockQueueProducer = new Mock<IQueueProducer<AddUserResourceReponseMessage>>();

        var userDto = new UserDto{Id = Guid.NewGuid()};
        var resource = new ResourceDto{Id = Guid.NewGuid()};

        mockResourceService.Setup(service => service.AddUserResource(userDto, resource))
            .Returns(Task.CompletedTask);

        var consumer = new AddUserResourceRequestMessageConsumer(mockResourceService.Object, mockQueueProducer.Object);

        var requestMessage = new AddUserResourceRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            User = userDto,
            Resource = resource
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<AddUserResourceReponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive 
        )), Times.Once);
    }
}