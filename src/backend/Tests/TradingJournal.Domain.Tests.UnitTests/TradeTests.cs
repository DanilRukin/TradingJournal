using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.Domain.Infrastructure;
using TradingJournal.Domain.TradeAgregate;
using TradingJournal.SharedKernel;
using static TradingJournal.Domain.Errors.TradeErrors;

namespace TradingJournal.Domain.Tests.UnitTests
{
    public class TradeTests
    {
        [Fact]
        public void CanNotSetOpenDateAfterClosingDate()
        {
            DateTime open = DateTime.UtcNow;
            DateTime close = open.AddMinutes(1);
            DateTime updatedOpenDate = close.AddMinutes(1);
            Trade trade = Trade.Open("test", open);

            trade.Close(close);

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
            Trade trade = Trade.Open("test", open);
            trade.Close(close);

            trade.UpdateOpenDate(updatedOpenDate);
            trade.OpenDate.Should().Be(updatedOpenDate);
        }

        [Fact]
        public void CanSetAnyOpenDateIfNoClosingDate()
        {
            DateTime open = DateTime.UtcNow;
            DateTime minDate = DateTime.MinValue;
            DateTime maxDate = DateTime.MaxValue;
            Trade trade = Trade.Open("test", open);
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
            Trade trade = Trade.Open("test", open);

            var action = () => trade.UpdateCloseDate(close);
            action.Should().Throw<DomainException<Trade>>()
                .WithMessage(ErrorCodeConstructor.Build<Trade>(CanNotSetCloseDateForActiveTrade));
        }

        [Fact]
        public void CanNotCloseNotActiveTrade()
        {
            DateTime open = DateTime.UtcNow;
            DateTime close = open.AddDays(2);
            Trade trade = Trade.Open("test", open);
            trade.Close(close);

            var action = () => trade.Close(close);
            action.Should().Throw<DomainException<Trade>>()
                .WithMessage(ErrorCodeConstructor.Build<Trade>(Errors.TradeErrors.CanNotCloseNotActiveTrade));
        }
    }
}
