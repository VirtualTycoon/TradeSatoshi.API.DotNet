using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    //below is an example of a response returned from URI https://tradesatoshi.com/api/public/getorderbook?market=LTC_BTC&type=both&depth=20
    /*
       {
                  "success": true,
                  "message": null,
                  "result": {
                    "buy": [
                      {
                        "quantity": 0.00000000,
                        "rate": 0.01500000
                      },
                      {
                        "quantity": 1.00000000,
                        "rate": 0.00750000
                      }
                    ],
                    "sell": [
                      {
                        "quantity": 0.00000000,
                        "rate": 0.00756150
                      },
                      {
                        "quantity": 0.00000000,
                        "rate": 0.00770000
                      },
                      {
                        "quantity": 0.00000000,
                        "rate": 0.01000000
                      }
                    ]
                  }
                }               
    */

    /// <summary>
    /// A <see cref="MarketHistoryInfo"/> instance is one returned from the URI https://tradesatoshi.com/api/public/getorderbook?market=LTC_BTC&type=both&depth=20
    /// </summary>
    public class OrderBookInfo
    {
        public IDictionary<string, BookEntry> BookEntries { get; set; }
    }

    public class BookEntry
    {
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }

        public override string ToString()
        {
            return $"Quantity: {Quantity}, Rate: {Rate}";
        }
    }
}
