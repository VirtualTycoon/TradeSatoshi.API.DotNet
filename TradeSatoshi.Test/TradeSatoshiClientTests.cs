using Autofac.Extras.Moq;
using CryptoExchange.API;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeSatoshi.API;
using TradeSatoshi.API.Entities.Response;
using TradeSatoshi.Test.MockData;
using Xunit;

namespace TradeSatoshi.Test
{
    public class TradeSatoshiClientTests
    {
        //private Mock<ILogger> _logger;
        //private Mock<RestClient> _restClient;
        //private Mock<ApiClient> _apiClient;

        public TradeSatoshiClientTests()
        {
            
        }
        //[Fact]
        //public void TestConnection_Call_ReturnsEmptyResponse()
        //{
        //    var logger = new Mock<ILogger>();
        //    var client = new TradeSatoshiClient(logger.Object);
        //    var message = client.GetCurrencies();
        //    Assert.True(message == "Meow");
        //}

        [Fact]
        public void TestConnection_Call_ReturnsTrue()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //arrange
                var client = mock.Create<TradeSatoshiClient>();
                //act
                var result = client.PingExchange();
                //assert
                Assert.True(result);
            } 
        }

        [Fact]
        public void GetCurrencies_WithMockData_UnwrapsTradeSatoshiResponseAndReturnsListOfCurrencies()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var apiClient = mock.Mock<IApiClient>();
                mock.Provide(apiClient.Object);
                var json = DataProvider.GetCurrencies();
                var deserializedJson = JsonConvert.DeserializeObject<TradeSatoshiResponse<IList<CurrencyInfo>>>(json);
                var client = mock.Create<TradeSatoshiClient>();
                apiClient.Setup(x => x.GetRequest<TradeSatoshiResponse<IList<CurrencyInfo>>>(It.IsAny<Uri>())).Returns(deserializedJson);
                //Act
                var currencies = client.GetCurrencies();

                //Assert
                Assert.Equal(deserializedJson.Data, currencies);
                Assert.Same(deserializedJson.Data, currencies);
            }
        }

        [Fact]
        public async void GetCurrenciesAsync_WithMockData_UnwrapsTradeSatoshiResponseAndReturnsListOfCurrencies()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var apiClient = mock.Mock<IApiClient>();
                mock.Provide(apiClient.Object);
                var json = DataProvider.GetCurrencies();
                var deserializedJsonTask = Task.FromResult(JsonConvert.DeserializeObject<TradeSatoshiResponse<IList<CurrencyInfo>>>(json));
                var client = mock.Create<TradeSatoshiClient>();
                apiClient.Setup(x => x.GetRequestAsync<TradeSatoshiResponse<IList<CurrencyInfo>>>(It.IsAny<Uri>())).Returns(deserializedJsonTask);
                //Act
                var currencies = await client.GetCurrenciesAsync();

                //Assert
                Assert.Equal(deserializedJsonTask.Result.Data, currencies);
                Assert.Same(deserializedJsonTask.Result.Data, currencies);
            }
        }

        [Fact]
        public void GetCurrency_WithMockData_AllPropertiesArePopulated()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var apiClient = mock.Mock<IApiClient>();
                mock.Provide(apiClient.Object);
                var json = DataProvider.GetCurrency();
                var deserializedJson = JsonConvert.DeserializeObject<TradeSatoshiResponse<DetailedCurrencyInfo>>(json);
                var client = mock.Create<TradeSatoshiClient>();
                apiClient.Setup(x => x.GetRequest<TradeSatoshiResponse<DetailedCurrencyInfo>>(It.IsAny<Uri>())).Returns(deserializedJson);
                //Act
                var currency = client.GetCurrency("BTC");

                //Assert
                Assert.NotNull(currency);
                Assert.NotNull(currency.StatusMessage);
                Assert.True(currency.IsTipEnabled);
                Assert.True(currency.MinBaseTrade == 0.00010000m);
                Assert.True(currency.MinTip == 0.00000100m);
                Assert.True(currency.MaxTip == 0.10000000m);
            }
        }

        [Fact]
        public async void GetCurrencyAsync_WithMockData_AllPropertiesArePopulated()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var apiClient = mock.Mock<IApiClient>();
                mock.Provide(apiClient.Object);
                var json = DataProvider.GetCurrency();
                var deserializedJson = Task.FromResult(JsonConvert.DeserializeObject<TradeSatoshiResponse<DetailedCurrencyInfo>>(json));
                var client = mock.Create<TradeSatoshiClient>();
                apiClient.Setup(x => x.GetRequestAsync<TradeSatoshiResponse<DetailedCurrencyInfo>>(It.IsAny<Uri>())).Returns(deserializedJson);
                //Act
                var currency = await client.GetCurrencyAsync("BTC");

                //Assert
                Assert.NotNull(currency);
                Assert.NotNull(currency.StatusMessage);
                Assert.True(currency.IsTipEnabled);
                Assert.True(currency.MinBaseTrade == deserializedJson.Result.Data.MinBaseTrade);
                Assert.True(currency.MinTip == 0.00000100m);
                Assert.True(currency.MaxTip == 0.10000000m);
            }
        }

        [Fact]
        public async Task GetCurrencyAsync_InvalidSymbol_ThrowsException()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var apiClient = mock.Mock<IApiClient>();
                mock.Provide(apiClient.Object);
                var json = DataProvider.GetCurrency();
                var deserializedJson = Task.FromResult(JsonConvert.DeserializeObject<TradeSatoshiResponse<DetailedCurrencyInfo>>(json));
                deserializedJson.Result.IsSuccess = false;
                deserializedJson.Result.Message = "Something bad happened.  Please try again later.";
                var client = mock.Create<TradeSatoshiClient>();
                apiClient.Setup(x => x.GetRequestAsync<TradeSatoshiResponse<DetailedCurrencyInfo>>(It.IsAny<Uri>())).Returns(deserializedJson);
                //Act & Assert
                var currency = await Assert.ThrowsAsync<TradeSatoshiException>(() => client.GetCurrencyAsync("xxxBTC"));
            }
        }

        [Fact]
        public async void GetTickerAsync_WithMockData_AllPropertiesArePopulated()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var apiClient = mock.Mock<IApiClient>();
                mock.Provide(apiClient.Object);
                var json = DataProvider.GetTicker();
                var deserializedJson = Task.FromResult(JsonConvert.DeserializeObject<TradeSatoshiResponse<TickerInfo>>(json));
                var client = mock.Create<TradeSatoshiClient>();
                apiClient.Setup(x => x.GetRequestAsync<TradeSatoshiResponse<TickerInfo>>(It.IsAny<Uri>())).Returns(deserializedJson);
                //Act
                var ticker = await client.GetTickerAsync("LTC_BTC");

                //Assert
                Assert.NotNull(ticker);
                Assert.True(ticker.Ask == 100.00000000m);
                Assert.True(ticker.Bid == deserializedJson.Result.Data.Bid);
                Assert.True(ticker.Last == 0.01000000m);
                Assert.True(ticker.Market == "LTC_BTC");
            }
        }

        [Fact]
        public async Task GetTickerAsync_InvalidTicker_ThrowsException()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var apiClient = mock.Mock<IApiClient>();
                mock.Provide(apiClient.Object);
                var json = DataProvider.GetTicker();
                var deserializedJson = Task.FromResult(JsonConvert.DeserializeObject<TradeSatoshiResponse<TickerInfo>>(json));
                deserializedJson.Result.IsSuccess = false;
                deserializedJson.Result.Message = "Something bad happened.  Please try again later.";
                var client = mock.Create<TradeSatoshiClient>();
                apiClient.Setup(x => x.GetRequestAsync<TradeSatoshiResponse<TickerInfo>>(It.IsAny<Uri>())).Returns(deserializedJson);
                //Act & Assert
                var currency = await Assert.ThrowsAsync<TradeSatoshiException>(() => client.GetTickerAsync("LTC?BTC"));
            }
        }
    }
}


    //var apiClient = mock.Mock<IApiClient>();
    //mock.Provide(apiClient.Object);
    //            var json = DataProvider.GetCurrencies();
    //var httpResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json) };
    //apiClient.Protected().Setup<Task<TradeSatoshiResponse<IList<CurrencyInfo>>>>("HandleResponse",
    //                ItExpr.Is<HttpResponseMessage>(x => x.Content == httpResponse.Content)).Verifiable();