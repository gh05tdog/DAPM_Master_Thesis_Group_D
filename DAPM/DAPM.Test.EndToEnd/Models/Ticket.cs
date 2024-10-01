namespace DAPM.Test.EndToEnd.Models;

public record Ticket<T>(Guid TicketId, TicketStatus Status, string Message, T Result);