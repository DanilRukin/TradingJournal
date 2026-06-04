using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class ClosePriceUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public decimal Price { get; }

    public ClosePriceUpdatedDomainEvent(Guid tradeCode, decimal price)
    {
        TradeCode = tradeCode;
        Price = price;
    }
}
