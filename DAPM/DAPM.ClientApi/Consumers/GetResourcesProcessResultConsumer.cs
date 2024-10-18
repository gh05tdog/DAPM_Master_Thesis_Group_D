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
    public class GetResourcesProcessResultConsumer : IQueueConsumer<GetResourcesProcessResult>
    {
        private ILogger<GetResourcesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        private readonly IAccessControlService _accessControlService;
        
        public GetResourcesProcessResultConsumer(ILogger<GetResourcesProcessResultConsumer> logger, ITicketService ticketService, IAccessControlService accessControlService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _accessControlService = accessControlService;
        }

        public async Task ConsumeAsync(GetResourcesProcessResult message)
        {
            _logger.LogInformation("GetResourcesProcessResult received");


            IEnumerable<ResourceDTO> resourcesDTOs = message.Resources;
            var userId = _ticketService.GetUserFromTicket(message.TicketId);
            var resources = (await _accessControlService.GetUserResources(userId)).Select(r => r.Id).ToHashSet();
            resourcesDTOs = resourcesDTOs.Where(r => resources.Contains(r.Id));
            
            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken resourcesJSON = JToken.FromObject(resourcesDTOs, serializer);
            result["resources"] = resourcesJSON;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            await Task.CompletedTask;
        }
    }
}
