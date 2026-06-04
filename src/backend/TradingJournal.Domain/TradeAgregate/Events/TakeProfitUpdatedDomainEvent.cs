using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class TakeProfitUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public decimal? TakePrice { get; }

    public TakeProfitUpdatedDomainEvent(Guid tradeCode, decimal? takePrice)
    {
        TradeCode = tradeCode;
        TakePrice = takePrice;
    }
}
