using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoExchange.API
{
    public interface IApiClient
    {
        void AddDefaultHeader(string name, string value);
        void ClearDefaultHeader(string name);
        T GetRequest<T>(Uri uri);
        Task<T> GetRequestAsync<T>(Uri uri);
        Task<T> GetRequestAsync<T>(Uri uri, CancellationToken cancelToken);
        /// <summary>
        /// Sends a Post request to the exchange API
        /// </summary>
        /// <param name="uri">The Uri to request</param>
        /// <param name="strategyObject">An <see cref="IRequestCreation"/> instance implementing all creation strategies</param>
        /// <param name="queryString">The parameters passed to the hashing algorithm</param>
        /// <param name="content">An <see cref="HttpContent"/> instance or null</param>
        /// <param name="cancelToken">a token used to cancel the asynchronous request</param>
        /// <returns>The <see cref="HttpResponseMessage"/> via a <see cref="Task"/></returns>
        Task<T> PostAsync<T>(Uri uri, IRequestCreation strategyObject = null, string queryString = "", CancellationToken cancelToken = default(CancellationToken));

        /// <summary>
        /// Sends a Post request to the exchange API
        /// </summary>
        /// <param name="hashingStrategy">The Hashing Algorithm needed by the exchange API</param>
        /// <param name="uri">The Uri to request</param>
        /// <param name="queryString">The parameters passed to the hashing algorithm</param>
        /// <param name="content">An <see cref="HttpContent"/> instance or null</param>
        /// <param name="cancelToken">a token used to cancel the asynchronous request</param>
        /// <returns>The <see cref="HttpResponseMessage"/> via a <see cref="Task"/></returns>
        Task<T> PostAsync<T>(Uri uri, string queryString = "", ISignatureCreation hashingStrategy = null, IHttpContentCreation contentStrategy = null, IHttpRequestMessageCreation requestStrategy = null, CancellationToken cancelToken = default(CancellationToken));
    }
}