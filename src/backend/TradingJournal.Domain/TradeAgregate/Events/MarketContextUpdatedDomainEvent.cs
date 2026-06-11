using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class MarketContextUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public string MarketContext { get; }

    public MarketContextUpdatedDomainEvent(Guid tradeCode, MarketContext? context)
    {
        TradeCode = tradeCode;
        MarketContext = GetContextName(context);
    }

    private string GetContextName(MarketContext? context) =>
        context switch
        {
            TradeAgregate.MarketContext.Trend => "Trend",
            TradeAgregate.MarketContext.Range => "Range",
            TradeAgregate.MarketContext.Quiet => "Quiet",
            _ => ""
        };
}
