using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    //below is an example of the response returned from URI https://tradesatoshi.com/api/public/getmarketsummary?market=LTC_BTC
    /*
                {
                    "success":true,
                    "message":null,
                    "result":{
                    "market":"LTC_BTC",
                    "high":0.01000000,
                    "low":0.01000000,
                    "volume":0.00784797,
                    "baseVolume":0.00007848,
                    "last":0.01000000,
                    "bid":0.01500000,
                    "ask":100.00000000,
                    "openBuyOrders":2,
                    "openSellOrders":7
                    }
                }
     */
    /// <summary>
    /// A <see cref="MarketSummaryInfo"/> instance is one returned from the https://tradesatoshi.com/api/public/getmarketsummary?market=LTC_BTC
    /// </summary>
    public class MarketSummaryInfo
    {
        public string Market { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public decimal BaseVolume { get; set; }
        public decimal Last { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public uint OpenBuyOrders { get; set; }
        public uint OpenSellOrders { get; set; }

        public override string ToString()
        {
            return $"Market: {Market}, High: {High}, Low: {Low}, Volume: {Volume}, BaseVolume: {BaseVolume}, Last: {Last}, Bid: {Bid}, Ask: {Ask}, OpenBuys: {OpenBuyOrders}, OpenSells: {OpenSellOrders}";
        }
    }
}
