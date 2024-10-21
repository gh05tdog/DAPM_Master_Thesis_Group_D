using DAPM.AccessControlService.Core.Services.Abstractions;
using DAPM.AccessControlService.Infrastructure;
using Moq;
using RabbitMQLibrary.Messages.AccessControl.Requests;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.AccessControlService.Test.Unit.Facade;

public class AccessControlFacadeTests
{
    private readonly Mock<IPipelineService> pipelineServiceMock;
    private readonly Mock<IResourceService> resourceServiceMock;
    private readonly Mock<IRepositoryService> repositoryServiceMock;

    public AccessControlFacadeTests()
    {
        pipelineServiceMock = new Mock<IPipelineService>();
        resourceServiceMock = new Mock<IResourceService>();
        repositoryServiceMock = new Mock<IRepositoryService>();
    }

    [Fact]
    public async Task AddUserPipeline_ShouldResult_UserHasAccessToPipeline()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var pipeline = new PipelineDto { Id = Guid.NewGuid() };
        var addUserPipelineRequestMessage = new AddUserPipelineRequestMessage { User = user, Pipeline = pipeline };

        pipelineServiceMock.Setup(service => service.AddUserPipeline(user, pipeline)).ReturnsAsync(true);
        pipelineServiceMock.Setup(service => service.GetPipelinesForUser(user)).ReturnsAsync(new List<PipelineDto>{pipeline});;
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object);
        
        var addUserPipelineResponseMessage = await accessControlFacade.AddUserPipeline(addUserPipelineRequestMessage);
        Assert.True(addUserPipelineResponseMessage.Success);
        
        var getPipelineForUserRequestMessage = new GetPipelinesForUserRequestMessage { User = user };
        var pipelines = (await accessControlFacade.GetPipelinesForUser(getPipelineForUserRequestMessage)).Pipelines;

        Assert.Contains(pipelines, p => p.Id == pipeline.Id);
        
        pipelineServiceMock.Verify(service => service.AddUserPipeline(user, pipeline), Times.Once);
        pipelineServiceMock.Verify(service => service.GetPipelinesForUser(user), Times.Once);
    }

    [Fact]
    public async Task AddUserResource_ShouldResult_UserHasAccessToResource()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var resource = new ResourceDto() { Id = Guid.NewGuid() };
        var addUserResourceRequestMessage = new AddUserResourceRequestMessage() { User = user, Resource = resource };

        resourceServiceMock.Setup(service => service.AddUserResource(user, resource)).ReturnsAsync(true);
        resourceServiceMock.Setup(service => service.GetResourcesForUser(user)).ReturnsAsync(new List<ResourceDto>{resource});;
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object);
        
        var addUserResourceResponseMessage = await accessControlFacade.AddUserResource(addUserResourceRequestMessage);
        Assert.True(addUserResourceResponseMessage.Success);
        
        var getResourcesForUserRequestMessage = new GetResourcesForUserRequestMessage { User = user };
        var resources = (await accessControlFacade.GetResourcesForUser(getResourcesForUserRequestMessage)).Resources;

        Assert.Contains(resources, p => p.Id == resource.Id);
        
        resourceServiceMock.Verify(service => service.AddUserResource(user, resource), Times.Once);
        resourceServiceMock.Verify(service => service.GetResourcesForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task AddUserRepository_ShouldResult_UserHasAccessToRepository()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var repository = new RepositoryDto() { Id = Guid.NewGuid() };
        var addUserRepositoryRequestMessage = new AddUserRepositoryRequestMessage { User = user, Repository = repository };

        repositoryServiceMock.Setup(service => service.AddUserRepository(user, repository)).ReturnsAsync(true);
        repositoryServiceMock.Setup(service => service.GetRepositoriesForUser(user)).ReturnsAsync(new List<RepositoryDto>{repository});;
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object);
        
        var addUserRepositoryResponseMessage = await accessControlFacade.AddUserRepository(addUserRepositoryRequestMessage);
        Assert.True(addUserRepositoryResponseMessage.Success);
        
        var getRepositoriesForUserRequestMessage = new GetRepositoriesForUserRequestMessage { User = user };
        var repositories = (await accessControlFacade.GetRepositoriesForUser(getRepositoriesForUserRequestMessage)).Repositories;

        Assert.Contains(repositories, p => p.Id == repository.Id);
        
        repositoryServiceMock.Verify(service => service.AddUserRepository(user, repository), Times.Once);
        repositoryServiceMock.Verify(service => service.GetRepositoriesForUser(user), Times.Once);
    }
}