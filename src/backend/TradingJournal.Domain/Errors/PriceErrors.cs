using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingJournal.Domain.Common;

namespace TradingJournal.Domain.Errors;

/// <summary>
/// Коды ошибок <see cref="Price"/>
/// </summary>
public static class PriceErrors
{
    public const string PriceCanNotBeNegative = "price_can_not_be_negative";
}
