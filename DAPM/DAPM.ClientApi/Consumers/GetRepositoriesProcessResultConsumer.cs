using DAPM.ClientApi.AccessControl;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models;
using RabbitMQLibrary.Messages.Orchestrator.ServiceResults;
using RabbitMQLibrary.Messages.ClientApi;

namespace DAPM.ClientApi.Consumers
{
    public class GetRepositoriesProcessResultConsumer : IQueueConsumer<GetRepositoriesProcessResult>
    {
        private ILogger<GetRepositoriesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        private readonly IAccessControlService _accessControlService;
        public GetRepositoriesProcessResultConsumer(ILogger<GetRepositoriesProcessResultConsumer> logger, ITicketService ticketService, IAccessControlService accessControlService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _accessControlService = accessControlService;
        }

        public async Task ConsumeAsync(GetRepositoriesProcessResult message)
        {
            _logger.LogInformation("GetRepositoriesProcessResult received");

            IEnumerable<RepositoryDTO> repositoriesDTOs = message.Repositories;
            var userId = _ticketService.GetUserFromTicket(message.TicketId);
            var repositories = (await _accessControlService.GetUserRepositories(userId)).Select(r => r.Id).ToHashSet();
            repositoriesDTOs = repositoriesDTOs.Where(r => repositories.Contains(r.Id));
            
            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken repositoriesJSON = JToken.FromObject(repositoriesDTOs, serializer);
            result["repositories"] = repositoriesJSON;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);
            
            await Task.CompletedTask;
        }
    }
}
