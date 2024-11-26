namespace RabbitMQLibrary.Models
{
    public class PipelineExecutionDTO
    {
        public List<Guid> ExecutionIds { get; set; }
        public Guid PipelineId { get; set; }
    }
}