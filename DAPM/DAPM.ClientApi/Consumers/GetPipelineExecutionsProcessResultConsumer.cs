using DAPM.ClientApi.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Messages.ClientApi;
using RabbitMQLibrary.Models;

namespace DAPM.ClientApi.Consumers
{
    public class GetPipelineExecutionsProcessResultConsumer : IQueueConsumer<GetPipelineExecutionsRequestResult>
    {
        private ILogger<GetPipelineExecutionsProcessResultConsumer> _logger;
        private readonly ITicketService _ticketService;
        public GetPipelineExecutionsProcessResultConsumer(ILogger<GetPipelineExecutionsProcessResultConsumer> logger, ITicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public Task ConsumeAsync(GetPipelineExecutionsRequestResult message)
        {
            _logger.LogInformation("GetPipelineExecutionsRequestResult received");


            var executions = message.Executions;

            // Objects used for serialization
            JToken result = new JObject();
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });


            //Serialization
            JToken executionsJson = JToken.FromObject(executions, serializer);
            result["executions"] = executionsJson;

            _logger.LogInformation($"Pipeline executions: {result}");
            // Update resolution

            _ticketService.UpdateTicketResolution(message.TicketId, result);

            return Task.CompletedTask;
        }
    }
}
