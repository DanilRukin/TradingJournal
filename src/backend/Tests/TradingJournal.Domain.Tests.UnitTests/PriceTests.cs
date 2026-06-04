using FluentAssertions;
using TradingJournal.Domain.Common;
using TradingJournal.Domain.Errors;
using TradingJournal.Domain.Infrastructure;
using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.Tests.UnitTests;

public class PriceTests
{
    [Fact]
    public void PriceCanNotBeNegative()
    {
        var action = () => { Price price = new(-1); };
        action.Should().Throw<DomainException<Price>>()
            .WithMessage(ErrorCodeConstructor.Build<Price>(PriceErrors.PriceCanNotBeNegative));
    }

    [Fact]
    public void PriceCanBeZero()
    {
        Price price;
        var action = () => { price = new(0); };
        action.Should().NotThrow();
        price = new(0);
        price.Value.Should().Be(0);
    }
}
