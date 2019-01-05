using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    //below is an example of a response from the URI https://tradesatoshi.com/api/public/GetMarketStatus?market=LTC_BTC
    /*
     {
            "success": true,
            "message": null,
            "result": {
            "marketStatus": "OK",
            "statusMessage": null
            }
        }  
     */

    /// <summary>
    /// A <see cref="MarketStatusInfo"/> instance is one returned from the https://tradesatoshi.com/api/public/GetMarketStatus?market=LTC_BTC URI
    /// </summary>
    public class MarketStatusInfo
    {
        public string MarketStatus { get; set; }
        public string StatusMessage { get; set; }

        public override string ToString()
        {
            return $"MarketStatus: {MarketStatus}, StatusMessage: {StatusMessage}";
        }
    }
}
