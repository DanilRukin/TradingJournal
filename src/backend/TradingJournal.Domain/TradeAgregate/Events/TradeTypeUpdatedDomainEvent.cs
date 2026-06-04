using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events
{
    public class TradeTypeUpdatedDomainEvent : DomainEvent
    {
        public Guid TradeCode { get; }
        public string Type { get; } = default!;

        public TradeTypeUpdatedDomainEvent(Guid tradeCode, TradeType type)
        {
            TradeCode = tradeCode;
            Type = GetTypeName(type);

        }

        private string GetTypeName(TradeType type) => type switch
        {
            TradeType.Long => "long",
            TradeType.Short => "short",
            _ => "",
        };
    }
}
