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
    /// Обновляет дату открытия сделки
    /// </summary>
    public void UpdateOpenDate(DateTime openDate)
    {
        if (CloseDate.HasValue && CloseDate.Value < openDate)
            throw new DomainException<Trade>(OpenDateCanNotBeEarlierThanCloseDate);
        OpenDate = openDate;
        AddDomainEvent(new OpenDateUpdatedDomainEvent(Id, OpenDate));
    }

    protected Trade() { }

    public static Trade Open(string name, DateTime? openDate = null)
    {
        Trade trade = new();
        trade.Id = Guid.NewGuid();
        trade.SetName(name);
        DateTime tradeOpenDate = openDate ?? DateTime.UtcNow;
        trade.UpdateOpenDate(tradeOpenDate);
        trade.Status = TradeStatus.Active;
        return trade;
    }

    /// <summary>
    /// Закрывает сделку
    /// </summary>
    public void Close(DateTime closeDate)
    {
        if (!IsActive())
            throw new DomainException<Trade>(CanNotCloseNotActiveTrade);
        SetCloseDate(closeDate);
        Status = TradeStatus.Closed;
    }

    /// <summary>
    /// Обновляет дату закрытия сделки
    /// </summary>
    public void UpdateCloseDate(DateTime closeDate)
    {
        if (IsActive())
            throw new DomainException<Trade>(CanNotSetCloseDateForActiveTrade);
        SetCloseDate(closeDate);
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

