using System;
using TradeSatoshi.API;
using Autofac;
using CryptoExchange.API;
using System.Net.Http;
using TradeSatoshi.API.Entities.Response;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography;

namespace TradeSatoshi.Console
{
    class Program
    {
        /// <summary>
        /// Autofac IoC container
        /// </summary>
        private static IContainer _container = null;
        //private static TradeSatoshiSettings _settings;
        

        static Program()
        {
            _container = DependencyInjectionExample.AutoFacSetup();
        }

        static void Main(string[] args)
        {
            var container = DependencyInjectionExample.AutoFacSetup();
            //TestConnection(container);
            //GetCurrencies();
            //GetCurrenciesAsync();
            //GetCurrency("BTC");
            //GetCurrencyAsync("BTC");
            //GetTicker("LTC_BTC");
            //GetTickerAsync("LTC_BTC");
            //GetMarketStatus("LTC_BTC");
            //GetMarketStatusAsync("LTC_BTC");
            //GetMarketHistory("LTC_BTC", 5);
            //GetMarketHistoryAsync("LTC_BTC", 5);
            //GetMarketSummary("LTC_BTC");
            //GetMarketSummaryAsync("LTC_BTC");
            //GetMarketSummaries();
            //GetMarketSummariesAsync();
            //GetOrderBook("LTC_BTC");
            //GetOrderBookAsync("LTC_BTC", OrderType.Sell);
            //GetBalanceAsync("BTC");  //doesn't work
            //GetBalancesAsync();
            //GenerateAddressAsync("BTC");
            var result = StackExchangeQuestion.GetBalance("BTC");
            System.Console.WriteLine(result);
            System.Console.ReadKey();
        }

        

        private static void GetMarketStatus(string ticker)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = client.GetMarketStatus(ticker);
            System.Console.WriteLine(response);
        }

        private async static void GetMarketStatusAsync(string ticker)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetMarketStatusAsync(ticker);
            System.Console.WriteLine(response);
        }

        private async static void GetCurrencyAsync(string symbol)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetCurrencyAsync(symbol);
            System.Console.WriteLine(response);
        }

        private static void GetTicker(string ticker)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = client.GetTicker(ticker);
            System.Console.WriteLine(response);
        }

        private async static void GetTickerAsync(string ticker)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetTickerAsync(ticker);
            System.Console.WriteLine(response);
        }

        private static void GetCurrency(string symbol)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = client.GetCurrency(symbol);
            System.Console.WriteLine(response);
        }

        private static void TestConnection()
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = client.PingExchange();
            System.Console.WriteLine(response);
        }

        private static void GetCurrencies()
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            // Run.
            var result = client.GetCurrencies();
            foreach (var cur in result)
                System.Console.WriteLine(cur.Currency);
        }

        private async static void GetCurrenciesAsync()
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            // Run.
            var result = await client.GetCurrenciesAsync();
            foreach (var cur in result)
            {
                System.Console.WriteLine(cur);

            }
        }

        private static void GetMarketHistory(string market, int count)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var result = client.GetMarketHistory(market, count);
            foreach (var trade in result)
                System.Console.WriteLine(trade);
        }

        private async static void GetMarketHistoryAsync(string market, int count)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var result = await client.GetMarketHistoryAsync(market, count);
            foreach (var trade in result)
                System.Console.WriteLine(trade);
        }

        private static void RestClient_GetCurrencies()
        {
            System.Console.WriteLine("Hello World!");
            var container = DependencyInjectionExample.AutoFacSetup();
            var client = container.Resolve<IApiClient>();
            var result = client.GetRequest<TradeSatoshiResponse<IList<CurrencyInfo>>>(new Uri(@"https://tradesatoshi.com/api/public/getcurrencies"));
            System.Console.WriteLine(result);
        }

        private static void GetMarketSummary(string market)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = client.GetMarketSummary(market);
            System.Console.WriteLine(response);
        }

        private async static void GetMarketSummaryAsync(string market)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetMarketSummaryAsync(market);
            System.Console.WriteLine(response);
        }

        private static void GetMarketSummaries()
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = client.GetMarketSummaries();
            foreach (var summary in response)
                System.Console.WriteLine(summary);
        }

        private async static void GetMarketSummariesAsync()
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetMarketSummariesAsync();
            foreach (var summary in response)
                System.Console.WriteLine(summary);
        }

        private static void GetOrderBook(string market, OrderType type = default(OrderType), int depth = 20)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = client.GetOrderBook(market, type, depth);
            foreach (var ordertype in response)
            {
                System.Console.WriteLine(ordertype.Key + ":");
                foreach(var entry in ordertype.Value)
                {
                    System.Console.WriteLine("\t" + entry);
                }
                System.Console.WriteLine();
            }   
        }

        private async static void GetOrderBookAsync(string market, OrderType type = default(OrderType), int depth = 20)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetOrderBookAsync(market, type, depth);
            foreach (var ordertype in response)
            {
                System.Console.WriteLine(ordertype.Key + ":");
                foreach (var entry in ordertype.Value)
                {
                    System.Console.WriteLine("\t" + entry);
                }
                System.Console.WriteLine();
            }
        }

        private async static void GetBalanceAsync(string currencySymbol)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetBalanceAsync(currencySymbol);
            System.Console.WriteLine(response);
        }

        private async static void GetBalancesAsync()
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetBalancesAsync();
            foreach(var info in response)
                System.Console.WriteLine(info);
        }

        private async static void GetOrderAsync(int orderId)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetOrderAsync(orderId);
            System.Console.WriteLine(response);
        }

        private async static void GetOrdersAsync(string market, int count = 20)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GetOrdersAsync(market, count);
            System.Console.WriteLine(response);
        }

        private async static void GenerateAddressAsync(string symbol)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.GenerateAddressAsync(symbol);
            System.Console.WriteLine(response);
        }

        private async static void SubmitOrderAsync(string market, OrderType orderType, decimal amount, decimal price)
        {
            var client = _container.Resolve<TradeSatoshiClient>();
            var response = await client.SubmitOrderAsync(market, orderType, amount, price);
            System.Console.WriteLine(response);
        }



        //private async static void GetBalancesAsync()
        //{
        //    var client = _container.Resolve<TradeSatoshiClient>();
        //    //var response = await client.GetBalancesAsync();
        //    //System.Console.WriteLine(response);

        //    var uri = new Uri("https://tradesatoshi.com/api/private/getbalances");
        //    var settings = client.Settings;
        //    string nonce = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        //    var strategy = new TradeSatoshiHashingStrategy();
        //    string signature = strategy.CreateSignature(settings, uri, null, nonce);
        //    //var signature = GetSignature(settings.PrivateKey, settings.PublicKey, uri.ToString(), nonce);
        //    string authenticationString = "Basic " + settings.PublicKey + ":" + signature + ":" + nonce;
        //    //client.DefaultRequestHeaders.Add("Authorization", authenticationString);
        //    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    var content = new StringContent("", Encoding.UTF8, "application/json");
        //    var message = new HttpRequestMessage(HttpMethod.Post, uri);
        //    message.Headers.Add("Authorization", authenticationString);
        //    message.Content = content;
        //    //content.Headers.Add("Content-Type", "application/json;");
        //    using (var c = new HttpClient())
        //    {
        //        var result = await c.SendAsync(message);
        //        var retVal = await result.Content.ReadAsStringAsync();
        //        System.Console.Write(retVal);
        //    }

        //}

        //private static string GetSignature(string privatekey, string publickey, string uri, string nonce, string post_params = null)
        //{
        //    uri = WebUtility.UrlEncode(uri).ToLower();
        //    string signature = "";
        //    if (post_params != null)
        //    {
        //        post_params = Convert.ToBase64String(Encoding.UTF8.GetBytes(post_params));
        //        signature = publickey + "POST" + uri + nonce + post_params;
        //    }
        //    else
        //    {
        //        var bytes = Encoding.UTF8.GetBytes("");
        //        signature = publickey + "POST" + uri + nonce + Convert.ToBase64String(bytes);
        //    }
        //    byte[] messageBytes = Encoding.UTF8.GetBytes(signature);
        //    using (HMACSHA512 _object = new HMACSHA512(Convert.FromBase64String(privatekey)))
        //    {
        //        byte[] hashmessage = _object.ComputeHash(messageBytes);
        //        return Convert.ToBase64String(hashmessage);
        //    }
        //}
    }
}
