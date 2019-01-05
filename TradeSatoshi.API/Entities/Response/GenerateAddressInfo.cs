using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    public class GenerateAddressInfo
    {
        public string Currency { get; set; }
        public string Address { get; set; }
        public string PaymentId { get; set; }

        public override string ToString()
        {
            return $"Currency: {Currency}, Address: {Address}, PaymentId: {PaymentId}";
        }
    }
}
