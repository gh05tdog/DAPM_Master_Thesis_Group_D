using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RabbitMQLibrary.Messages.ClientApi
{
    public class GetPipelineExecutionsRequestResult : IQueueMessage
    {
        public Guid MessageId { get; set; }
        public Guid TicketId { get; set; }
        public TimeSpan TimeToLive { get; set; }
        public IEnumerable<PipelineExecutionDTO> Executions { get; set; }
    }
}
