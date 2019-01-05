using CarsleySoft.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace CryptoExchange.API
{
    /// <summary>
    /// The ApiClient is used by several cryptocurrency exchanges.
    /// It uses an <see cref="IRestClient"/> to get data from the exchange.
    /// It then deserializes the returned json and returns the requested objects as T
    /// </summary>
    public class ApiClient : IDisposable, IApiClient
    {
        private readonly IRestClient _client;
        private readonly IApiSettings _settings;
        private readonly ILogger<IApiClient> _logger;
        private bool _disposed;

        public ApiClient(ILogger<IApiClient> logger)
            : this(logger, null, null)
        { }

        public ApiClient(ILogger<IApiClient> logger, IRestClient restClient, IApiSettings settings = null)
        {
            Requires.NotNull(logger, nameof(logger));
            _logger = logger;
            if(restClient == null)
                restClient = RestClient.CreateCryptoApiRestClient();
            _client = restClient;
            _settings = settings;
        }

        /// <summary>
        /// Adds a header value to the <see cref="IRestClient"/> default headers
        /// </summary>
        /// <param name="name">the name of the header</param>
        /// <param name="value">the value of the header</param>
        public void AddDefaultHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// Removes a header value from the <see cref="IRestClient"/> default headers
        /// </summary>
        /// <param name="name">the name of the header</param>
        public void ClearDefaultHeader(string name)
        {
            _client.DefaultRequestHeaders.Remove(name);
        }

        /// <summary>
        /// Sends a <c>GET</c> request to the specified <paramref name="uri"/>.
        /// </summary>
        /// <returns>The <see cref="HttpResponseMessage"/></returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="UriFormatException"/>
        public T GetRequest<T>(Uri uri)
        {
            return GetRequestAsync<T>(uri).Result;
        }


        /// <summary>
        /// Sends an asynchronous <c>GET</c> request to the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The Uri to request</param>
        /// <returns>The <see cref="HttpResponseMessage"/> via a <see cref="Task"/></returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="UriFormatException"/>
        public async Task<T> GetRequestAsync<T>(Uri uri) => await GetRequestAsync<T>(uri, CancellationToken.None);

        /// <summary>
        /// Sends an asynchronous <c>GET</c> request to the specified <paramref name="uri"/> using the given <paramref name="cancelToken"/>.
        /// </summary>
        /// <param name="uri">The Uri to request</param>
        /// <param name="cancelToken">a token used to cancel the asynchronous request</param>
        /// <returns>The <see cref="HttpResponseMessage"/> via a <see cref="Task"/></returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="UriFormatException"/>
        public async Task<T> GetRequestAsync<T>(Uri uri, CancellationToken cancelToken)
        {
            Requires.NotNull(uri, nameof(uri));
            return await _client.GetAsync(uri, cancelToken).ContinueWith(t => HandleResponse<T>(t, uri.ToString())).Unwrap();
        }

        /// <summary>
        /// Sends a HMAC-SHA512 signed asynchronous <c>GET</c> request to the specified <paramref name="uri"/> using the given <paramref name="cancelToken"/>.
        /// </summary>
        /// <param name="uri">The Uri to request</param>
        /// <param name="strategyObject">An <see cref="IRequestCreation"/> instance implementing all creation strategies</param>
        /// <param name="parameters">The request parameters to be serialized to json</param>
        /// <param name="cancelToken">a token used to cancel the asynchronous request</param>
        /// <returns>The <see cref="HttpResponseMessage"/> via a <see cref="Task"/></returns>
        public async Task<T> PostAsync<T>(Uri uri, IRequestCreation strategyObject = null, string parameters = "", CancellationToken cancelToken = default(CancellationToken))
        {
            Requires.NotNull(uri, nameof(uri));
            parameters = parameters ?? "";
            return await PostAsync<T>(uri, parameters, strategyObject, strategyObject, strategyObject, cancelToken);
        }

        /// <summary>
        /// Sends a Post request to the exchange API
        /// </summary>
        /// <param name="hashingStrategy">The Hashing Algorithm needed by the exchange API</param>
        /// <param name="uri">The Uri to request</param>
        /// <param name="queryString">The parameters passed to the hashing algorithm</param>
        /// <param name="content">An <see cref="HttpContent"/> instance or null</param>
        /// <param name="cancelToken">a token used to cancel the asynchronous request</param>
        /// <returns>The <see cref="HttpResponseMessage"/> via a <see cref="Task"/></returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="UriFormatException"/>
        public async Task<T> PostAsync<T>(Uri uri, string queryString = "", ISignatureCreation hashingStrategy = null, IHttpContentCreation contentStrategy = null, IHttpRequestMessageCreation requestStrategy = null, CancellationToken cancelToken = default(CancellationToken))
        {
            //TODO:  Test whether or not missing strategies will error.
            Requires.NotNull(uri, nameof(uri));
            string nonce;
#if NETSTANDARD2_0
            nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
#else
            nonce = DateTime.UtcNow.ConvertToUnixTime().ToString();
#endif
            string signature = hashingStrategy?.CreateSignature(_settings, uri, queryString, nonce);
            var content = contentStrategy?.CreateHttpContent(_settings, queryString, nonce);
            var requestMessage = requestStrategy?.CreateHttpRequestMessage(uri, _settings, signature, content, nonce);
            if(requestMessage == null)
            {
                requestMessage = new HttpRequestMessage(HttpMethod.Post, uri) { Content = content };
            }
            else
            {
                if (requestMessage.Method != HttpMethod.Post)
                    _logger.LogDebug($"PostAsync is not using the Http Post method. Instead it is using the Http verb {requestMessage.Method}.");
                if (requestMessage.RequestUri == null)
                    requestMessage.RequestUri = uri;
                if (requestMessage.Content == null)
                    requestMessage.Content = content;
            }
            return await _client.PostAsync(requestMessage).ContinueWith(t => HandleResponse<T>(t, uri.ToString())).Unwrap();
        }

        

        ///// <summary>
        ///// Sends a HMAC-SHA512 signed asynchronous <c>GET</c> request to the specified <paramref name="uri"/> using the given <paramref name="cancelToken"/>.
        ///// </summary>
        ///// <param name="strategy">The Hashing Algorithm needed by the exchange API</param>
        ///// <param name="uri">The Uri to request</param>
        ///// <param name="requestObject">The object containing request parameters to be serialized to json</param>
        ///// <param name="content">An <see cref="HttpContent"/> instance or null</param>
        ///// <param name="cancelToken">a token used to cancel the asynchronous request</param>
        ///// <returns>The <see cref="HttpResponseMessage"/> via a <see cref="Task"/></returns>
        ///// <exception cref="ArgumentNullException"/>
        ///// <exception cref="UriFormatException"/>
        //public async Task<T> PostAsync<T>(IHashingStrategy strategy, Uri uri, object requestObject = null, HttpContent content = null, CancellationToken cancelToken = default(CancellationToken))
        //{
        //    Requires.NotNull(uri, nameof(uri));
        //    string hash = strategy.ComputeHash(_settings, uri, queryString);
        //    return await _client.PostAsync(hash, content, cancelToken).ContinueWith(t => HandleResponse<T>(t, uri.ToString())).Unwrap();
        //}

        protected async Task<T> HandleResponse<T>(Task<HttpResponseMessage> task, string uri)
        {
            _logger.LogInformation("The task has a status of {0} and an IsCompleted flag of {1}.", Enum.GetName(typeof(TaskStatus), task.Status), (task.IsCompleted ? "true" : "false"));
            if (task.IsFaulted)
            {
                _logger.LogError(task.Exception.ToString());
                throw task.Exception;
            }
            T deserializedJson;
            string json;
            try
            {
                //await task.ConfigureAwait(false); //removes the synchronization context as the client no longer needs it at this point.
                json = await task.Result.Content.ReadAsStringAsync();
                deserializedJson = JsonConvert.DeserializeObject<T>(json);
                _logger.LogDebug($"Successful Response = {json}");
            }
            catch (Exception ex)
            {
                string msg = $"Unable to deserialize response from: {uri}. Exception: {ex.Message}";
                _logger.LogError(ex, msg);
                throw;
            }
            return deserializedJson;
        }

        public void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                if (_client != null)
                    _client.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
