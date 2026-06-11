using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class CloseComissionUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public decimal CloseComission { get; }

    public CloseComissionUpdatedDomainEvent(Guid tradeCode, decimal closeComission)
    {
        TradeCode = tradeCode;
        CloseComission = closeComission;
    }
}
