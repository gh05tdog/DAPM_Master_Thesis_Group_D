using System.Collections.Concurrent;
using DAPM.AccessControlService.Core.Dtos;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Requests;
using DAPM.AccessControlService.Infrastructure.MessageQueue.Messages.Responses;
using DAPM.ClientApi.Services.Interfaces;
using RabbitMQLibrary.Interfaces;

namespace DAPM.ClientApi.Services;

public class AccessControlService : IAccessControlService
{
    private readonly ITicketService ticketService;
    private readonly IQueueProducer<AddUserPipelineRequestMessage> addUserPipelineRequestProducer;
    private readonly IQueueProducer<AddUserRepositoryRequestMessage> addUserRepositoryRequestProducer;
    private readonly IQueueProducer<AddUserResourceRequestMessage> addUserResourceRequestProducer;
    private readonly IQueueProducer<GetPipelinesForUserRequestMessage> getPipelinesForUserRequestProducer;
    private readonly IQueueProducer<GetRepositoriesForUserRequestMessage> getRepositoriesForUserRequestProducer;
    private readonly IQueueProducer<GetResourcesForUserRequestMessage> getResourcesForUserRequestProducer;
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<GetPipelinesForUserResponseMessage>> getPipelinesTaskCompletionSources = new();
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<GetResourcesForUserResponseMessage>> getResourcesTaskCompletionSources = new();
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<GetRepositoriesForUserResponseMessage>> getRepositoriesTaskCompletionSources = new();
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<AddUserPipelineResponseMessage>> addUserPipelineTaskCompletionSources = new();
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<AddUserRepositoryResponseMessage>> addUserRepositoryTaskCompletionSources = new();
    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<AddUserResourceReponseMessage>> addUserResourceTaskCompletionSources = new();
    
    public AccessControlService(ITicketService ticketService, 
        IQueueProducer<AddUserPipelineRequestMessage> addUserPipelineRequestProducer, 
        IQueueProducer<AddUserRepositoryRequestMessage> addUserRepositoryRequestProducer, 
        IQueueProducer<AddUserResourceRequestMessage> addUserResourceRequestProducer, 
        IQueueProducer<GetPipelinesForUserRequestMessage> getPipelinesForUserRequestProducer, 
        IQueueProducer<GetRepositoriesForUserRequestMessage> getRepositoriesForUserRequestProducer, 
        IQueueProducer<GetResourcesForUserRequestMessage> getResourcesForUserRequestProducer)
    {
        this.ticketService = ticketService;
        this.addUserPipelineRequestProducer = addUserPipelineRequestProducer;
        this.addUserRepositoryRequestProducer = addUserRepositoryRequestProducer;
        this.addUserResourceRequestProducer = addUserResourceRequestProducer;
        this.getPipelinesForUserRequestProducer = getPipelinesForUserRequestProducer;
        this.getRepositoriesForUserRequestProducer = getRepositoriesForUserRequestProducer;
        this.getResourcesForUserRequestProducer = getResourcesForUserRequestProducer;
    }

    public async Task<bool> UserHasAccessToPipeline(UserDto user, PipelineDto pipeline)
    {
        var ticketId = ticketService.CreateNewTicket(TicketResolutionType.Json);

        var message = new GetPipelinesForUserRequestMessage
        {
            MessageId = ticketId,
            TimeToLive = TimeSpan.FromMinutes(1),
            User = user
        };
        
        var tcs = new TaskCompletionSource<GetPipelinesForUserResponseMessage>();
        getPipelinesTaskCompletionSources.TryAdd(ticketId, tcs);
        
        getPipelinesForUserRequestProducer.PublishMessage(message);

        var response = await tcs.Task;
        getPipelinesTaskCompletionSources.TryRemove(ticketId, out _);
        
        return response.Pipelines.Contains(pipeline);
    }

    public async Task<bool> UserHasAccessToRepository(UserDto user, RepositoryDto repository)
    {
        var ticketId = ticketService.CreateNewTicket(TicketResolutionType.Json);
        
        var message = new GetRepositoriesForUserRequestMessage
        {
            MessageId = ticketId,
            TimeToLive = TimeSpan.FromMinutes(1),
            User = user
        };
        
        var tcs = new TaskCompletionSource<GetRepositoriesForUserResponseMessage>();
        getRepositoriesTaskCompletionSources.TryAdd(ticketId, tcs);
        
        getRepositoriesForUserRequestProducer.PublishMessage(message);
        
        var response = await tcs.Task;
        getRepositoriesTaskCompletionSources.TryRemove(ticketId, out _);
        
        return response.Repositories.Contains(repository);
    }

    public async Task<bool> UserHasAccessToResource(UserDto user, ResourceDto resource)
    {
        var ticketId = ticketService.CreateNewTicket(TicketResolutionType.Json);
        
        var message = new GetResourcesForUserRequestMessage
        {
            MessageId = ticketId,
            TimeToLive = TimeSpan.FromMinutes(1),
            User = user
        };
        
        var tcs = new TaskCompletionSource<GetResourcesForUserResponseMessage>();
        getResourcesTaskCompletionSources.TryAdd(ticketId, tcs);
        
        getResourcesForUserRequestProducer.PublishMessage(message);
        
        var response = await tcs.Task;
        getResourcesTaskCompletionSources.TryRemove(ticketId, out _);
        
        return response.Resources.Contains(resource);
    }

    public async Task<bool> AddUserToPipeline(UserDto user, PipelineDto pipeline)
    {
        var ticketId = ticketService.CreateNewTicket(TicketResolutionType.Json);
        
        var message = new AddUserPipelineRequestMessage
        {
            MessageId = ticketId,
            TimeToLive = TimeSpan.FromMinutes(1),
            User = user,
            Pipeline = pipeline
        };
        
        var tcs = new TaskCompletionSource<AddUserPipelineResponseMessage>();
        addUserPipelineTaskCompletionSources.TryAdd(ticketId, tcs);
        
        addUserPipelineRequestProducer.PublishMessage(message);
        
        await tcs.Task;
        addUserPipelineTaskCompletionSources.TryRemove(ticketId, out _);

        return true;
    }

    public async Task<bool> AddUserToResource(UserDto user, ResourceDto resource)
    {
        var ticketId = ticketService.CreateNewTicket(TicketResolutionType.Json);
        
        var message = new AddUserResourceRequestMessage
        {
            MessageId = ticketId,
            TimeToLive = TimeSpan.FromMinutes(1),
            User = user,
            Resource = resource
        };
        
        var tcs = new TaskCompletionSource<AddUserResourceReponseMessage>();
        addUserResourceTaskCompletionSources.TryAdd(ticketId, tcs);
        
        addUserResourceRequestProducer.PublishMessage(message);
        
        await tcs.Task;
        addUserResourceTaskCompletionSources.TryRemove(ticketId, out _);
        
        return true;
    }

    public async Task<bool> AddUserToRepository(UserDto user, RepositoryDto repository)
    {
        var ticketId = ticketService.CreateNewTicket(TicketResolutionType.Json);
        
        var message = new AddUserRepositoryRequestMessage
        {
            MessageId = ticketId,
            TimeToLive = TimeSpan.FromMinutes(1),
            User = user,
            Repository = repository
        };
        
        var tcs = new TaskCompletionSource<AddUserRepositoryResponseMessage>();
        addUserRepositoryTaskCompletionSources.TryAdd(ticketId, tcs);
        
        addUserRepositoryRequestProducer.PublishMessage(message);
        
        await tcs.Task;
        addUserRepositoryTaskCompletionSources.TryRemove(ticketId, out _);
        
        return true;
    }
}