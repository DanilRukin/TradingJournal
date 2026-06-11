using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.TradeAgregate.Events;

public class TradeGradeUpdatedDomainEvent : DomainEvent
{
    public Guid TradeCode { get; }
    public int? Grade { get; }

    public TradeGradeUpdatedDomainEvent(Guid tradeCode, int? grade)
    {
        TradeCode = tradeCode;
        Grade = grade;
    }
}
