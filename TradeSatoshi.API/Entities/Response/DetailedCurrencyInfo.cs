using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    //below is an example of data returned from the URI https://tradesatoshi.com/api/public/GetCurrency?Symbol=BTC
    /*
        {
            "success": true,
            "message": null,
            "result": {
            "currency": "BTC",
            "currencyLong": "Bitcoin",
            "minConfirmation": 6,
            "txFee": 0.00200000,
            "status": "OK",
            "statusMessage": "",
            "minBaseTrade": 0.00010000,
            "isTipEnabled": true,
            "minTip": 0.00000100,
            "maxTip": 0.10000000
            }
         }
    */
    /// <summary>
    /// A <see cref="DetailedCurrencyInfo"/> instance is one returned from the https://tradesatoshi.com/api/public/GetCurrency?Symbol=BTC URI
    /// </summary>
    public class DetailedCurrencyInfo : CurrencyInfo
    {
        public string StatusMessage { get; set; }
        public decimal MinBaseTrade { get; set; }
        public bool IsTipEnabled { get; set; }
        public decimal MinTip { get; set; }
        public decimal MaxTip { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" : {StatusMessage} : {MinBaseTrade} : {IsTipEnabled} : {MinTip} : {MaxTip}";
        }
    }
}
