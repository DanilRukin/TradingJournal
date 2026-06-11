using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.Common;

/// <summary>
/// Финансовый результат сделки. Может быть положительным (прибыль),
/// отрицательным (убыток) или нулевым (безубыток).
/// </summary>
public class PnL : ValueObject
{
    public decimal Value { get; }

    public PnL(decimal value)
    {
        Value = value;
    }

    public bool IsProfit => Value > 0;
    public bool IsLoss => Value < 0;
    public bool IsBreakeven => Value == 0;

    public static PnL Zero => new(0);

    public static PnL operator +(PnL a, PnL b) => new(a.Value + b.Value);
    public static PnL operator -(PnL a, PnL b) => new(a.Value - b.Value);
    public static PnL operator -(PnL a) => new(-a.Value);

    public static implicit operator decimal(PnL pnl) => pnl.Value;
    public static explicit operator PnL(decimal value) => new(value);

    public override string ToString()
    {
        var sign = Value >= 0 ? "+" : "";
        return $"{sign}{Value:F2}";
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
