namespace TradeSatoshi.API
{
    //TODO:  We will never return a value of Both.  Both is just for requests, not for responses, so maybe split
    //the requests and response types into two different enums??
    public enum OrderType
    {
        Both = 0,
        Buy,
        Sell
    }
}