using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class TradeCloseDateUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public DateTime CloseDate { get; }
    public TradeCloseDateUpdatedDomainEvent(Guid tradeCode, DateTime closeDate)
    {
        TradeCode = tradeCode;
        CloseDate = closeDate;
    }
}
