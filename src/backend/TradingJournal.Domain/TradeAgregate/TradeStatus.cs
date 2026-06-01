namespace TradingJournal.Domain.TradeAgregate
{
    /// <summary>
    /// Статус сделки
    /// </summary>
    public enum TradeStatus
    {
        /// <summary>
        /// Активна
        /// </summary>
        Active,
        /// <summary>
        /// Закрыта
        /// </summary>
        Closed,
        /// <summary>
        /// Отменена
        /// </summary>
        Cancelled
    }
}
