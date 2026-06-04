using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class OpenPriceUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public decimal Value { get; }

    public OpenPriceUpdatedDomainEvent(Guid tradeCode, decimal value)
    {
        TradeCode = tradeCode;
        Value = value;
    }
}
