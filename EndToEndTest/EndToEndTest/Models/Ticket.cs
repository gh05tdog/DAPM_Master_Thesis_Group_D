namespace EndToEndTest.Models;

public record Ticket<T>(Guid TicketId, TicketStatus Status, string Message, T Result);