using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    //below is an example of a response returned from https://tradesatoshi.com/api/public/getmarkethistory?market=LTC_BTC&count=20
    /*
                     {
                  "success": true,
                  "message": null,
                  "result": [
                    {
                      "id": 512,
                      "timeStamp": "2016-04-28T01:34:02.397",
                      "quantity": 0.00784797,
                      "price": 0.01000000,
                      "orderType": "Buy",
                      "total": 0.00007848
                    },
                    {
                      "id": 503,
                      "timeStamp": "2016-04-23T08:16:38.087",
                      "quantity": 0.00134797,
                      "price": 0.08555000,
                      "orderType": "Buy",
                      "total": 0.00011532
                    },
                    {
                      "id": 502,
                      "timeStamp": "2016-04-23T08:16:34.91",
                      "quantity": 0.00650000,
                      "price": 0.07900000,
                      "orderType": "Buy",
                      "total": 0.00051350
                    }
                  ]
                }   
     */
    /// <summary>
    /// A <see cref="MarketHistoryInfo"/> instance is one returned from the https://tradesatoshi.com/api/public/getmarkethistory?market=LTC_BTC&count=20 URI
    /// </summary>
    public class MarketHistoryInfo
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string OrderType { get; set; }
        public decimal Total { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, TimeStamp: {TimeStamp}, Quantity: {Quantity}, Price: {Price}, OrderType: {OrderType}, Total: {Total}";
        }
    }
}
