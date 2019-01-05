using System;

namespace CryptoExchange.API
{
    /// <summary>
    /// An interface used for the hashing method of the URI
    /// </summary>
    public interface ISignatureCreation
    {
        /// <summary>
        /// The hashing algorithm needed to create the signature for the exchange API
        /// </summary>
        /// <param name="settings">an <see cref="IApiSettings"/> instance containing security keys</param>
        /// <param name="uri">The URL of the endpoint</param>
        /// <param name="parameters">the parameters</param>
        /// <param name="nonce">The time in ticks from Jan 1, 1970</param>
        /// <returns>the generated hash</returns>
        string CreateSignature(IApiSettings settings, Uri uri, string parameters, string nonce);

    }
}