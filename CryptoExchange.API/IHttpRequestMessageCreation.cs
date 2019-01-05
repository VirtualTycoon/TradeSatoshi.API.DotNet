using System;
using System.Net.Http;

namespace CryptoExchange.API
{
    public interface IHttpRequestMessageCreation
    {
        // <summary>
        /// Creates the HttpRequestMessage and any headers needed by the exchange API
        /// </summary>
        /// <param name="settings">an <see cref="IApiSettings"/> instance containing security keys</param>
        /// <param name="signature">The hashed signature from the CreateSignature method</param>
        /// <param name="nonce">The time in ticks from Jan 1, 1970</param>
        /// <returns>The HttpRequestMessage and headers to return to the ApiClient.  Return null if not needed.</returns>
        HttpRequestMessage CreateHttpRequestMessage(Uri uri, IApiSettings settings, string signature, HttpContent content, string nonce);
        //TODO:  Test wheter a null return value will fail
    }
}