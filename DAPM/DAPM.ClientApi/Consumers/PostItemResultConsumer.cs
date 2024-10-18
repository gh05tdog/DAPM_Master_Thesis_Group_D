using DAPM.ClientApi.Models.DTOs;
using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.Consumers
{
    public class PostItemResultConsumer : IQueueConsumer<PostItemProcessResult>
    {
        private ILogger<PostItemResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        private readonly IOrganizationService _organizationService;
        private readonly IAccessControlService _accessControlService;
        
        public PostItemResultConsumer(ILogger<PostItemResultConsumer> logger, ITicketService ticketService, IAccessControlService accessControlService, IOrganizationService organizationService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _accessControlService = accessControlService;
            _organizationService = organizationService;
        }

        public async Task ConsumeAsync(PostItemProcessResult message)
        {
            _logger.LogInformation("CreateNewItemResultMessage received");

            switch (message.ItemType)
            {
                case "Repository":
                    var user = _organizationService.GetUserFromTicket(message.TicketId);
                    await _accessControlService.AddUserToRepository(user, new RepositoryDto{ Id = message.ItemIds.RepositoryId });
                    break;
            }

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            JToken idsJSON = JToken.FromObject(message.ItemIds, serializer);

            //Serialization
            result["itemIds"] = idsJSON;
            result["itemType"] = message.ItemType;
            result["succeeded"] = message.Succeeded;
            result["message"] = message.Message;  

            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            await Task.CompletedTask;
        }
    }
}
