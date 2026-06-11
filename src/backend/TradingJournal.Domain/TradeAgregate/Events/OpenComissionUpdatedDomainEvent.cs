using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class OpenComissionUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public decimal OpenComission { get; }

    public OpenComissionUpdatedDomainEvent(Guid tradeCode, decimal openComission)
    {
        TradeCode = tradeCode;
        OpenComission = openComission;
    }
}
