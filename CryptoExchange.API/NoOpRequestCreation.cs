using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CryptoExchange.API
{
    //TODO:  test if when we return null values, this will break
    public class NoOpRequestCreation : ISignatureCreation, IHttpContentCreation, IHttpRequestMessageCreation
    {
        public HttpContent CreateHttpContent(IApiSettings settings, string signature, string nonce)
        {
            return new StringContent("", Encoding.UTF8);
        }

        public HttpRequestMessage CreateHttpRequestMessage(Uri uri, IApiSettings settings, string signature, HttpContent content, string nonce)
        {
            return new HttpRequestMessage();
        }

        public string CreateSignature(IApiSettings settings, Uri uri, string parameters, string nonce)
        {
            return "";
        }

    }
}
