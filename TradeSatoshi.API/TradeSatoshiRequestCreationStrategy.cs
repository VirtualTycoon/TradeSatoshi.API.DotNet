using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CryptoExchange.API
{
    public class TradeSatoshiRequestCreationStrategy : IRequestCreation
    {
        public TradeSatoshiRequestCreationStrategy()
        {
        }

        /// <summary>
        /// Implements the HMAC SHA512 hashing method
        /// </summary>
        /// <param name="settings">an <see cref="IApiSettings"/> instance containing security keys</param>
        /// <param name="uri">The URL of the endpoint</param>
        /// <param name="queryString">the query string parmeters of the URI</param>
        /// <param name="nonce">The time in ticks from Jan 1, 1970</param>
        /// <returns>the HMAC SHA512 generated hash</returns>
        public virtual string CreateSignature(IApiSettings settings, Uri uri, string parameters, string nonce)
        {
            //SIGNATURE: API_KEY + "POST" + URI + NONCE + POST_PARAMS(signed by secret key according to HMAC - SHA512 method.)
            string endpoint = WebUtility.UrlEncode(uri.ToString()).ToLower();
            parameters = Convert.ToBase64String(Encoding.UTF8.GetBytes(parameters ?? ""));
            var signature = $"{settings.PublicKey}POST{endpoint}{nonce}{parameters}";
            using (var hashAlgo = new HMACSHA512(Convert.FromBase64String(settings.PrivateKey)))
            {
                var signedBytes = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(signature));
                return Convert.ToBase64String(signedBytes);
            }

            //return GetSignature(settings.PrivateKey, settings.PublicKey, uri.ToString(), nonce, parameters);
        }

        private static string GetSignature(string privatekey, string publickey, string uri, string nonce, string post_params = null)
        {
            uri = WebUtility.UrlEncode(uri).ToLower();
            string signature = "";
            if (post_params != null)
            {
                post_params = Convert.ToBase64String(Encoding.UTF8.GetBytes(post_params));
                signature = publickey + "POST" + uri + nonce + post_params;
            }
            else
            {
                var bytes = Encoding.UTF8.GetBytes("");
                signature = publickey + "POST" + uri + nonce + Convert.ToBase64String(bytes);
            }
            byte[] messageBytes = Encoding.UTF8.GetBytes(signature);
            using (HMACSHA512 _object = new HMACSHA512(Convert.FromBase64String(privatekey)))
            {
                byte[] hashmessage = _object.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        /// <summary>
        /// Adds the signature to the HttpContent and adds an Authorization header.
        /// </summary>
        /// <param name="settings">an <see cref="IApiSettings"/> instance containing security keys</param>
        /// <param name="signature">The computed hash from the ComputeHash method</param>
        /// <param name="content">A new empty <see cref="StringContent"/> with an Authentication Header.</param>
        public HttpContent CreateHttpContent(IApiSettings settings, string jsonParams, string nonce)
        {
            return new StringContent(jsonParams ?? "", Encoding.UTF8, "application/json"); 
        }

        public HttpRequestMessage CreateHttpRequestMessage(Uri uri, IApiSettings settings, string signature, HttpContent content, string nonce)
        {
            var header = $"Basic {settings.PublicKey}:{signature}:{nonce}";
            var message = new HttpRequestMessage(HttpMethod.Post, uri);
            message.Headers.Add("Authorization", header);
            message.Content = content;
            return message;
        }
    }
}
