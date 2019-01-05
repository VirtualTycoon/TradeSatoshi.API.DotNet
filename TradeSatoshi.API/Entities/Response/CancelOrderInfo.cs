using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    public class CancelOrderInfo
    {
        public IList<int> CanceledOrders { get; set; }
    }
}
