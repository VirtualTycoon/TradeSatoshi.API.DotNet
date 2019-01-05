namespace CryptoExchange.API
{
    public class ApiError
    {
        public ApiError()
        {
        }

        public string Uri { get; set; }
        public string ErrorMessage { get; set; }
    }
}