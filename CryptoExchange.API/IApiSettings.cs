namespace CryptoExchange.API
{
    public interface IApiSettings
    {
        string PrivateKey { get; set; }
        string PublicKey { get; set; }
    }
}