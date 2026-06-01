using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

/// <summary>
/// Событие изменения название сделки
/// </summary>
public class NameChangedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; private set; }
    public string Name { get; private set; }

    public NameChangedDomainEvent(Guid tradeCode, string name)
    {
        TradeCode = tradeCode;
        Name = name;
    }
}
