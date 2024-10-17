using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Consumers;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using Moq;
using RabbitMQLibrary.Interfaces;

namespace DAPM.AccessControlService.Test.Unit.MessageConsumers;

public class GetPipelinesForUserRequestMessageConsumerTests
{
    [Fact]
    public async Task ConsumeAsync_ShouldPublishResponseMessage()
    {
        // Arrange
        var mockPipelineService = new Mock<IPipelineService>();
        var mockQueueProducer = new Mock<IQueueProducer<GetPipelinesForUserResponseMessage>>();

        var userDto = new UserDto{Id = Guid.NewGuid()};
        var pipelines = new List<PipelineDto> { new PipelineDto{Id = Guid.NewGuid()} };

        mockPipelineService.Setup(service => service.GetPipelinesForUser(userDto))
            .ReturnsAsync(pipelines);

        var consumer = new GetPipelinesForUserRequestMessageConsumer(mockPipelineService.Object, mockQueueProducer.Object);

        var requestMessage = new GetPipelinesForUserRequestMessage
        {
            MessageId = Guid.NewGuid(),
            TimeToLive = TimeSpan.FromMinutes(5),
            User = userDto
        };

        // Act
        await consumer.ConsumeAsync(requestMessage);

        // Assert
        mockQueueProducer.Verify(producer => producer.PublishMessage(It.Is<GetPipelinesForUserResponseMessage>(response =>
            response.MessageId == requestMessage.MessageId &&
            response.TimeToLive == requestMessage.TimeToLive &&
            response.Pipelines == pipelines
        )), Times.Once);
    }
}