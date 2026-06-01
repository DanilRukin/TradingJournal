using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class TradeOpenedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }

    public DateTime OpenDate { get; }

    public TradeOpenedDomainEvent(Guid tradeCode, DateTime openDate)
    {
        TradeCode = tradeCode;
        OpenDate = openDate;
    }
}
