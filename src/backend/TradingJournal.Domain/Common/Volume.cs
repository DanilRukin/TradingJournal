using TradingJournal.Domain.Errors;
using TradingJournal.Domain.Infrastructure;
using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.Common;

/// <summary>
/// Объем сделки
/// </summary>
public class Volume : ValueObject
{
    public int Value { get; }
    protected Volume() { }
    public Volume(int value)
    {
        if (value < 1)
            throw new DomainException<Volume>(VolumeErrors.VolumeMustBeGreaterThanZero);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator int(Volume volume) => volume.Value;
    public static explicit operator Volume(int value) => new(value);
}
