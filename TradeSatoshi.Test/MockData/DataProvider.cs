using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TradeSatoshi.Test.MockData
{
    public class DataProvider
    {
        public static string DataDirectory { get; }
        static DataProvider()
        {
            DataDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MockData");
        }

        public static string GetCurrencies()
        {
            return File.ReadAllText(Path.Combine(DataDirectory, "CurrencyInfoData.txt"));
        }

        public static string GetCurrency()
        {
            return File.ReadAllText(Path.Combine(DataDirectory, "DetailedCurrencyInfo.txt"));
        }

        public static string GetTicker()
        {
            return File.ReadAllText(Path.Combine(DataDirectory, "TickerInfo.txt"));
        }
    }
}
