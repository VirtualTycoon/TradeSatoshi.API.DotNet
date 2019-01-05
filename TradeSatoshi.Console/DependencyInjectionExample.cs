using Autofac;
using CryptoExchange.API;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TradeSatoshi.API;

namespace TradeSatoshi.Console
{
    internal class DependencyInjectionExample
    {
        private const string PUBLIC_API_KEY = "XXX";
        private const string PRIVATE_API_KEY = "XXX";
        //private static IServiceProvider BuildDi()
        //{
        //    var services = new ServiceCollection();

        //    services.AddTransient<TradeSatoshiClient>();

        //    services.AddSingleton<ILoggerFactory, LoggerFactory>();
        //    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
        //    services.AddLogging((builder) => builder.SetMinimumLevel(LogLevel.Trace));

        //    var serviceProvider = services.BuildServiceProvider();

        //    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        //    //configure NLog
        //    loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
        //    NLog.LogManager.LoadConfiguration("nlog.config");

        //    return serviceProvider;
        //}

        public static IContainer AutoFacSetup()
        {
            // Autofac container.
            var builder = new ContainerBuilder();

            var settings = new TradeSatoshiSettings { PublicKey = PUBLIC_API_KEY, PrivateKey = PRIVATE_API_KEY };
            builder.RegisterInstance<TradeSatoshiSettings>(settings).As<TradeSatoshiSettings>().As<IApiSettings>();

            // Create Logger<T> when ILogger<T> is required.
            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>));

            // Use NLogLoggerFactory as a factory required by Logger<T>.
            builder.RegisterType<NLogLoggerFactory>()
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            IRestClient client = RestClient.CreateCryptoApiRestClient();
            builder.RegisterInstance(client).As<IRestClient>().SingleInstance();

            builder.RegisterType<ApiClient>().As<IApiClient>().SingleInstance();

            builder.RegisterType<TradeSatoshiClient>();


            // Finish registrations and prepare the container that can resolve things.
            var container = builder.Build();
            return container;

        }
    }
}
