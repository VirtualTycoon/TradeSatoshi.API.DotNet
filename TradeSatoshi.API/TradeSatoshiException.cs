using System;
using System.Runtime.Serialization;

namespace TradeSatoshi.API
{
    [Serializable]
    public class TradeSatoshiException : Exception
    {
        public TradeSatoshiException()
        {
        }

        public TradeSatoshiException(string message) : base(message)
        {
        }

        public TradeSatoshiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TradeSatoshiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}