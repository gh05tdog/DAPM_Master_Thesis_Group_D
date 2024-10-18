﻿using System.Collections.Concurrent;
using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.Orchestrator.ProcessRequests;
using RabbitMQLibrary.Messages.ResourceRegistry;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ILogger<OrganizationService> _logger;
        private readonly IQueueProducer<GetRepositoriesRequest> _getRepositoriesRequestProducer;
        private readonly IQueueProducer<GetOrganizationsRequest> _getOrganizationsRequestProducer;
        private readonly IQueueProducer<PostRepositoryRequest> _postRepositoryRequestProducer;
        private readonly ITicketService _ticketService;
        private static readonly ConcurrentDictionary<Guid, Guid> TicketToUser = new();
        
        public UserDto GetUserFromTicket(Guid ticketId) 
            =>  new UserDto { Id = TicketToUser[ticketId]};

        public OrganizationService(ILogger<OrganizationService> logger, 
            IQueueProducer<GetOrganizationsMessage> getOrgsProducer, 
            IQueueProducer<GetOrganizationsMessage> getOrgByIdProducer,
            IQueueProducer<GetRepositoriesRequest> getRepositoriesRequestProducer,
            IQueueProducer<GetOrganizationsRequest> getOrganizationsRequestProducer,
            IQueueProducer<PostRepositoryRequest> postRepositoryRequestProducer,
            ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _getRepositoriesRequestProducer = getRepositoriesRequestProducer;
            _getOrganizationsRequestProducer = getOrganizationsRequestProducer;
            _postRepositoryRequestProducer = postRepositoryRequestProducer;
        }

        public Guid GetOrganizationById(Guid organizationId)
        {
            var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetOrganizationsRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId
            };

            _getOrganizationsRequestProducer.PublishMessage(message);

            _logger.LogDebug("GetOrganizationByIdMessage Enqueued");

            return ticketId;
        }

        public Guid GetOrganizations()
        {
       
            var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetOrganizationsRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = null,
            };

            _getOrganizationsRequestProducer.PublishMessage(message);

            _logger.LogDebug("GetOrganizationsMessage Enqueued");

            return ticketId;

        }

        public Guid GetRepositoriesOfOrganization(Guid organizationId, Guid userId)
        {
            var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new GetRepositoriesRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                RepositoryId = null,
            };

            _getRepositoriesRequestProducer.PublishMessage(message);

            _logger.LogDebug("GetRepositoriesRequest Enqueued");
            
            TicketToUser[ticketId] = userId;
            
            return ticketId;
        }

        public Guid PostRepositoryToOrganization(Guid organizationId, string name, Guid userId)
        {
            var ticketId = _ticketService.CreateNewTicket(TicketResolutionType.Json);

            var message = new PostRepositoryRequest
            {
                TimeToLive = TimeSpan.FromMinutes(1),
                TicketId = ticketId,
                OrganizationId = organizationId,
                Name = name,
            };

            _postRepositoryRequestProducer.PublishMessage(message);

            _logger.LogDebug("PostRepositoryRequest Enqueued");

            TicketToUser[ticketId] = userId;
            
            return ticketId;
        }
    }
}
