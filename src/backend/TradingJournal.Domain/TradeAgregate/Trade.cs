using System.Data;
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

    protected Trade() { }

    public static Trade Open(string name, Volume volume, Price openPrice, TradeType type, DateTime? openDate = null,
        Price? stopLoss = null, Price? takeProfit = null)
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
        DateTime now = DateTime.UtcNow;
        trade.CreatedAt = now;
        trade.UpdatedAt = now;
        return trade;
    }

    /// <summary>
    /// Закрывает сделку
    /// </summary>
    public void Close(DateTime closeDate, Price closePrice, Price? stop = null, Price? take = null)
    {
        if (!IsActive())
            throw new DomainException<Trade>(CanNotCloseNotActiveTrade);
        SetCloseDate(closeDate);
        Status = TradeStatus.Closed;
        UpdateClosePrice(closePrice);
        UpdateStopLoss(stop);
        UpdateTakeProfit(take);
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
}

