using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CryptoExchange.API;

namespace TradeSatoshi.API
{


    public static class ApiEndpoints
    {
        public static string BaseEndpoint => "https://tradesatoshi.com/";
        public static string BaseApiUrl => $"{BaseEndpoint}api/";

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> equal to https://tradesatoshi.com/api/public/getcurrencies
        /// </summary>
        public static ApiEndpoint GetCurrencies_Endpoint => new ApiEndpoint($"{BaseApiUrl}public/getcurrencies");

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> similar to https://tradesatoshi.com/api/public/GetCurrency?Symbol=BTC for the requested symbol
        /// </summary>
        /// <param name="symbol">The ticker symbol of the currency</param>
        /// <returns>An <see cref="ApiEndpoint"/> with no security</returns>
        public static ApiEndpoint GetCurrency_Endpoint(string symbol)
        {
            return new ApiEndpoint($"{BaseApiUrl}public/GetCurrency?Symbol={symbol}");
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> similar to https://tradesatoshi.com/api/public/getticker?market=LTC_BTC for the requested market
        /// </summary>
        /// <param name="ticker">The ticker for the market (e.g. LTC_BTC)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with no security</returns>
        public static ApiEndpoint GetTicker_Endpoint(string ticker)
        {
            return new ApiEndpoint($"{BaseApiUrl}public/getticker?market={ticker}");
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> similar to https://tradesatoshi.com/api/public/GetMarketStatus?market=LTC_BTC for the requested market
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with no security</returns>
        public static ApiEndpoint GetMarketStatus_Endpoint(string market)
        {
            return new ApiEndpoint($"{BaseApiUrl}public/GetMarketStatus?market={market}");
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> similar to https://tradesatoshi.com/api/public/getmarkethistory?market=LTC_BTC&count=20 for the requested market
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <param name="count">The max amount of records to return (optional, default: 20)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with no security</returns>
        public static ApiEndpoint GetMarketHistory_Endpoint(string market, int count = 20)
        {
            return new ApiEndpoint($"{BaseApiUrl}public/getmarkethistory?market={market}&count={count}");
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> similar to https://tradesatoshi.com/api/public/getmarketsummary?market=LTC_BTC for the requested market
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with no security</returns>
        public static ApiEndpoint GetMarketSummary_Endpoint(string market)
        {
            return new ApiEndpoint($"{BaseApiUrl}public/getmarketsummary?market={market}");
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> equal to https://tradesatoshi.com/api/public/getmarketsummaries
        /// </summary>
        /// <param name="market">The ticker for the market (e.g. LTC_BTC)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with no security</returns>
        public static ApiEndpoint GetMarketSummaries_Endpoint()
        {
            return new ApiEndpoint($"{BaseApiUrl}public/getmarketsummaries");
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> similar to https://tradesatoshi.com/api/public/getorderbook?market=LTC_BTC&type=both&depth=20
        /// </summary>
        /// <param name="market">The market name (e.g. LTC_BTC)</param>
        /// <param name="type">The order book type 'buy', 'sell', 'both' (optional, default: 'both')</param>
        /// <param name="depth">Max of records to return (optional, default: 20)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with no security</returns>
        internal static ApiEndpoint GetOrderBook_Endpoint(string market, OrderType type = default(OrderType), int depth = 20)
        {
            return new ApiEndpoint($"{BaseApiUrl}public/getorderbook?market={market}&type={type}&depth={depth}");
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> equal to https://tradesatoshi.com/api/private/getbalance with signed security
        /// </summary>
        /// <param name="market">The symbol of the currency (e.g. BTC)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with signed authentication method</returns>
        internal static ApiEndpoint GetBalance_Endpoint()
        {
            return new ApiEndpoint($"{BaseApiUrl}private/getbalance", AuthenticationMethod.HMAC_SHA512);
        }

        /// <summary>
        /// Creates an <see cref="ApiEndpoint" /> equal to https://tradesatoshi.com/api/private/getbalance with signed security
        /// </summary>
        /// <param name="market">The symbol of the currency (e.g. BTC)</param>
        /// <returns>An <see cref="ApiEndpoint"/> with signed authentication method</returns>
        internal static ApiEndpoint GetBalances_Endpoint()
        {
            return new ApiEndpoint($"{BaseApiUrl}private/getbalances", AuthenticationMethod.HMAC_SHA512);
        }

        internal static ApiEndpoint GenerateAddress_Endpoint()
        {
            return new ApiEndpoint($"{BaseApiUrl}private/generateaddress", AuthenticationMethod.HMAC_SHA512);
        }

        internal static ApiEndpoint GetOrder_Endpoint()
        {
            return new ApiEndpoint($"{BaseApiUrl}private/getorder", AuthenticationMethod.HMAC_SHA512);
        }

        internal static ApiEndpoint GetOrders_Endpoint()
        {
            return new ApiEndpoint($"{BaseApiUrl}private/getorders", AuthenticationMethod.HMAC_SHA512);
        }

        internal static ApiEndpoint SubmitOrder_Endpoint()
        {
            return new ApiEndpoint($"{BaseApiUrl}private/submitorder", AuthenticationMethod.HMAC_SHA512);
        }
    }
}
