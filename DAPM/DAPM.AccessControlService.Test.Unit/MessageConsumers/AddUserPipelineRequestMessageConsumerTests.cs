using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class AddUserPipelineRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockPipelineService = new Mock<IPipelineService>();
        var mockQueueProducer = new Mock<IQueueProducer<AddUserPipelineResponseMessage>>();
        var mockLogger = new Mock<ILogger<AddUserPipelineRequestMessageConsumer>>();

        var userDto = new UserDto{Id = Guid.NewGuid()};
        var pipeline = new PipelineDto{Id = Guid.NewGuid()};

        mockPipelineService.Setup(service => service.AddUserPipeline(userDto, pipeline))
            .Returns(Task.CompletedTask);

        var consumer = new AddUserPipelineRequestMessageConsumer(mockPipelineService.Object, mockQueueProducer.Object, mockLogger.Object);

        var requestMessage = new AddUserPipelineRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            User = userDto,
            Pipeline = pipeline
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<AddUserPipelineResponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive 
        )), Times.Once);
    }
}