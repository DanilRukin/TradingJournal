using TradingJournal.Domain.Common;
using TradingJournal.Domain.Infrastructure;
using TradingJournal.Domain.TradeAgregate.Events;
using TradingJournal.SharedKernel;
using static TradingJournal.Domain.Errors.TradeErrors;

namespace TradingJournal.Domain.TradeAgregate;

/// <summary>
/// Сделка
/// </summary>
public class Trade : EntityBase<Guid>
{
    /// <summary>
    /// Название сделки
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    /// Изменяет название сделки
    /// </summary>
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException<Trade>(TradeNameCanNotBeEmpty);
        Name = name;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new NameChangedDomainEvent(Id, Name));
    }

    /// <summary>
    /// Дата открытия сделки
    /// </summary>
    public DateTime OpenDate { get; private set; }

    /// <summary>
    /// Дата закрытия сделки
    /// </summary>
    public DateTime? CloseDate { get; private set; }

    /// <summary>
    /// Статус сделки
    /// </summary>
    public TradeStatus Status { get; private set; }

    /// <summary>
    /// Дата создания сделки
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Дата обновления сделки
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    /// <summary>
    /// Цена открытия
    /// </summary>
    public Price OpenPrice { get; private set; }

    /// <summary>
    /// Объем сделки
    /// </summary>
    public Volume Volume { get; private set; }

    /// <summary>
    /// Тип сделки
    /// </summary>
    public TradeType TradeType { get; private set; }

    /// <summary>
    /// Цена закрытия
    /// </summary>
    public Price? ClosePrice { get; private set; }

    /// <summary>
    /// Цена стоп-приказа
    /// </summary>
    public Price? StopLoss { get; private set; }

    /// <summary>
    /// Цена тейка
    /// </summary>
    public Price? TakeProfit { get; private set; }

    /// <summary>
    /// Комиссия за вход
    /// </summary>
    public Price OpenComission { get; private set; }

    /// <summary>
    /// Комиссия за выход
    /// </summary>
    public Price? CloseComission { get; private set; }

    /// <summary>
    /// Оценка сделки
    /// </summary>
    public TradeGrade? Grade { get; private set; }

    /// <summary>
    /// Тип рынка
    /// </summary>
    public MarketContext? MarketContext { get; private set; }

    /// <summary>
    /// Заметки
    /// </summary>
    public string Notes { get; private set; } = default!;

    /// <summary>
    /// Грязный финансовый результат. 
    /// Сколько заработал рынок, до вычета комиссий.
    /// Чистая математика входа и выхода. Без учёта издержек.
    /// </summary>
    public PnL? GrossPnL { get; private set; }

    /// <summary>
    /// Чистый финансовый результат. Сколько упало на счёт после всех издержек.
    /// </summary>
    public PnL? NetPnL { get; private set; }

    public float? RiskRewardRatio { get; private set; }

    /// <summary>
    /// Обновляет цену открытия
    /// </summary>
    public void UpdateOpenPrice(Price price)
    {
        if (!IsActive())
            throw new DomainException<Trade>(CanNotSetOpenPriceInNotActiveTrade);
        OpenPrice = price;
        AddDomainEvent(new OpenPriceUpdatedDomainEvent(Id, price));
        UpdatedAt = DateTime.UtcNow;
    }


    /// <summary>
    /// Обновляет дату открытия сделки
    /// </summary>
    public void UpdateOpenDate(DateTime openDate)
    {
        if (CloseDate.HasValue && CloseDate.Value < openDate)
            throw new DomainException<Trade>(OpenDateCanNotBeEarlierThanCloseDate);
        OpenDate = openDate;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new OpenDateUpdatedDomainEvent(Id, OpenDate));
    }

    /// <summary>
    /// Обновляет объем сделки
    /// </summary>
    public void UpdateVolume(Volume volume)
    {
        Volume = volume;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new VolumeUpdatedDomainEvent(Id, volume));
    }
    
    /// <summary>
    /// Обновляет тип сделки (направление)
    /// </summary>
    /// <param name="type"></param>
    public void UpdateTradeType(TradeType type)
    {
        TradeType = type;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new TradeTypeUpdatedDomainEvent(Id, type));
    }

    /// <summary>
    /// Обновляет цену закрытия
    /// </summary>
    public void UpdateClosePrice(Price price)
    {
        if (IsActive())
            throw new DomainException<Trade>(CanNotUpdateClosePriceInActiveTrade);
        ClosePrice = price;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new ClosePriceUpdatedDomainEvent(Id, price));
    }

    /// <summary>
    /// Обновляет цену стоп приказа
    /// </summary>
    public void UpdateStopLoss(Price? stop)
    {
        StopLoss = stop;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new StopLossUpdatedDomainEvent(Id, stop?.Value));
    }

    /// <summary>
    /// Обновляет центу тейка
    /// </summary>
    public void UpdateTakeProfit(Price? take)
    {
        TakeProfit = take;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new TakeProfitUpdatedDomainEvent(Id, take?.Value));
    }

    /// <summary>
    /// Обновляет комиссию входа
    /// </summary>
    public void UpdateOpenComission(Price openComission)
    {
        if (!IsActive())
            throw new DomainException<Trade>(CanNotUpdateOpenComissionInNotActiveTrade);
        UpdatedAt = DateTime.UtcNow;
        OpenComission = openComission;
        AddDomainEvent(new OpenComissionUpdatedDomainEvent(Id, openComission));
    }

    /// <summary>
    /// Обновляет комиссию за выход
    /// </summary>
    public void UpdateCloseComission(Price closeComission)
    {
        if (IsActive())
            throw new DomainException<Trade>(CanNotUpdateCloseComissionInActiveTrade);
        UpdatedAt = DateTime.UtcNow;
        CloseComission = closeComission;
        AddDomainEvent(new CloseComissionUpdatedDomainEvent(Id, closeComission));
    }

    /// <summary>
    /// Обновялет оценку сделки
    /// </summary>
    public void UpdateTradeGrade(TradeGrade? grade)
    {
        Grade = grade;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new TradeGradeUpdatedDomainEvent(Id, (int?)grade));
    }

    /// <summary>
    /// Обновляет тип рынка
    /// </summary>
    public void UpdateMarketContext(MarketContext? context)
    {
        MarketContext = context;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new MarketContextUpdatedDomainEvent(Id, context));
    }

    /// <summary>
    /// Обновляет заметки
    /// </summary>
    public void UpdateNotes(string notes)
    {
        Notes = notes ?? string.Empty;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new NotesUpdatedDomainEvent(Id, Notes));
    }

    protected Trade() { }

    public static Trade Open(string name, Volume volume, Price openPrice, Price openComission, TradeType type, DateTime? openDate = null,
        Price? stopLoss = null, Price? takeProfit = null, MarketContext? marketContext = null)
    {
        Trade trade = new();
        trade.Id = Guid.NewGuid();
        trade.SetName(name);
        DateTime tradeOpenDate = openDate ?? DateTime.UtcNow;
        trade.UpdateOpenDate(tradeOpenDate);
        trade.Status = TradeStatus.Active;
        trade.OpenPrice = openPrice;
        trade.UpdateVolume(volume);
        trade.UpdateTradeType(type);
        trade.UpdateStopLoss(stopLoss);
        trade.UpdateTakeProfit(takeProfit);
        trade.UpdateOpenComission(openComission);
        trade.UpdateMarketContext(marketContext);
        DateTime now = DateTime.UtcNow;
        trade.CreatedAt = now;
        trade.UpdatedAt = now;
        return trade;
    }

    /// <summary>
    /// Закрывает сделку
    /// </summary>
    public void Close(DateTime closeDate, Price closePrice, Price closeComission, Price? stop = null,
        Price? take = null, TradeGrade? grade = null)
    {
        if (!IsActive())
            throw new DomainException<Trade>(CanNotCloseNotActiveTrade);
        SetCloseDate(closeDate);
        Status = TradeStatus.Closed;
        UpdateClosePrice(closePrice);
        UpdateCloseComission(closeComission);
        UpdateStopLoss(stop);
        UpdateTakeProfit(take);
        UpdateTradeGrade(grade);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Обновляет дату закрытия сделки
    /// </summary>
    public void UpdateCloseDate(DateTime closeDate)
    {
        if (IsActive())
            throw new DomainException<Trade>(CanNotSetCloseDateForActiveTrade);
        SetCloseDate(closeDate);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Устанавливает дату закрытия сделки
    /// </summary>
    private void SetCloseDate(DateTime closeDate)
    {
        if (closeDate < OpenDate)
            throw new DomainException<Trade>(CloseDateMustBeAfterOpenDate);
        CloseDate = closeDate;
        AddDomainEvent(new TradeCloseDateUpdatedDomainEvent(Id, CloseDate.Value));
    }

    /// <summary>
    /// Указывает, является ли сделка активной
    /// </summary>
    private bool IsActive() => Status == TradeStatus.Active;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lotMultiplier">Мультипликатор лота (для акций = 1 или 10)</param>
    /// <param name="tickSize">Шаг цены (только для фьючерсов)</param>
    /// <param name="tickCost">Стоимость шага цены (только для фьючерсов)</param>
    /// <param name="marketType">Тип рынка (Stock, Derivatives, Crypto)</param>
    private void CalculatePnL(int lotMultiplier, decimal? tickSize, decimal? tickCost, MarketType marketType)
    {
        // Сделка должна быть закрыта и иметь цену выхода
        if (ClosePrice is null)
        {
            GrossPnL = null;
            NetPnL = null;
            return;
        }
        decimal priceDifference = CalculatePriceDiffierence();      
        decimal grossPnlValue = CalculateGrossPnl(marketType, tickSize, tickCost, priceDifference, lotMultiplier);
        decimal totalComission = OpenComission.Value + (CloseComission?.Value ?? 0m);
        GrossPnL = new PnL(grossPnlValue);
        NetPnL = new PnL(grossPnlValue - totalComission);

    }

    /// <summary>
    /// Расчёт GrossPnL в зависимости от типа рынка
    /// </summary>
    private decimal CalculateGrossPnl(MarketType marketType, decimal? tickSize, decimal? tickCost,
        decimal priceDifference, int lotMultiplier)
    {
        decimal grossPnlValue;
        if (marketType == MarketType.Derivatives 
            && tickSize.HasValue && tickSize.Value > 0 
            && tickCost.HasValue && tickCost.Value > 0)
        {
            // Фьючерсы: переводим пункты в тики, умножаем на стоимость тика и объём
            // Пример: SiU4, шаг = 1, стоимость шага = 10 руб
            // Цена прошла 240 пунктов = 240 тиков × 10 руб × 3 контракта = 7200 руб
            var ticks = priceDifference / tickSize.Value;
            grossPnlValue = ticks * tickCost.Value * Volume.Value;
        }
        else
        {
            // Акции / Спотовая крипта:
            // Разница в цене × мультипликатор лота × объём
            // Пример: SBER, разница 5 руб × 1 × 100 акций = 500 руб
            grossPnlValue = priceDifference * lotMultiplier * Volume.Value;
        }
        return grossPnlValue;
    }

    /// <summary>
    /// Вычисляет разницу цен в пунктах
    /// </summary>
    private decimal CalculatePriceDiffierence()
    {
        // Разница цен в пунктах (направление зависит от типа сделки)
        decimal priceDifference = 0m;

        if (TradeType == TradeType.Long)
        {
            // Long: купили дёшево, продали дорого → прибыль при росте цены
            priceDifference = ClosePrice!.Value - OpenPrice.Value;
        }
        else
        {
            // Short: продали дорого, откупили дёшево → прибыль при падении цены
            priceDifference = OpenPrice.Value - ClosePrice!.Value;
        }
        return priceDifference;
    }

    /// <summary>
    /// Считает соотношение риска к прибыли
    /// </summary>
    /// <returns></returns>
    private void CalculateRiskRewardRatio()
    {
        // Risk/Reward Ratio (реализованный)
        // RRR = |Цена выхода − Цена входа| / |Цена входа − Стоп-лосс|
        if (ClosePrice is null)
        {
            RiskRewardRatio = null;
            return;
        }
        if (StopLoss is not  null)
        {
            var risk = Math.Abs(OpenPrice.Value - StopLoss.Value);
            if (risk > 0)
            {
                var reward = Math.Abs(ClosePrice.Value - OpenPrice.Value);
                RiskRewardRatio = decimal.ToSingle(reward / risk);
            }
            else
            {
                // Стоп-лосс равен цене входа (риск = 0), RRR не определён
                RiskRewardRatio = null;
            }
        }
        else
        {
            // Стоп-лосс не установлен, RRR не определён
            RiskRewardRatio = null;
        }
    }

    private void CalculatePnlAndRiskReward(int lotMultiplier, decimal? tickSize, decimal? tickCost, MarketType marketType)
    {
        CalculatePnL(lotMultiplier, tickSize, tickCost, marketType);
        CalculateRiskRewardRatio();
    }
}

