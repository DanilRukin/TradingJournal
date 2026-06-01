using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class OpenDateUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public DateTime OpenDate { get; }
    public OpenDateUpdatedDomainEvent(Guid tradeCode,  DateTime openDate)
    {
        TradeCode = tradeCode;
        OpenDate = openDate;
    }
}
