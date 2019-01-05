using CryptoExchange.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API
{
    public class TradeSatoshiSettings : IApiSettings
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
