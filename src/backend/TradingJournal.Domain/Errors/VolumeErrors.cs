using TradingJournal.Domain.Common;

namespace TradingJournal.Domain.Errors;

/// <summary>
/// Ошибки <see cref="Volume"/>
/// </summary>
public static class VolumeErrors
{
    public const string VolumeMustBeGreaterThanZero = "volume_must_be_greater_than_zero";
}
