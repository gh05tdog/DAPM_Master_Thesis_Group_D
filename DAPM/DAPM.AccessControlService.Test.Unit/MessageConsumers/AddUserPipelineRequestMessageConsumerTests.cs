using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Moq;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class AddUserPipelineRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockPipelineService = new Mock<IPipelineService>();
        var mockQueueProducer = new Mock<IQueueProducer<AddUserPipelineResponseMessage>>();

        var userDto = new UserDto(Guid.NewGuid());
        var pipeline = new PipelineDto(Guid.NewGuid());

        mockPipelineService.Setup(service => service.AddUserPipeline(userDto, pipeline))
            .Returns(Task.CompletedTask);

        var consumer = new AddUserPipelineRequestMessageConsumer(mockPipelineService.Object, mockQueueProducer.Object);

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