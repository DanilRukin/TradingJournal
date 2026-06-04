using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class StopLossUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public decimal? StopPrice { get; }

    public StopLossUpdatedDomainEvent(Guid tradeCode, decimal? stopPrice)
    {
        TradeCode = tradeCode;
        StopPrice = stopPrice;
    }
}
