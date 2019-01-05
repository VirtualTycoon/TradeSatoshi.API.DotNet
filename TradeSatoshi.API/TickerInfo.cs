using Newtonsoft.Json;

namespace TradeSatoshi.API
{
    //below is an example of a response from the URI https://tradesatoshi.com/api/public/getticker?market=LTC_BTC
    /*
    {
        "success":true,
        "message":null,
        "result":{
        "bid":0.01500000,
        "ask":100.00000000,
        "last":0.01000000,
        "market":"LTC_BTC"
        }
    }
    */
    /// <summary>
    /// A <see cref="TickerInfo"/> instance is one returned from the https://tradesatoshi.com/api/public/getticker?market=LTC_BTC URI
    /// </summary>
    public class TickerInfo
    {
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public decimal Last { get; set; }
        public string Market { get; set; }

        public override string ToString()
        {
            return $"Market: {Market}, Bid: {Bid}, Ask: {Ask}, Last: {Last}";
        }
    }
}
 