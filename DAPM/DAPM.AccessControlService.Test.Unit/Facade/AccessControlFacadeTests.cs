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
    private readonly Mock<IOrganizationService> organizationServiceMock;

    public AccessControlFacadeTests()
    {
        pipelineServiceMock = new Mock<IPipelineService>();
        resourceServiceMock = new Mock<IResourceService>();
        repositoryServiceMock = new Mock<IRepositoryService>();
        organizationServiceMock = new Mock<IOrganizationService>();
    }

    [Fact]
    public async Task AddUserPipeline_ShouldResult_UserHasAccessToPipeline()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var pipeline = new PipelineDto { Id = Guid.NewGuid() };
        var addUserPipelineRequestMessage = new AddUserPipelineRequestMessage { User = user, Pipeline = pipeline };

        pipelineServiceMock.Setup(service => service.AddUserPipeline(user, pipeline)).ReturnsAsync(true);
        pipelineServiceMock.Setup(service => service.GetPipelinesForUser(user)).ReturnsAsync(new List<PipelineDto>{pipeline});;
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
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
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
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
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var addUserRepositoryResponseMessage = await accessControlFacade.AddUserRepository(addUserRepositoryRequestMessage);
        Assert.True(addUserRepositoryResponseMessage.Success);
        
        var getRepositoriesForUserRequestMessage = new GetRepositoriesForUserRequestMessage { User = user };
        var repositories = (await accessControlFacade.GetRepositoriesForUser(getRepositoriesForUserRequestMessage)).Repositories;

        Assert.Contains(repositories, p => p.Id == repository.Id);
        
        repositoryServiceMock.Verify(service => service.AddUserRepository(user, repository), Times.Once);
        repositoryServiceMock.Verify(service => service.GetRepositoriesForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task AddUserOrganization_ShouldResult_UserHasAccessToOrganization()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var organization = new OrganizationDto() { Id = Guid.NewGuid() };
        var addUserOrganizationRequestMessage = new AddUserOrganizationRequestMessage() { User = user, Organization = organization };

        organizationServiceMock.Setup(service => service.AddUserOrganization(user, organization)).ReturnsAsync(true);
        organizationServiceMock.Setup(service => service.GetOrganizationsForUser(user)).ReturnsAsync(new List<OrganizationDto>{organization});;
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var addUserRepositoryResponseMessage = await accessControlFacade.AddUserOrganization(addUserOrganizationRequestMessage);
        Assert.True(addUserRepositoryResponseMessage.Success);
        
        var getOrganizationsForUserRequestMessage = new GetOrganizationsForUserRequestMessage() { User = user };
        var organizations = (await accessControlFacade.GetOrganizationsForUser(getOrganizationsForUserRequestMessage)).Organizations;

        Assert.Contains(organizations, p => p.Id == organization.Id);
        
        organizationServiceMock.Verify(service => service.AddUserOrganization(user, organization), Times.Once);
        organizationServiceMock.Verify(service => service.GetOrganizationsForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task RemoveUserPipeline_ShouldResult_UserDoesNotHaveAccessToPipeline()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var pipeline = new PipelineDto { Id = Guid.NewGuid() };
        var removeUserPipelineRequestMessage = new RemoveUserPipelineRequestMessage { User = user, Pipeline = pipeline };

        pipelineServiceMock.Setup(service => service.RemoveUserPipeline(user, pipeline)).ReturnsAsync(true);
        pipelineServiceMock.Setup(service => service.GetPipelinesForUser(user)).ReturnsAsync(new List<PipelineDto>());
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var removeUserPipelineResponseMessage = await accessControlFacade.RemoveUserPipeline(removeUserPipelineRequestMessage);
        Assert.True(removeUserPipelineResponseMessage.Success);
        
        var getPipelineForUserRequestMessage = new GetPipelinesForUserRequestMessage { User = user };
        var pipelines = (await accessControlFacade.GetPipelinesForUser(getPipelineForUserRequestMessage)).Pipelines;

        Assert.DoesNotContain(pipelines, p => p.Id == pipeline.Id);
        
        pipelineServiceMock.Verify(service => service.RemoveUserPipeline(user, pipeline), Times.Once);
        pipelineServiceMock.Verify(service => service.GetPipelinesForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task RemoveUserResource_ShouldResult_UserDoesNotHaveAccessToResource()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var resource = new ResourceDto() { Id = Guid.NewGuid() };
        var removeUserResourceRequestMessage = new RemoveUserResourceRequestMessage() { User = user, Resource = resource };

        resourceServiceMock.Setup(service => service.RemoveUserResource(user, resource)).ReturnsAsync(true);
        resourceServiceMock.Setup(service => service.GetResourcesForUser(user)).ReturnsAsync(new List<ResourceDto>());
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var removeUserResourceResponseMessage = await accessControlFacade.RemoveUserResource(removeUserResourceRequestMessage);
        Assert.True(removeUserResourceResponseMessage.Success);
        
        var getResourcesForUserRequestMessage = new GetResourcesForUserRequestMessage { User = user };
        var resources = (await accessControlFacade.GetResourcesForUser(getResourcesForUserRequestMessage)).Resources;

        Assert.DoesNotContain(resources, p => p.Id == resource.Id);
        
        resourceServiceMock.Verify(service => service.RemoveUserResource(user, resource), Times.Once);
        resourceServiceMock.Verify(service => service.GetResourcesForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task RemoveUserRepository_ShouldResult_UserDoesNotHaveAccessToRepository()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var repository = new RepositoryDto() { Id = Guid.NewGuid() };
        var removeUserRepositoryRequestMessage = new RemoveUserRepositoryRequestMessage { User = user, Repository = repository };

        repositoryServiceMock.Setup(service => service.RemoveUserRepository(user, repository)).ReturnsAsync(true);
        repositoryServiceMock.Setup(service => service.GetRepositoriesForUser(user)).ReturnsAsync(new List<RepositoryDto>());
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var removeUserRepositoryResponseMessage = await accessControlFacade.RemoveUserRepository(removeUserRepositoryRequestMessage);
        Assert.True(removeUserRepositoryResponseMessage.Success);
        
        var getRepositoriesForUserRequestMessage = new GetRepositoriesForUserRequestMessage { User = user };
        var repositories = (await accessControlFacade.GetRepositoriesForUser(getRepositoriesForUserRequestMessage)).Repositories;

        Assert.DoesNotContain(repositories, p => p.Id == repository.Id);
        
        repositoryServiceMock.Verify(service => service.RemoveUserRepository(user, repository), Times.Once);
        repositoryServiceMock.Verify(service => service.GetRepositoriesForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task RemoveUserOrganization_ShouldResult_UserDoesNotHaveAccessToOrganization()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var organization = new OrganizationDto() { Id = Guid.NewGuid() };
        var removeUserOrganizationRequestMessage = new RemoveUserOrganizationRequestMessage() { User = user, Organization = organization };

        organizationServiceMock.Setup(service => service.RemoveUserOrganization(user, organization)).ReturnsAsync(true);
        organizationServiceMock.Setup(service => service.GetOrganizationsForUser(user)).ReturnsAsync(new List<OrganizationDto>());
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var removeUserOrganizationResponseMessage = await accessControlFacade.RemoveUserOrganization(removeUserOrganizationRequestMessage);
        Assert.True(removeUserOrganizationResponseMessage.Success);
        
        var getOrganizationsForUserRequestMessage = new GetOrganizationsForUserRequestMessage() { User = user };
        var organizations = (await accessControlFacade.GetOrganizationsForUser(getOrganizationsForUserRequestMessage)).Organizations;

        Assert.DoesNotContain(organizations, p => p.Id == organization.Id);
        
        organizationServiceMock.Verify(service => service.RemoveUserOrganization(user, organization), Times.Once);
        organizationServiceMock.Verify(service => service.GetOrganizationsForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task ReadAllUserOrganizations_ShouldReturnAllOrganizations()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var organization = new UserOrganizationDto() { UserId = Guid.NewGuid(), OrganizationId = Guid.NewGuid() };
        
        organizationServiceMock.Setup(service => service.GetAllUserOrganizations()).ReturnsAsync(new List<UserOrganizationDto>{organization});
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var organizations = (await accessControlFacade.GetAllUserOrganizations()).Organizations;

        Assert.Contains(organizations, p => p.OrganizationId == organization.OrganizationId);
        
        organizationServiceMock.Verify(service => service.GetOrganizationsForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task ReadAllUserRepositories_ShouldReturnAllRepositories()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var repository = new UserRepositoryDto() { UserId = Guid.NewGuid(), RepositoryId = Guid.NewGuid() };
        
        repositoryServiceMock.Setup(service => service.GetAllUserRepositories()).ReturnsAsync(new List<UserRepositoryDto>{repository});
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var repositories = (await accessControlFacade.GetAllUserRepositories()).Repositories;

        Assert.Contains(repositories, p => p.RepositoryId == repository.RepositoryId);
        
        repositoryServiceMock.Verify(service => service.GetRepositoriesForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task ReadAllUserPipelines_ShouldReturnAllPipelines()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var pipeline = new UserPipelineDto() { UserId = Guid.NewGuid(), PipelineId = Guid.NewGuid() };
        
        pipelineServiceMock.Setup(service => service.GetAllUserPipelines()).ReturnsAsync(new List<UserPipelineDto>{pipeline});
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var pipelines = (await accessControlFacade.GetAllUserPipelines()).Pipelines;

        Assert.Contains(pipelines, p => p.PipelineId == pipeline.PipelineId);
        
        pipelineServiceMock.Verify(service => service.GetPipelinesForUser(user), Times.Once);
    }
    
    [Fact]
    public async Task ReadAllUserResources_ShouldReturnAllResources()
    {
        var user = new UserDto { Id = Guid.NewGuid() };
        var resource = new UserResourceDto() { UserId = Guid.NewGuid(), ResourceId = Guid.NewGuid() };
        
        resourceServiceMock.Setup(service => service.GetAllUserResources()).ReturnsAsync(new List<UserResourceDto>{resource});
        
        var accessControlFacade = new AccessControlFacade(pipelineServiceMock.Object, resourceServiceMock.Object, repositoryServiceMock.Object, organizationServiceMock.Object);
        
        var resources = (await accessControlFacade.GetAllUserResources()).Resources;

        Assert.Contains(resources, p => p.ResourceId == resource.ResourceId);
        
        resourceServiceMock.Verify(service => service.GetResourcesForUser(user), Times.Once);
    }
}