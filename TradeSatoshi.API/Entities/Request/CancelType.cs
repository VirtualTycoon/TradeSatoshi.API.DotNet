using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Request
{
    public enum CancelType
    {
        Single,
        Market,
        MarketBuys,
        MarketSells,
        AllBuys,
        AllSells,
        All
    }
}
