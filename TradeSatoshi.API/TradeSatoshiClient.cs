using CarsleySoft.Common;
using CryptoExchange.API;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TradeSatoshi.API.Entities.Request;
using TradeSatoshi.API.Entities.Response;

namespace TradeSatoshi.API
{
    public class TradeSatoshiClient
    {
        private readonly ILogger<TradeSatoshiClient> _logger;
        private readonly IApiClient _client;

        public TradeSatoshiSettings Settings { get; }

        public TradeSatoshiClient(ILogger<TradeSatoshiClient> logger, IApiClient client) : this(logger, client, null)
        {
        }

        public TradeSatoshiClient(ILogger<TradeSatoshiClient> logger, IApiClient client, TradeSatoshiSettings settings)
        {
            _logger = logger;
            _client = client;
            Settings = settings;
        }

        /// <summary>
        /// Used to check the success flag of the json response
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="response">The json response</param>
        private void ValidateResponse<T>(TradeSatoshiResponse<T> response)
        {
            if (!response.IsSuccess)
            {
                _logger.LogError($"The response returned a message of '{response.Message}'");
                throw new TradeSatoshiException(response.Message);
            }
        }

        /// <summary>
        /// Pings the TradeSatoshi server
        /// </summary>
        /// <returns>true if a connection was successful, otherwise false</returns>
        public bool PingExchange()
        {
            Ping sender = new Ping();
            Uri uri = new Uri(ApiEndpoints.BaseEndpoint);
            PingReply reply = sender.Send(uri.Host);
            _logger.LogInformation("Testing Status of Host: {0}", ApiEndpoints.BaseEndpoint);
            if (reply.Status == IPStatus.Success)
            {
                _logger.LogInformation("IP Address: {0}", reply.Address.ToString());
                _logger.LogInformation("RoundTrip time: {0}", reply.RoundtripTime);
                _logger.LogInformation("Time to live: {0}", reply.Options.Ttl);
                _logger.LogInformation("Buffer size: {0}", reply.Buffer.Length);
                return true;
            }
            _logger.LogInformation(Enum.GetName(typeof(IPStatus), reply.Status));
            return false;
        }

        public IList<CurrencyInfo> GetCurrencies()
        {
            var endpoint = ApiEndpoints.GetCurrencies_Endpoint;
            var result = _client.GetRequest<TradeSatoshiResponse<IList<CurrencyInfo>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public async Task<IList<CurrencyInfo>> GetCurrenciesAsync()
        {
            var endpoint = ApiEndpoints.GetCurrencies_Endpoint;
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<IList<CurrencyInfo>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public DetailedCurrencyInfo GetCurrency(string symbol)
        {
            var endpoint = ApiEndpoints.GetCurrency_Endpoint(symbol);
            var result = _client.GetRequest<TradeSatoshiResponse<DetailedCurrencyInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public async Task<DetailedCurrencyInfo> GetCurrencyAsync(string symbol)
        {
            var endpoint = ApiEndpoints.GetCurrency_Endpoint(symbol);
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<DetailedCurrencyInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public TickerInfo GetTicker(string ticker)
        {
            var endpoint = ApiEndpoints.GetTicker_Endpoint(ticker);
            var result = _client.GetRequest<TradeSatoshiResponse<TickerInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public async Task<TickerInfo> GetTickerAsync(string ticker)
        {
            var endpoint = ApiEndpoints.GetTicker_Endpoint(ticker);
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<TickerInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public MarketStatusInfo GetMarketStatus(string ticker)
        {
            var endpoint = ApiEndpoints.GetMarketStatus_Endpoint(ticker);
            var result = _client.GetRequest<TradeSatoshiResponse<MarketStatusInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public async Task<MarketStatusInfo> GetMarketStatusAsync(string ticker)
        {
            var endpoint = ApiEndpoints.GetMarketStatus_Endpoint(ticker);
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<MarketStatusInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets the history of trades for the passed in market
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <param name="count">The max amount of records to return (optional, default: 20)</param>
        /// <returns>A list of trades</returns>
        public IList<MarketHistoryInfo> GetMarketHistory(string market, int count = 20)
        {
            var endpoint = ApiEndpoints.GetMarketHistory_Endpoint(market, count);
            var result = _client.GetRequest<TradeSatoshiResponse<IList<MarketHistoryInfo>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets the history of trades for the passed in market
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <param name="count">The max amount of records to return (optional, default: 20)</param>
        /// <returns>A list of trades</returns>
        public async Task<IList<MarketHistoryInfo>> GetMarketHistoryAsync(string market, int count = 20)
        {
            var endpoint = ApiEndpoints.GetMarketHistory_Endpoint(market, count);
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<IList<MarketHistoryInfo>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets the market summary for the current day for the requested market
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <returns>The market summary</returns>
        public MarketSummaryInfo GetMarketSummary(string market)
        {
            var endpoint = ApiEndpoints.GetMarketSummary_Endpoint(market);
            var result = _client.GetRequest<TradeSatoshiResponse<MarketSummaryInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets the market summary for the current day for the passed in market
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <returns>The market summary</returns>
        public async Task<MarketSummaryInfo> GetMarketSummaryAsync(string market)
        {
            var endpoint = ApiEndpoints.GetMarketSummary_Endpoint(market);
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<MarketSummaryInfo>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets the market summary for the current day for all markets
        /// </summary>
        /// <returns>A list of market summary</returns>
        public IList<MarketSummaryInfo> GetMarketSummaries()
        {
            var endpoint = ApiEndpoints.GetMarketSummaries_Endpoint();
            var result = _client.GetRequest<TradeSatoshiResponse<IList<MarketSummaryInfo>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets the market summary for the current day for all markets
        /// </summary>
        /// <returns>A list of market summary</returns>
        public async Task<IList<MarketSummaryInfo>> GetMarketSummariesAsync()
        {
            var endpoint = ApiEndpoints.GetMarketSummaries_Endpoint();
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<IList<MarketSummaryInfo>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public IDictionary<OrderType, IList<BookEntry>> GetOrderBook(string market, OrderType type = default(OrderType), int depth = 20)
        {
            var endpoint = ApiEndpoints.GetOrderBook_Endpoint(market, type, depth);
            var result = _client.GetRequest<TradeSatoshiResponse<IDictionary<OrderType, IList<BookEntry>>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }

        public async Task<IDictionary<OrderType, IList<BookEntry>>> GetOrderBookAsync(string market, OrderType type = default(OrderType), int depth = 20)
        {
            var endpoint = ApiEndpoints.GetOrderBook_Endpoint(market, type, depth);
            var result = await _client.GetRequestAsync<TradeSatoshiResponse<IDictionary<OrderType, IList<BookEntry>>>>(endpoint.Uri);
            ValidateResponse(result);
            return result.Data;
        }


        public async Task<BalanceInfo> GetBalanceAsync(string currencySymbol)
        {
            Requires.NotNull(Settings, nameof(Settings));
            var endpoint = ApiEndpoints.GetBalance_Endpoint();
            var strategy = new TradeSatoshiRequestCreationStrategy();
            var jsonObj = new JObject();
            jsonObj.Add("Currency", currencySymbol);
            var result = await _client.PostAsync<TradeSatoshiResponse<BalanceInfo>>(endpoint.Uri, strategy, JsonConvert.SerializeObject(jsonObj));
            ValidateResponse(result);
            return result.Data;
        }

        public async Task<IList<BalanceInfo>> GetBalancesAsync()
        {
            Requires.NotNull(Settings, nameof(Settings));
            var endpoint = ApiEndpoints.GetBalances_Endpoint();
            var strategy = new TradeSatoshiRequestCreationStrategy();
            var result = await _client.PostAsync<TradeSatoshiResponse<IList<BalanceInfo>>>(endpoint.Uri, strategy);
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets a placed order using the given <paramref name="orderId"/>.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<OrderInfo> GetOrderAsync(int orderId)
        {
            Requires.NotNull(Settings, nameof(Settings));
            var endpoint = ApiEndpoints.GetOrder_Endpoint();
            var strategy = new TradeSatoshiRequestCreationStrategy();
            var jsonObj = new JObject();
            jsonObj.Add("OrderId", orderId);
            var result = await _client.PostAsync<TradeSatoshiResponse<OrderInfo>>(endpoint.Uri, strategy, JsonConvert.SerializeObject(jsonObj));
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Gets the orders for the given <paramref name="market"/>.
        /// </summary>
        /// <param name="market">The name of the currency market (e.g. LTC_BTC)</param>
        /// <param name="count">The maximum number of orders to return (default 20).</param>
        /// <returns></returns>
        public async Task<IList<OrderInfo>> GetOrdersAsync(string market, int count = 20)
        {
            Requires.NotNull(Settings, nameof(Settings));
            var endpoint = ApiEndpoints.GetOrders_Endpoint();
            var strategy = new TradeSatoshiRequestCreationStrategy();
            var jsonObj = new JObject();
            jsonObj.Add("Market", market);
            jsonObj.Add("Count", count);
            var result = await _client.PostAsync<TradeSatoshiResponse<IList<OrderInfo>>>(endpoint.Uri, strategy, JsonConvert.SerializeObject(jsonObj));
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Generates an deposit address for the given <paramref name="symbol"/>
        /// </summary>
        /// <param name="symbol">The currency symbol (e.g. BTC)</param>
        /// <returns></returns>
        public async Task<GenerateAddressInfo> GenerateAddressAsync(string symbol)
        {
            Requires.NotNull(Settings, nameof(Settings));
            var endpoint = ApiEndpoints.GenerateAddress_Endpoint();
            var jsonObj = new JObject();
            jsonObj.Add("Currency", symbol);
            var strategy = new TradeSatoshiRequestCreationStrategy();
            var result = await _client.PostAsync<TradeSatoshiResponse<GenerateAddressInfo>>(endpoint.Uri, strategy, JsonConvert.SerializeObject(jsonObj));
            ValidateResponse(result);
            return result.Data;
        }

        public async Task<SubmitOrderInfo> SubmitOrderAsync(string market, OrderType orderType, decimal amount, decimal price)
        {
            Requires.NotNull(Settings, nameof(Settings));
            var endpoint = ApiEndpoints.SubmitOrder_Endpoint();
            var strategy = new TradeSatoshiRequestCreationStrategy();
            var jsonObj = new JObject();
            jsonObj.Add("Market", market);
            jsonObj.Add("OrderType", Enum.GetName(typeof(OrderType), orderType));
            jsonObj.Add("Amount", amount);
            jsonObj.Add("Price", price);
            var result = await _client.PostAsync<TradeSatoshiResponse<SubmitOrderInfo>>(endpoint.Uri, strategy, JsonConvert.SerializeObject(jsonObj));
            ValidateResponse(result);
            return result.Data;
        }

        /// <summary>
        /// Cancels a submitted order
        /// </summary>
        /// <param name="cancelType">The cancel type, options: 'Single','Market','MarketBuys','MarketSells','AllBuys','AllSells','All'(required)</param>
        /// <param name="orderId">The order to cancel(required if cancel type 'Single')</param>
        /// <param name="market">The order to cancel(required if cancel type 'Market','MarketBuys','MarketSells')</param>
        /// <returns></returns>
        public async Task<CancelOrderInfo> CancelOrderAsync(CancelType cancelType, int? orderId, string market = null)
        {
            Requires.NotNull(Settings, nameof(Settings));
            var endpoint = ApiEndpoints.SubmitOrder_Endpoint();
            var strategy = new TradeSatoshiRequestCreationStrategy();
            var jsonObj = new JObject();
            jsonObj.Add("Type", Enum.GetName(typeof(CancelType), cancelType));
            if(orderId.HasValue)
                jsonObj.Add("OrderId", orderId);
            if(!string.IsNullOrWhiteSpace(market))
                jsonObj.Add("Market", market);
            var result = await _client.PostAsync<TradeSatoshiResponse<CancelOrderInfo>>(endpoint.Uri, strategy, JsonConvert.SerializeObject(jsonObj));
            ValidateResponse(result);
            return result.Data;
        }


    }
}
