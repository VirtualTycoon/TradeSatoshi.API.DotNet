using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    public class SubmitOrderInfo
    {
        public int OrderId { get; set; }
        public IList<int> Filled { get; set; }
    }
}
