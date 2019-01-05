using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoExchange.API
{
    public class ApiSettings : IApiSettings
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
