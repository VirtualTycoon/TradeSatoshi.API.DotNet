using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CryptoExchange.API.Test
{
    public class ApiClientTests
    {
        //[Fact]
        //public async void TestConnection_Call_ReturnsEmptyResponse()
        //{
        //    //Arrange
        //    var logger = new Mock<ILogger<IApiClient>>();
        //    var restClient = new Mock<IRestClient>();
        //    var requestUri = new Uri("http://google.com");
        //    var expectedContent = "Response text";
        //    var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedContent) };
        //    restClient.Setup(x => x.GetAsync(requestUri))
        //        .Returns(Task.FromResult(mockResponse));
        //    var apiClient = new ApiClient(logger.Object, restClient.Object);

        //    //Act
        //    var response = apiClient.GetRequest(requestUri);
        //    var actualcontent = await response.Content.ReadAsStringAsync();
        //    Debug.WriteLine(actualcontent);

        //    //Assert
        //    Assert.True(expectedContent == actualcontent);
        //}

        //[Fact]
        //public async void GetRequest_GetAsyncMethodGetsCalled_ReturnsExpectedResponse()
        //{
        //    //Arrange
        //    var requestUri = new Uri("http://google.com");
        //    var expectedContent = "Response text";
        //    var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(expectedContent) };
        //    var httpHandler = new Mock<HttpClientHandler>();
        //    var rclient = new RestClient(null, httpHandler.Object, true, null, null);
        //    var restClient = Mock.Get<RestClient>(rclient);
        //    restClient.Setup(x => x.GetAsync(It.IsAny<Uri>()))
        //        .Returns(Task.FromResult(mockResponse));
        //    var logger = new Mock<ILogger<IApiClient>>();
        //    var apiClient = new ApiClient(logger.Object, restClient.Object);

        //    //Act
        //    var response = apiClient.GetRequest(requestUri);
        //    var actualcontent = await response.Content.ReadAsStringAsync();

        //    //Assert
        //    restClient.Verify(x => x.GetAsync(It.IsAny<Uri>()), Times.Once);
        //    Assert.True(expectedContent == actualcontent);
        //}

        [Fact]
        public void GetRequest_RestClientGetAsyncMethodIsCalled_ReturnsExpectedResponse()
        {
            //Arrange

            var logger = new Mock<ILogger<IApiClient>>();
            var restClient = new Mock<IRestClient>();
            var requestUri = new Uri("http://google.com");
            var expectedContent = "Response Data";
            var jsonContent = JsonConvert.SerializeObject(expectedContent);
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonContent) };
            var apiClient = new ApiClient(logger.Object, restClient.Object);
            restClient.Setup(x => x.GetAsync(requestUri, CancellationToken.None)).Returns(Task.FromResult(mockResponse));

            //Act
            var response = apiClient.GetRequest<string>(requestUri);
            Debug.WriteLine(response);

            //Assert
            restClient.Verify(x => x.GetAsync(requestUri, CancellationToken.None), Times.Once);
            Assert.True(expectedContent == response);
        }

        [Fact]
        public async void GetRequestAsync_WithNoCancellationToken_HasACancellationTokenValueOfNone()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var uri = new Uri("http://google.com");
                var cancelToken = CancellationToken.None;
                var restClient = mock.Mock<IRestClient>();
                var jsonContent = JsonConvert.SerializeObject("Response Data");
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonContent) };
                restClient.Setup(x => x.GetAsync(uri, It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse)).Verifiable();
                mock.Provide(restClient.Object);
                var apiClient = mock.Create<ApiClient>();

                //Act
                var response = await apiClient.GetRequestAsync<string>(uri);

                //Assert
                restClient.Verify(x => x.GetAsync(uri, cancelToken), Times.Once);
                restClient.VerifyAll();
            }
        }

        [Fact]
        public async void GetRequestAsync_WithProvidedCancellationToken_HasSameCancellationTokenPassed()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                var uri = new Uri("http://google.com");
                var cancelToken = new CancellationTokenSource().Token;
                var restClient = mock.Mock<IRestClient>();
                var jsonContent = JsonConvert.SerializeObject("Response Data");
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonContent) };
                restClient.Setup(x => x.GetAsync(uri, It.IsAny<CancellationToken>())).Returns(Task.FromResult(mockResponse)).Verifiable();
                mock.Provide(restClient.Object);
                var apiClient = mock.Create<ApiClient>();

                //Act
                var response = await apiClient.GetRequestAsync<string>(uri, cancelToken);

                //Assert
                restClient.Verify(x => x.GetAsync(uri, cancelToken), Times.Once);
                restClient.VerifyAll();
            }
        }
    }
}
