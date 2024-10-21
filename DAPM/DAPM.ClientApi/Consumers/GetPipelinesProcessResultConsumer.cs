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
    public class GetPipelinesProcessResultConsumer : IQueueConsumer<GetPipelinesProcessResult>
    {
        private ILogger<GetPipelinesProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        private readonly IAccessControlService _accessControlService;
        
        public GetPipelinesProcessResultConsumer(ILogger<GetPipelinesProcessResultConsumer> logger, ITicketService ticketService, IAccessControlService accessControlService)
        {
            _logger = logger;
            _ticketService = ticketService;
            _accessControlService = accessControlService;
        }

        public async Task ConsumeAsync(GetPipelinesProcessResult message)
        {
            _logger.LogInformation("GetPipelinesProcessResult received");

            IEnumerable<PipelineDTO> pipelinesDTOs = message.Pipelines;
            var userId = _ticketService.GetUserFromTicket(message.TicketId);
            var pipelines = (await _accessControlService.GetUserPipelines(userId)).Select(r => r.Id).ToHashSet();
            pipelinesDTOs = pipelinesDTOs.Where(p => pipelines.Contains(p.Id));
                
            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken pipelinesJSON = JToken.FromObject(pipelinesDTOs, serializer);
            result["pipelines"] = pipelinesJSON;


            // Update resolution
            _ticketService.UpdateTicketResolution(message.TicketId, result);

            await Task.CompletedTask;
        }
    }
}
