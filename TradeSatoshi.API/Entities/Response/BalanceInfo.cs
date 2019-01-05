using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{

    /// <summary>
    /// A <see cref="BalanceInfo"/> instance is one returned from the  https://tradesatoshi.com/api/private/getbalance URI
    /// </summary>
    public class BalanceInfo
    {
        public string Currency { get; set; }
        [JsonProperty(PropertyName ="CurrencyLong")]
        public string CurrencyLongName { get; set; }
        public decimal Available { get; set; }
        public decimal Total { get; set; }
        public decimal HeldForTrades { get; set; }
        public decimal Unconfirmed { get; set; }
        public decimal PendingWithdraw { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Currency: {Currency}, CurrencyLongName: {CurrencyLongName}, Available: {Available}, Total: {Total}, HeldForTrades: {HeldForTrades}, Unconfirmed: {Unconfirmed}, PendingWithdrawl: {PendingWithdraw}, Address: {Address}";
        }
    }
}
