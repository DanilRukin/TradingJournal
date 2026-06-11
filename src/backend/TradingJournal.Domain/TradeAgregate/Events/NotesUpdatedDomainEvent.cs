using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class NotesUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public string Notes { get; } = default!;

    public NotesUpdatedDomainEvent(Guid tradeCode, string notes)
    {
        TradeCode = tradeCode;
        Notes = notes;
    }
}
