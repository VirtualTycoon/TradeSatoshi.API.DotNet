using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    //below is an example of json returned from https://tradesatoshi.com/api/public/getcurrencies
    /*
    {
        "success": true,
        "message": null,
        "result": [
        {
            "currency": "BOLI",
            "currencyLong": "Bolivarcoin",
            "minConfirmation": 3,
            "txFee": 0.00000000,
            "status": "OK"
        },
        {
            "currency": "BTC",
            "currencyLong": "Bitcoin",
            "minConfirmation": 6,
            "txFee": 0.00000000,
            "status": "OK"
        }
        ]
    }
    */
    /// <summary>
    /// A <see cref="CurrencyInfo"/> instance is one returned from the https://tradesatoshi.com/api/public/getcurrencies URI
    /// </summary>
    public class CurrencyInfo
    {
        public string Currency { get; set; }
        [JsonProperty(PropertyName = "currencyLong")]
        public string CurrencyLongName { get; set; }
        public int MinConfirmation { get; set; }
        [JsonProperty(PropertyName = "txFee")]
        public decimal TransactionFee { get; set; }
        public string Status { get; set; }
        public override string ToString()
        {
            return $"{Currency} : {CurrencyLongName} : {MinConfirmation} : {Status} : {TransactionFee}";
        }
    }
}
