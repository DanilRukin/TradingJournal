namespace TradingJournal.Domain.Errors
{
    /// <summary>
    /// Ошибки агрегата <see cref="TradeAgregate.Trade"/>
    /// </summary>
    public static class TradeErrors
    {
        public const string TradeNameCanNotBeEmpty = "trade_name_can_not_be_empty";
        public const string OpenDateCanNotBeEarlierThanCloseDate = "open_date_can_not_be_earlier_than_close_date";
        public const string CanNotSetCloseDateForActiveTrade = "can_not_set_close_date_for_active_trade";
        public const string CloseDateMustBeAfterOpenDate = "close_date_must_be_after_open_date";
        public const string CanNotCloseNotActiveTrade = "can_not_close_not_active_trade";
    }
}
