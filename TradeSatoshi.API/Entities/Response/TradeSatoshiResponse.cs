using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeSatoshi.API.Entities.Response
{
    public class TradeSatoshiResponse<T>
    {
        [JsonProperty(PropertyName="success")]
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        [JsonProperty(PropertyName="result")]
        public T Data { get; set; }
    }
}
