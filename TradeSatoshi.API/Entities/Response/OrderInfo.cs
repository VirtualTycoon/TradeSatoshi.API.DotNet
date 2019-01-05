using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    //below is a sample response from https://tradesatoshi.com/api/private/getorder
    /*
    {  
   "success":true,
   "message":null,
   "result":{  
         "Id": "18253",
         "Market": "LTC_BTC",
         "Type": "Buy",
         "Amount": 100.00000000,
         "Rate": 0.01000000,
         "Remaining": 0.50000000,
         "Total": 1.00000000,
         "Status": "Partial",
         "Timestamp": "2015-12-07T20:04:05.3947572",
         "IsApi": true
      }
}
    */
    public class OrderInfo
    {
        public int Id { get; set; }
        public string Market { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal Remaining { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsApi { get; set; }
    }
}
