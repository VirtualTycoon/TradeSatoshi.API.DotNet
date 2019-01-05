using System.Net.Http;

namespace CryptoExchange.API
{
    public interface IHttpContentCreation
    {
        /// <summary>
        /// The HttpContent and any headers needed by the exchange API
        /// </summary>
        /// <param name="settings">an <see cref="IApiSettings"/> instance containing security keys</param>
        /// <param name="signature">The hashed signature from the CreateSignature method</param>
        /// <param name="nonce">The time in ticks from Jan 1, 1970</param>
        /// <returns>The HttpContent and headers to return to the ApiClient.  Return null if not needed.</returns>
        HttpContent CreateHttpContent(IApiSettings settings, string signature, string nonce);
        //TODO: test if a null return will fail
    }
}