using DAPM.ClientApi.AccessControl;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetOrganizationsProcessResultConsumer : IQueueConsumer<GetOrganizationsProcessResult>
    {
        private ILogger<GetOrganizationsProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        private readonly IAccessControlService _accessControlService;
        
        public GetOrganizationsProcessResultConsumer(ILogger<GetOrganizationsProcessResultConsumer> logger, ITicketService ticketService, IAccessControlService accessControlService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _accessControlService = accessControlService;
        }

        public async Task ConsumeAsync(GetOrganizationsProcessResult message)
        {
            _logger.LogInformation("GetOrganizationsResultMessage received");


            IEnumerable<OrganizationDTO> organizationsDTOs = message.Organizations;
            var userId = _ticketService.GetUserFromTicket(message.TicketId);
            var organizations = (await _accessControlService.GetUserOrganizations(userId)).Select(o => o.Id).ToHashSet();
            organizationsDTOs = organizationsDTOs.Where(o => organizations.Contains(o.Id));

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken organizationsJSON = JToken.FromObject(organizationsDTOs, serializer);
            result["organizations"] = organizationsJSON;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);
            
            await Task.CompletedTask;
        }

    }
}
