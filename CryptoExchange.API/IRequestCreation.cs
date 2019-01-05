using System.Net.Http;

namespace CryptoExchange.API
{
    public interface IRequestCreation : ISignatureCreation, IHttpContentCreation, IHttpRequestMessageCreation
    {
    }
}