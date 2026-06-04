using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class VolumeUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public int Volume { get; }

    public VolumeUpdatedDomainEvent(Guid tradeCode, int volume)
    {
        TradeCode = tradeCode;
        Volume = volume;
    }
}
