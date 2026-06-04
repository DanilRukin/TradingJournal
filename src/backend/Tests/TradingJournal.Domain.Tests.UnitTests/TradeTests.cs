using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.Domain.Common;
using TradingJournal.Domain.Infrastructure;
using TradingJournal.Domain.TradeAgregate;
using TradingJournal.SharedKernel;
using static TradingJournal.Domain.Errors.TradeErrors;

namespace TradingJournal.Domain.Tests.UnitTests;

public class TradeTests
{
    [Fact]
    public void CanNotSetOpenDateAfterClosingDate()
    {
        DateTime open = DateTime.UtcNow;
        DateTime close = open.AddMinutes(1);
        DateTime updatedOpenDate = close.AddMinutes(1);
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);

        Price closePrice = new(45);
        trade.Close(close, closePrice);

        var action = () => trade.UpdateOpenDate(updatedOpenDate);

        action.Should().Throw<DomainException<Trade>>()
            .WithMessage(ErrorCodeConstructor.Build<Trade>(OpenDateCanNotBeEarlierThanCloseDate));
    }

    [Fact]
    public void CanSetOpenDateBeforeClosingDate()
    {
        DateTime open = DateTime.UtcNow;
        DateTime close = open.AddDays(2);
        DateTime updatedOpenDate = open.AddDays(1);
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);
        Price closePrice = new(45);
        trade.Close(close, closePrice);

        trade.UpdateOpenDate(updatedOpenDate);
        trade.OpenDate.Should().Be(updatedOpenDate);
    }

    [Fact]
    public void CanSetAnyOpenDateIfNoClosingDate()
    {
        DateTime open = DateTime.UtcNow;
        DateTime minDate = DateTime.MinValue;
        DateTime maxDate = DateTime.MaxValue;
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);
        trade.CloseDate.Should().BeNull();

        trade.UpdateOpenDate(minDate);
        trade.OpenDate.Should().Be(minDate);
        trade.UpdateOpenDate(maxDate);
        trade.OpenDate.Should().Be(maxDate);
    }

    [Fact]
    public void CanNotUpdateCloseDateForActiveTrade()
    {
        DateTime open = DateTime.UtcNow;
        DateTime close = open.AddDays(2);
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);

        var action = () => trade.UpdateCloseDate(close);
        action.Should().Throw<DomainException<Trade>>()
            .WithMessage(ErrorCodeConstructor.Build<Trade>(CanNotSetCloseDateForActiveTrade));
    }

    [Fact]
    public void CanNotCloseNotActiveTrade()
    {
        DateTime open = DateTime.UtcNow;
        DateTime close = open.AddDays(2);
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);
        Price closePrice = new(45);
        trade.Close(close, closePrice);

        var action = () => trade.Close(close, closePrice);
        action.Should().Throw<DomainException<Trade>>()
            .WithMessage(ErrorCodeConstructor.Build<Trade>(Errors.TradeErrors.CanNotCloseNotActiveTrade));
    }

    [Fact]
    public void CanNotUpdatePriceInNotActiveTrade()
    {
        DateTime open = DateTime.UtcNow;
        DateTime close = open.AddDays(2);
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);
        Price closePrice = new(45);
        trade.Close(close, closePrice);

        var action = () => trade.UpdateOpenPrice((Price)555);

        action.Should().Throw<DomainException<Trade>>()
            .WithMessage(ErrorCodeConstructor.Build<Trade>(CanNotSetOpenPriceInNotActiveTrade));
    }

    [Fact]
    public void SuccessfullyUpdateOpenPriceInActiveTrade()
    {
        DateTime open = DateTime.UtcNow;
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);
        Price updatePrice = new(555);
        var action = () => trade.UpdateOpenPrice(updatePrice);

        action.Should().NotThrow();
        trade.UpdateOpenPrice(updatePrice);
        trade.OpenPrice.Should().Be(updatePrice);
    }

    [Fact]
    public void CanNotUpdateClosePriceInActiveTrade()
    {
        DateTime open = DateTime.UtcNow;
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);

        var action = () => trade.UpdateClosePrice(new(45));

        action.Should().Throw<DomainException<Trade>>()
            .WithMessage(ErrorCodeConstructor.Build<Trade>(Errors.TradeErrors.CanNotUpdateClosePriceInActiveTrade));
    }

    [Fact]
    public void SuccessfullyUpdateCloseDateAfterClosing()
    {
        DateTime open = DateTime.UtcNow;
        Price price = new(1);
        Volume volume = new(1);
        Trade trade = Trade.Open("test", volume, price, TradeType.Long, open);
        Price closePrice = new Price(45);
        DateTime closeDate = open.AddDays(1);
        trade.Close(closeDate, closePrice);
        Price updatedClosePrice = new Price(55);

        trade.ClosePrice.Should().Be(closePrice);

        var action = () => trade.UpdateClosePrice(updatedClosePrice);

        action.Should().NotThrow();

        trade.ClosePrice.Should().Be(updatedClosePrice);
    }
}
