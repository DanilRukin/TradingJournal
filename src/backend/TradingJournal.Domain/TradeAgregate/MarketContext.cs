namespace TradingJournal.Domain.TradeAgregate;

/// <summary>
/// Тип рынка
/// </summary>
public enum MarketContext
{
    /// <summary>
    /// Тренд
    /// </summary>
    Trend,

    /// <summary>
    /// Боковик
    /// </summary>
    Range,

    /// <summary>
    /// Сильная волатильность (новости)
    /// </summary>
    Volitile,

    /// <summary>
    /// Слабая активность (предпраздничный день)
    /// </summary>
    Quiet
}
