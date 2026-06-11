namespace TradingJournal.Domain.Common;

/// <summary>
/// Вид рынка
/// </summary>
public enum MarketType
{
    /// <summary>
    /// Фондовый
    /// </summary>
    Stock,

    /// <summary>
    /// Срочный
    /// </summary>
    Derivatives,

    /// <summary>
    /// Крипта
    /// </summary>
    Crypto
}
