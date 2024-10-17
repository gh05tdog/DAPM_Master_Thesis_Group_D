using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Moq;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class AddUserResourceRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockResourceService = new Mock<IResourceService>();
        var mockQueueProducer = new Mock<IQueueProducer<AddUserResourceReponseMessage>>();

        var userDto = new UserDto(Guid.NewGuid());
        var resource = new ResourceDto(Guid.NewGuid());

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