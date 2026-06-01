using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.Infrastructure;

public class DomainException<T> : Exception
{
    public DomainException(string errorCode) : base(ErrorCodeConstructor.Build<T>(errorCode)) { }
}
