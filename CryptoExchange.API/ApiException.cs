using System;
using System.Runtime.Serialization;

namespace CryptoExchange.API
{
    /// <summary>
    /// Exception is used to return data to the client.
    /// It hides details about the server
    /// </summary>
    //[Serializable]
    //public class ApiException : Exception
    //{
    //    private string deserializeErrorMessage;
    //    private ApiError apiError;

    //    public ApiException()
    //    {
    //    }

    //    public ApiException(string message) : base(message)
    //    {
    //    }

    //    public ApiException(string deserializeErrorMessage, ApiError apiError) 
    //    {
    //        this.deserializeErrorMessage = deserializeErrorMessage;
    //        this.apiError = apiError;
    //    }

    //    public ApiException(string message, Exception innerException) : base(message, innerException)
    //    {
    //    }

    //    protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context)
    //    {
    //    }
    //}
}