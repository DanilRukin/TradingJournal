using FluentAssertions;
using TradingJournal.Domain.Common;
using TradingJournal.Domain.Errors;
using TradingJournal.Domain.Infrastructure;
using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.Tests.UnitTests;

public class VolumeTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void VolumeMustBeGreaterThanZero(int value)
    {
        var action = () => { Volume volume = new(value); };
        action.Should().Throw<DomainException<Volume>>()
            .WithMessage(ErrorCodeConstructor.Build<Volume>(VolumeErrors.VolumeMustBeGreaterThanZero));
    }
}
