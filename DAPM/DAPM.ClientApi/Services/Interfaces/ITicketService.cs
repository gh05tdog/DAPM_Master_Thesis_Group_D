using Newtonsoft.Json.Linq;
using RabbitMQLibrary.Models.AccessControl;

namespace DAPM.ClientApi.Services.Interfaces
{
    public interface ITicketService
    {
        public TicketStatus GetTicketStatus(Guid ticketId);
        public JToken GetTicketResolution(Guid ticketId);
        public TicketResolutionType GetTicketResolutionType(Guid ticketId);
        public Guid CreateNewTicket(TicketResolutionType resolutionType);
        public void UpdateTicketResolution(Guid ticketId, JToken resolution);
        UserDto GetUserFromTicket(Guid ticketId);
        void AddUserToTicket(Guid ticketId, Guid userId);
    }
}
